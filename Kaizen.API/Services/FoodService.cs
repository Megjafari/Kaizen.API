using Microsoft.EntityFrameworkCore;
using Kaizen.API.Data;
using Kaizen.API.Models;
using Kaizen.API.DTOs;

namespace Kaizen.API.Services;

public class FoodService : IFoodService
{
    private readonly KaizenDbContext _context;

    public FoodService(KaizenDbContext context)
    {
        _context = context;
    }

    public async Task<List<Ingredient>> SearchIngredientsAsync(string? query)
    {
        var q = _context.Ingredients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
            q = q.Where(i => i.Name.ToLower().Contains(query.ToLower()));

        return await q.Take(20).ToListAsync();
    }

    public async Task<Ingredient?> GetIngredientAsync(int id)
    {
        return await _context.Ingredients.FindAsync(id);
    }

    public async Task<List<FoodLog>> GetLogsAsync(string userId, DateTime? date)
    {
        var query = _context.FoodLogs
            .Include(f => f.Ingredient)
            .Where(f => f.UserId == userId);

        if (date.HasValue)
        {
            var utcDate = DateTime.SpecifyKind(date.Value.Date, DateTimeKind.Utc);
            var nextDay = utcDate.AddDays(1);
            query = query.Where(f => f.Date >= utcDate && f.Date < nextDay);
        }

        return await query.OrderByDescending(f => f.Date).ToListAsync();
    }

    public async Task<FoodLog> CreateLogAsync(string userId, CreateFoodLogDto dto)
    {
        var log = new FoodLog
        {
            UserId = userId,
            Date = DateTime.SpecifyKind(dto.Date, DateTimeKind.Utc),
            IngredientId = dto.IngredientId,
            AmountGrams = dto.AmountGrams
        };

        _context.FoodLogs.Add(log);
        await _context.SaveChangesAsync();

        await _context.Entry(log).Reference(f => f.Ingredient).LoadAsync();
        return log;
    }

    public async Task<bool> DeleteLogAsync(string userId, int id)
    {
        var log = await _context.FoodLogs
            .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);

        if (log == null)
            return false;

        _context.FoodLogs.Remove(log);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<DailySummary> GetDailySummaryAsync(string userId, DateTime date)
    {
        var utcDate = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);
        var nextDay = utcDate.AddDays(1);

        var logs = await _context.FoodLogs
            .Include(f => f.Ingredient)
            .Where(f => f.UserId == userId && f.Date >= utcDate && f.Date < nextDay)
            .ToListAsync();

        return new DailySummary
        {
            Date = utcDate,
            TotalCalories = logs.Sum(f => f.Ingredient.Calories * f.AmountGrams / 100),
            TotalProtein = logs.Sum(f => f.Ingredient.Protein * f.AmountGrams / 100),
            TotalCarbs = logs.Sum(f => f.Ingredient.Carbs * f.AmountGrams / 100),
            TotalFat = logs.Sum(f => f.Ingredient.Fat * f.AmountGrams / 100),
            Items = logs.Count
        };
    }
}