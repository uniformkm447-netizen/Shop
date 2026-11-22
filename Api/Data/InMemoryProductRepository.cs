using ComputerStoreApi.Models;

namespace ComputerStoreApi.Data;

/// <summary>Потокобезопасное учебное хранилище товаров в памяти.</summary>
public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _items = new()
    {
        new() { Id = 1, Name = "AMD Ryzen 5 5600", Manufacturer = "AMD", Category = "Процессоры", Price = 10990.00m, Stock = 12 },
        new() { Id = 2, Name = "Logitech G102 Lightsync", Manufacturer = "Logitech", Category = "Периферия", Price = 2190.00m, Stock = 25 },
        new() { Id = 3, Name = "Samsung 990 EVO 1TB", Manufacturer = "Samsung", Category = "Накопители", Price = 9990.00m, Stock = 7 }
    };

    private readonly object _sync = new();
    private int _nextId = 4;

    public Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_sync)
        {
            IReadOnlyList<Product> copy = _items.Select(Clone).ToList();
            return Task.FromResult(copy);
        }
    }

    public Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_sync)
        {
            Product? product = _items.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product is null ? null : Clone(product));
        }
    }

    public Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_sync)
        {
            product.Id = _nextId++;
            _items.Add(Clone(product));
            return Task.FromResult(Clone(product));
        }
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_sync)
        {
            int removed = _items.RemoveAll(p => p.Id == id);
            return Task.FromResult(removed > 0);
        }
    }

    private static Product Clone(Product source) => new()
    {
        Id = source.Id,
        Name = source.Name,
        Manufacturer = source.Manufacturer,
        Category = source.Category,
        Price = source.Price,
        Stock = source.Stock,
        CreatedAt = source.CreatedAt
    };
}
