namespace ComputerStoreDesk;

/// <summary>Товар, полученный из Web API каталога компьютерного магазина.</summary>
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>Тело ответа ASP.NET Core при ошибке проверки данных (HTTP 400).</summary>
public class ValidationProblemResponse
{
    public string? Title { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }
}

/// <summary>Данные, отправляемые клиентом для создания товара.</summary>
public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
