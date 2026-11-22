using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Diagnostics;
namespace ComputerStoreDesk;

public partial class Form1 : Form
{
    // Адрес берётся из Properties/launchSettings.json сервиса, профиль http.
    // Если порт в вашей Visual Studio отличается, измените только это значение.
    private readonly HttpClient _http = new()
    {
        BaseAddress = new Uri("http://localhost:5101/"),
        Timeout = TimeSpan.FromSeconds(10)
    };

    // Источник отмены текущей загрузки.
    private CancellationTokenSource? _loadCts;
    private List<Product> _allProducts = new();

    public Form1()
    {
        InitializeComponent();
        SetupHelp(); 
        loadButton.Click += loadButton_Click;
        cancelButton.Click += cancelButton_Click;
        syncLoadButton.Click += syncLoadButton_Click;
        addButton.Click += addButton_Click;
        searchTextBox.TextChanged += searchTextBox_TextChanged;
        FormClosed += Form1_FormClosed;
        SetBusy(false);
    }

    // Искусственная задержка в 2 секунды нужна для проверки индикатора и отмены.
    // После демонстрации задания 5 её можно удалить.
    private async Task<List<Product>> LoadProductsAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(2000, cancellationToken);

        List<Product>? items = await _http.GetFromJsonAsync<List<Product>>(
            "api/Products", cancellationToken);

        return items ?? new List<Product>();
    }

    // Задание 2. Асинхронная загрузка каталога.
    private async void loadButton_Click(object? sender, EventArgs e)
    {
        using var cts = new CancellationTokenSource();
        _loadCts = cts;
        SetBusy(true, "Загрузка...");

        try
        {
            _allProducts = await LoadProductsAsync(cts.Token);
            ApplySearchFilter();
            SetupGridHeaders();
            statusLabel.Text = $"Загружено записей: {_allProducts.Count}";
        }
        catch (OperationCanceledException) when (cts.IsCancellationRequested)
        {
            statusLabel.Text = "Загрузка отменена пользователем";
        }
        catch (TaskCanceledException)
        {
            statusLabel.Text = "Сервер не ответил вовремя";
            ShowWarning("Сервер не ответил за 10 секунд. Попробуйте позже.");
        }
        catch (HttpRequestException ex)
        {
            statusLabel.Text = "Нет связи с сервером";
            ShowWarning("Нет связи с сервером. Проверьте, запущен ли сервис." +
                Environment.NewLine + ex.Message);
        }
        catch (Exception ex)
        {
            statusLabel.Text = "Не удалось загрузить каталог";
            ShowWarning("Во время загрузки произошла ошибка: " + ex.Message);
        }
        finally
        {
            _loadCts = null;
            SetBusy(false);
        }
    }

    // Задание 3. Демонстрация неправильного синхронного ожидания.
    // .Result блокирует поток интерфейса; для выхода из зависания используйте Shift+F5.
    private void syncLoadButton_Click(object? sender, EventArgs e)
    {
        statusLabel.Text = "Эксперимент: синхронное ожидание...";
        List<Product> items = LoadProductsAsync(CancellationToken.None).Result;
        productsGrid.DataSource = items;
    }

    // Задание 4. Добавление товара и разбор ответов 201, 400 и 409.
    private async void addButton_Click(object? sender, EventArgs e)
    {
        // Очищаем старые значки ошибок перед новой попыткой отправки
        errorProvider.Clear();

        if (string.IsNullOrWhiteSpace(nameTextBox.Text))
        {
            errorProvider.SetError(nameTextBox, "Укажите название товара.");
            nameTextBox.Focus();
            return;
        }

        var request = new CreateProductRequest
        {
            Name = nameTextBox.Text.Trim(),
            Manufacturer = manufacturerTextBox.Text.Trim(),
            Category = categoryTextBox.Text.Trim(),
            Price = priceNumeric.Value,
            Stock = decimal.ToInt32(stockNumeric.Value)
        };

        bool added = false;
        SetBusy(true, "Сохранение...");

        try
        {
            using HttpResponseMessage response = await _http.PostAsJsonAsync(
                "api/Products", request);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                Product? created = await response.Content.ReadFromJsonAsync<Product>();
                statusLabel.Text = $"Добавлен товар № {created?.Id}";
                nameTextBox.Clear();
                manufacturerTextBox.Clear();
                categoryTextBox.Clear();
                added = true;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                statusLabel.Text = "Данные не прошли проверку";

                // Читаем стандартный объект ошибок валидации ASP.NET Core
                ValidationProblemResponse? problem = await response.Content
                    .ReadFromJsonAsync<ValidationProblemResponse>();

                // Отображаем красные значки ErrorProvider у полей формы
                ShowFieldErrors(problem);

                // Показываем общее предупреждение со списком ошибок
                ShowWarning(FormatValidationErrors(problem));
            }
            else if (response.StatusCode == HttpStatusCode.Conflict)
            {
                statusLabel.Text = "Операция не выполнена";
                ShowWarning(await ReadConflictMessageAsync(response));
            }
            else
            {
                int code = (int)response.StatusCode;
                statusLabel.Text = $"Ошибка сервера: {code}";
                ShowWarning("Сервер не смог сохранить данные. Попробуйте позже.");
            }
        }
        catch (TaskCanceledException)
        {
            statusLabel.Text = "Сервер не ответил вовремя";
            ShowWarning("Сервер не ответил за 10 секунд. Попробуйте позже.");
        }
        catch (HttpRequestException ex)
        {
            statusLabel.Text = "Нет связи с сервером";
            ShowWarning("Нет связи с сервером. Проверьте, запущен ли сервис." +
                Environment.NewLine + ex.Message);
        }
        catch (Exception ex)
        {
            statusLabel.Text = "Не удалось добавить товар";
            ShowWarning("Во время сохранения произошла ошибка: " + ex.Message);
        }
        finally
        {
            SetBusy(false);
        }

        if (added)
            loadButton.PerformClick(); // Обновить каталог после успешного добавления.
    }

    // Метод для расстановки красных значков ErrorProvider около полей
    private void ShowFieldErrors(ValidationProblemResponse? problem)
    {
        if (problem?.Errors is null)
            return;

        var fields = new Dictionary<string, Control>(StringComparer.OrdinalIgnoreCase)
        {
            ["Name"] = nameTextBox,
            ["Manufacturer"] = manufacturerTextBox,
            ["Category"] = categoryTextBox,
            ["Price"] = priceNumeric,
            ["Stock"] = stockNumeric,
        };

        foreach (var (field, messages) in problem.Errors)
        {
            if (fields.TryGetValue(field, out Control? control))
                errorProvider.SetError(control, string.Join(" ", messages));
        }
    }

    // Форматирование текста ошибок для всплывающего окна
    private static string FormatValidationErrors(ValidationProblemResponse? problem)
    {
        if (problem?.Errors is null || problem.Errors.Count == 0)
            return "Данные не прошли проверку на сервере.";

        IEnumerable<string> messages = problem.Errors.SelectMany(pair => pair.Value);

        return "Исправьте ошибки (поля отмечены красным значком):" + Environment.NewLine +
            string.Join(Environment.NewLine, messages);
    }

    private static async Task<string> ReadConflictMessageAsync(HttpResponseMessage response)
    {
        string content = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(content))
            return "Операция противоречит данным, уже сохранённым в каталоге.";

        try
        {
            return JsonSerializer.Deserialize<string>(content) ?? content;
        }
        catch (JsonException)
        {
            return content;
        }
    }

    // Задание 5. Отмена текущей загрузки.
    private void cancelButton_Click(object? sender, EventArgs e)
    {
        _loadCts?.Cancel();
    }

    // Дополнительное задание 7. Локальный поиск без нового запроса к серверу.
    private void searchTextBox_TextChanged(object? sender, EventArgs e)
    {
        ApplySearchFilter();
    }

    private void ApplySearchFilter()
    {
        string query = searchTextBox.Text.Trim();
        List<Product> filtered = string.IsNullOrWhiteSpace(query)
            ? _allProducts.ToList()
            : _allProducts.Where(product =>
                product.Name.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

        productsGrid.DataSource = null;
        productsGrid.DataSource = filtered;
        SetupGridHeaders();
    }

    private void SetBusy(bool busy, string status = "")
    {
        loadButton.Enabled = !busy;
        addButton.Enabled = !busy;
        syncLoadButton.Enabled = !busy;
        cancelButton.Enabled = busy && _loadCts is not null;
        progressBar.Visible = busy;
        if (status.Length > 0)
            statusLabel.Text = status;
    }

    private void SetupGridHeaders()
    {
        SetHeader(nameof(Product.Id), "№");
        SetHeader(nameof(Product.Name), "Название");
        SetHeader(nameof(Product.Manufacturer), "Производитель");
        SetHeader(nameof(Product.Category), "Категория");
        SetHeader(nameof(Product.Price), "Цена, руб.");
        SetHeader(nameof(Product.Stock), "Остаток, шт.");
        SetHeader(nameof(Product.CreatedAt), "Дата добавления");
    }

    private void SetHeader(string propertyName, string headerText)
    {
        if (productsGrid.Columns[propertyName] is DataGridViewColumn column)
            column.HeaderText = headerText;
    }

    private static void ShowWarning(string message) =>
        MessageBox.Show(message, "Магазин компьютерной техники",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);

    private void Form1_FormClosed(object? sender, FormClosedEventArgs e)
    {
        _loadCts?.Cancel();
        _http.Dispose();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {

    }

    private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {

    }

    private void руководствоПользователяToolStripMenuItem_Click(object sender, EventArgs e)
    {

        string path = Path.Combine(AppContext.BaseDirectory, "Help", "index.html");
        if (!File.Exists(path))
        {
            ShowWarning("Файл справки не найден:" + Environment.NewLine + path);
            return;
        }
        Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });

    }

    private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
    {
        MessageBox.Show(
        "Система управления магазином компьютерной техники" + Environment.NewLine +
        $"Версия {Application.ProductVersion}" + Environment.NewLine +
        "Курсовой проект по МДК 02.01" + Environment.NewLine +
        "Автор: Караваева Анастасия Яновна, группа 24-ИСП-3",
        "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void SetupHelp()
    {
        toolTip.SetToolTip(loadButton, "Получить с сервера актуальный список товаров");
        toolTip.SetToolTip(cancelButton, "Прервать загрузку, если сервер долго не отвечает");
        toolTip.SetToolTip(syncLoadButton, "Учебный эксперимент: окно зависнет. Не нажимайте при работе!");
        toolTip.SetToolTip(addButton, "Сохранить новый товар на сервере");
        toolTip.SetToolTip(nameTextBox, "Название товара, от 3 до 150 символов");
        toolTip.SetToolTip(manufacturerTextBox, "Производитель товара, от 2 до 100 символов");
        toolTip.SetToolTip(categoryTextBox, "Категория товара, от 2 до 80 символов");
        toolTip.SetToolTip(priceNumeric, "Цена товара в рублях (от 0,01 до 2 000 000)");
        toolTip.SetToolTip(stockNumeric, "Количество на складе (от 0 до 10 000)");

        nameTextBox.PlaceholderText = "Например: AMD Ryzen 5 5600";
        manufacturerTextBox.PlaceholderText = "Например: AMD";
        categoryTextBox.PlaceholderText = "Например: Процессоры";

        helpProvider.SetHelpString(nameTextBox, "Укажите название товара. Обязательно, от 3 до 150 символов.");
        helpProvider.SetHelpString(manufacturerTextBox, "Укажите производителя товара. Обязательно, от 2 до 100 символов.");
        helpProvider.SetHelpString(categoryTextBox, "Укажите категорию товара. Обязательно, от 2 до 80 символов.");
        helpProvider.SetHelpString(priceNumeric, "Укажите цену товара в рублях: от 0,01 до 2 000 000.");
        helpProvider.SetHelpString(stockNumeric, "Укажите остаток товара на складе: от 0 до 10 000 штук.");
        helpProvider.SetHelpString(productsGrid, "Каталог товаров магазина. Для обновления нажмите «Загрузить».");
    }

}
