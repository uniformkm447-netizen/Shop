using System.ComponentModel.DataAnnotations;

namespace ComputerStoreApi.Models;

/// <summary>Данные, которые пользователь передаёт для создания товара.</summary>
public class CreateProductRequest
{
    [Required(ErrorMessage = "Наименование товара обязательно для заполнения.")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "Наименование должно содержать от 3 до 150 символов.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите производителя.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Название производителя должно содержать от 2 до 100 символов.")]
    public string Manufacturer { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите категорию товара.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "Название категории должно содержать от 2 до 80 символов.")]
    public string Category { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "2000000.00", ParseLimitsInInvariantCulture = true,
        ErrorMessage = "Цена должна быть от 0,01 до 2 000 000 рублей.")]
    public decimal Price { get; set; }

    [Range(0, 10000, ErrorMessage = "Количество на складе должно быть от 0 до 10 000 штук.")]
    public int Stock { get; set; }
}
