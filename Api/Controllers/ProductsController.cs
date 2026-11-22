using ComputerStoreApi.Data;
using ComputerStoreApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductRepository repository, ILogger<ProductsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Возвращает список всех товаров.
    /// </summary>
    /// <response code="200">Список успешно получен.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetAll(CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync(cancellationToken);
        return Ok(products);
    }

    /// <summary>
    /// Возвращает товар по его идентификатору.
    /// </summary>
    /// <param name="id">Уникальный номер товара.</param>
    /// <response code="200">Товар найден и возвращен.</response>
    /// <response code="404">Товар с указанным номером не найден.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetById(int id, CancellationToken cancellationToken)
    {
        Product? product = await _repository.GetByIdAsync(id, cancellationToken);
        if (product is null)
            return NotFound();

        return Ok(product);
    }

    /// <summary>
    /// Добавляет новый товар в каталог.
    /// </summary>
    /// <remarks>
    /// Название товара должно быть уникальным (без учета регистра).
    /// </remarks>
    /// <param name="request">Данные нового товара.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Товар успешно создан; в теле ответа возвращается созданная запись.</response>
    /// <response code="400">Переданные данные не прошли валидацию.</response>
    /// <response code="409">Товар с таким названием уже существует.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Product>> Create(CreateProductRequest request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Product> all = await _repository.GetAllAsync(cancellationToken);
        string productName = request.Name.Trim();

        bool nameExists = all.Any(p => string.Equals(
            p.Name.Trim(), productName, StringComparison.OrdinalIgnoreCase));

        if (nameExists)
            return Conflict($"Товар с названием «{productName}» уже существует.");

        var product = new Product
        {
            Name = productName,
            Manufacturer = request.Manufacturer.Trim(),
            Category = request.Category.Trim(),
            Price = request.Price,
            Stock = request.Stock
        };

        Product created = await _repository.AddAsync(product, cancellationToken);
        _logger.LogInformation("Создан товар {ProductId}: {ProductName}", created.Id, created.Name);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Удаляет товар из каталога по его идентификатору.
    /// </summary>
    /// <param name="id">Уникальный номер удаляемого товара.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="204">Товар успешно удален.</response>
    /// <response code="404">Товар с указанным номером не найден.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        bool deleted = await _repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
            return NotFound();

        _logger.LogInformation("Удалён товар {ProductId}", id);
        return NoContent();
    }
}