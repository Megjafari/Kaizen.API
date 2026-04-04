namespace Kaizen.API.DTOs;

public class CreateFoodLogDto
{
    public DateTime Date { get; set; }
    public int IngredientId { get; set; }
    public decimal AmountGrams { get; set; }
}