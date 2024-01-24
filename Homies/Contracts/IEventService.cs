using Homies.Models;

namespace Homies.Contracts
{
    public interface IEventService
    {
        Task<IEnumerable<AllEventsViewModel>> GetAllEventsAsync();

        Task<IEnumerable<AllEventsViewModel>> GetMyJoinedEventsAsync(string userId);
        Task<DetailsViewModel> GetDetails(int eventId);
    }
}