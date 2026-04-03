using Kaizen.API.Models;

namespace Kaizen.API.Data;

public static class SeedData
{
    public static async Task InitializeAsync(KaizenDbContext context)
    {
        if (context.Ingredients.Any())
            return;

        // Ingredients (per 100g)
        var ingredients = new List<Ingredient>
        {
            new() { Name = "Chicken breast", Calories = 165, Protein = 31, Carbs = 0, Fat = 3.6m },
            new() { Name = "Salmon fillet", Calories = 208, Protein = 20, Carbs = 0, Fat = 13 },
            new() { Name = "Egg", Calories = 155, Protein = 13, Carbs = 1.1m, Fat = 11 },
            new() { Name = "Rice (cooked)", Calories = 130, Protein = 2.7m, Carbs = 28, Fat = 0.3m },
            new() { Name = "Pasta (cooked)", Calories = 131, Protein = 5, Carbs = 25, Fat = 1.1m },
            new() { Name = "Oats", Calories = 389, Protein = 17, Carbs = 66, Fat = 7 },
            new() { Name = "Banana", Calories = 89, Protein = 1.1m, Carbs = 23, Fat = 0.3m },
            new() { Name = "Apple", Calories = 52, Protein = 0.3m, Carbs = 14, Fat = 0.2m },
            new() { Name = "Broccoli", Calories = 34, Protein = 2.8m, Carbs = 7, Fat = 0.4m },
            new() { Name = "Sweet potato", Calories = 86, Protein = 1.6m, Carbs = 20, Fat = 0.1m },
            new() { Name = "Ground beef (10%)", Calories = 176, Protein = 20, Carbs = 0, Fat = 10 },
            new() { Name = "Greek yogurt", Calories = 63, Protein = 11, Carbs = 4, Fat = 0.2m },
            new() { Name = "Peanut butter", Calories = 588, Protein = 25, Carbs = 20, Fat = 50 },
            new() { Name = "Olive oil", Calories = 884, Protein = 0, Carbs = 0, Fat = 100 },
            new() { Name = "Almonds", Calories = 579, Protein = 21, Carbs = 22, Fat = 49 },
            new() { Name = "Cottage cheese", Calories = 98, Protein = 11, Carbs = 3.4m, Fat = 4.3m },
            new() { Name = "Whey protein", Calories = 375, Protein = 75, Carbs = 10, Fat = 5 },
            new() { Name = "Whole milk", Calories = 60, Protein = 3.4m, Carbs = 4.7m, Fat = 3 },
            new() { Name = "Whole grain bread", Calories = 247, Protein = 13, Carbs = 41, Fat = 3.4m },
            new() { Name = "Avocado", Calories = 160, Protein = 2, Carbs = 9, Fat = 15 }
        };

        context.Ingredients.AddRange(ingredients);

        // Workout Templates
        var templates = new List<WorkoutTemplate>
        {
            new()
            {
                Name = "Push Day",
                Description = "Chest, shoulders and triceps",
                Level = "intermediate",
                Exercises =
                [
                    new() { ExerciseName = "Bench press", Sets = 4, Reps = 8, Notes = "Controlled negative" },
                    new() { ExerciseName = "Overhead press", Sets = 3, Reps = 10 },
                    new() { ExerciseName = "Dips", Sets = 3, Reps = 12 },
                    new() { ExerciseName = "Lateral raises", Sets = 3, Reps = 15 },
                    new() { ExerciseName = "Tricep pushdowns", Sets = 3, Reps = 12 }
                ]
            },
            new()
            {
                Name = "Pull Day",
                Description = "Back and biceps",
                Level = "intermediate",
                Exercises =
                [
                    new() { ExerciseName = "Deadlift", Sets = 4, Reps = 6, Notes = "Focus on form" },
                    new() { ExerciseName = "Lat pulldown", Sets = 3, Reps = 10 },
                    new() { ExerciseName = "Barbell row", Sets = 3, Reps = 10 },
                    new() { ExerciseName = "Face pulls", Sets = 3, Reps = 15 },
                    new() { ExerciseName = "Bicep curls", Sets = 3, Reps = 12 }
                ]
            },
            new()
            {
                Name = "Leg Day",
                Description = "Legs and glutes",
                Level = "intermediate",
                Exercises =
                [
                    new() { ExerciseName = "Squat", Sets = 4, Reps = 8 },
                    new() { ExerciseName = "Romanian deadlift", Sets = 3, Reps = 10 },
                    new() { ExerciseName = "Leg press", Sets = 3, Reps = 12 },
                    new() { ExerciseName = "Walking lunges", Sets = 3, Reps = 12, Notes = "Per leg" },
                    new() { ExerciseName = "Calf raises", Sets = 4, Reps = 15 }
                ]
            },
            new()
            {
                Name = "Full Body Beginner",
                Description = "Basic full body workout",
                Level = "beginner",
                Exercises =
                [
                    new() { ExerciseName = "Goblet squat", Sets = 3, Reps = 10 },
                    new() { ExerciseName = "Push-ups", Sets = 3, Reps = 10, Notes = "On knees if needed" },
                    new() { ExerciseName = "Dumbbell row", Sets = 3, Reps = 10 },
                    new() { ExerciseName = "Plank", Sets = 3, Reps = 30, Notes = "Seconds" }
                ]
            },
            new()
            {
                Name = "Upper/Lower - Upper",
                Description = "Upper body strength focus",
                Level = "advanced",
                Exercises =
                [
                    new() { ExerciseName = "Bench press", Sets = 5, Reps = 5 },
                    new() { ExerciseName = "Barbell row", Sets = 5, Reps = 5 },
                    new() { ExerciseName = "Overhead press", Sets = 4, Reps = 6 },
                    new() { ExerciseName = "Pull-ups", Sets = 4, Reps = 8 },
                    new() { ExerciseName = "Bicep curls", Sets = 3, Reps = 10 },
                    new() { ExerciseName = "Tricep extensions", Sets = 3, Reps = 10 }
                ]
            }
        };

        context.WorkoutTemplates.AddRange(templates);

        await context.SaveChangesAsync();
    }
}