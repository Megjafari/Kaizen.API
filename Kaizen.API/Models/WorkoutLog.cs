namespace Kaizen.API.Models;

public class WorkoutLog
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public List<ExerciseLog> Exercises { get; set; } = [];
}