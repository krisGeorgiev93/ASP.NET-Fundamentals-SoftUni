using RunGroopApp.Models;

namespace RunGroopApp.Interfaces
{
    public interface IDashboard
    {
        Task<List<Race>> GetAllUserRaces();
        Task<List<Club>> GetAllUserClubs();
        Task<User> GetUserById(string id);
        Task<User> GetByIdNoTracking(string id);
        bool Update(User user);
        bool Save();
    }
}
