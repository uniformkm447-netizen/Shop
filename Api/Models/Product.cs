namespace ComputerStoreApi.Models;

/// <summary>Товар каталога компьютерного магазина.</summary>
public class Product
{
    /// <summary>Уникальный номер, назначаемый сервером.</summary>
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    /// <summary>Дата и время добавления записи, назначаемые сервером.</summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
