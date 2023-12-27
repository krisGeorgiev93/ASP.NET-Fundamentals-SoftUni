using RunGroopApp.Models;

namespace RunGroopApp.Interfaces
{
    public interface IRace
    {
        Task<IEnumerable<Race>> GetAll();

        Task<Race> GetIdAsync(int id);

        Task<IEnumerable<Race>> GetRaceByCity(string city);

        bool Add(Race race);

        bool Update(Race race);

        bool Delete(Race race);

        bool Save();
    }
}
