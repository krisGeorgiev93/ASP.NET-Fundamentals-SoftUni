using RunGroopApp.Models;

namespace RunGroopApp.Interfaces
{
    public interface IClub
    {
        Task<IEnumerable<Club>> GetAll();

        Task<Club> GetIdAsync(int id);

        Task<IEnumerable<Club>> GetClubByCity(string city);

        bool Add(Club club);

        bool Update(Club club);

        bool Delete(Club club);

        bool Save();
    }
}
