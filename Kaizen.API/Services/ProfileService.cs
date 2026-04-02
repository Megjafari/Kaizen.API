using Microsoft.EntityFrameworkCore;
using Kaizen.API.Data;
using Kaizen.API.Models;

namespace Kaizen.API.Services;

public class ProfileService : IProfileService
{
    private readonly KaizenDbContext _context;

    public ProfileService(KaizenDbContext context)
    {
        _context = context;
    }

    public async Task<UserProfile?> GetProfileAsync(string userId)
    {
        return await _context.UserProfiles
            .FirstOrDefaultAsync(p => p.UserId == userId);
    }

    public async Task<UserProfile> CreateProfileAsync(string userId, UserProfile profile)
    {
        profile.UserId = userId;
        _context.UserProfiles.Add(profile);
        await _context.SaveChangesAsync();
        return profile;
    }

    public async Task<UserProfile?> UpdateProfileAsync(string userId, UserProfile updated)
    {
        var profile = await _context.UserProfiles
            .FirstOrDefaultAsync(p => p.UserId == userId);

        if (profile == null)
            return null;

        profile.Height = updated.Height;
        profile.Weight = updated.Weight;
        profile.Age = updated.Age;
        profile.Gender = updated.Gender;
        profile.Goal = updated.Goal;

        await _context.SaveChangesAsync();
        return profile;
    }
}