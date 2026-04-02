namespace Kaizen.API.Models;

public class WorkoutTemplate
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty; // beginner/intermediate/advanced
    public List<TemplateExercise> Exercises { get; set; } = [];
}