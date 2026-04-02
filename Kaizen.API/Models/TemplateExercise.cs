namespace Kaizen.API.Models;

public class TemplateExercise
{
    public int Id { get; set; }
    public int TemplateId { get; set; }
    public WorkoutTemplate Template { get; set; } = null!;
    public string ExerciseName { get; set; } = string.Empty;
    public int Sets { get; set; }
    public int Reps { get; set; }
    public string? Notes { get; set; }
}