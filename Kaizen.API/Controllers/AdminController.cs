using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kaizen.API.Data;
using Kaizen.API.Models;
using System.Security.Claims;

namespace Kaizen.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AdminController : ControllerBase
{
    private readonly KaizenDbContext _context;

    public AdminController(KaizenDbContext context)
    {
        _context = context;
    }

    private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    private async Task<bool> IsAdmin()
    {
        var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == GetUserId());
        return profile?.IsAdmin ?? false;
    }

    [HttpGet("check")]
    public async Task<ActionResult<bool>> CheckAdmin()
    {
        return await IsAdmin();
    }

    [HttpGet("users")]
    public async Task<ActionResult<List<UserProfile>>> GetAllUsers()
    {
        if (!await IsAdmin()) return Forbid();

        return await _context.UserProfiles.ToListAsync();
    }

    [HttpDelete("users/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        if (!await IsAdmin()) return Forbid();

        var profile = await _context.UserProfiles.FindAsync(id);
        if (profile == null) return NotFound();

        var userId = profile.UserId;

        _context.WorkoutLogs.RemoveRange(_context.WorkoutLogs.Where(w => w.UserId == userId));
        _context.FoodLogs.RemoveRange(_context.FoodLogs.Where(f => f.UserId == userId));
        _context.WeightLogs.RemoveRange(_context.WeightLogs.Where(w => w.UserId == userId));
        _context.UserProfiles.Remove(profile);

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("ingredients")]
    public async Task<ActionResult<Ingredient>> CreateIngredient(Ingredient ingredient)
    {
        if (!await IsAdmin()) return Forbid();

        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetIngredients), new { id = ingredient.Id }, ingredient);
    }

    [HttpGet("ingredients")]
    public async Task<ActionResult<List<Ingredient>>> GetIngredients()
    {
        if (!await IsAdmin()) return Forbid();

        return await _context.Ingredients.ToListAsync();
    }

    [HttpPut("ingredients/{id}")]
    public async Task<IActionResult> UpdateIngredient(int id, Ingredient updated)
    {
        if (!await IsAdmin()) return Forbid();

        var ingredient = await _context.Ingredients.FindAsync(id);
        if (ingredient == null) return NotFound();

        ingredient.Name = updated.Name;
        ingredient.Calories = updated.Calories;
        ingredient.Protein = updated.Protein;
        ingredient.Carbs = updated.Carbs;
        ingredient.Fat = updated.Fat;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("ingredients/{id}")]
    public async Task<IActionResult> DeleteIngredient(int id)
    {
        if (!await IsAdmin()) return Forbid();

        var ingredient = await _context.Ingredients.FindAsync(id);
        if (ingredient == null) return NotFound();

        _context.Ingredients.Remove(ingredient);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("templates")]
    public async Task<ActionResult<List<WorkoutTemplate>>> GetTemplates()
    {
        if (!await IsAdmin()) return Forbid();

        return await _context.WorkoutTemplates
            .Include(t => t.Exercises)
            .ToListAsync();
    }

    [HttpPost("templates")]
    public async Task<ActionResult<WorkoutTemplate>> CreateTemplate(WorkoutTemplate template)
    {
        if (!await IsAdmin()) return Forbid();

        _context.WorkoutTemplates.Add(template);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTemplates), new { id = template.Id }, template);
    }

    [HttpDelete("templates/{id}")]
    public async Task<IActionResult> DeleteTemplate(int id)
    {
        if (!await IsAdmin()) return Forbid();

        var template = await _context.WorkoutTemplates.FindAsync(id);
        if (template == null) return NotFound();

        _context.WorkoutTemplates.Remove(template);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}