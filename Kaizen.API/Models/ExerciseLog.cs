namespace Kaizen.API.Models;

public class ExerciseLog
{
    public int Id { get; set; }
    public int WorkoutLogId { get; set; }
    public WorkoutLog WorkoutLog { get; set; } = null!;
    public string ExerciseName { get; set; } = string.Empty;
    public int Sets { get; set; }
    public int Reps { get; set; }
    public decimal Weight { get; set; }
}