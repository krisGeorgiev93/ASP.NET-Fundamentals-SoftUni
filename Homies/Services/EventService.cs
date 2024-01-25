using Homies.Contracts;
using Homies.Data;
using Homies.Models;
using Microsoft.EntityFrameworkCore;

namespace Homies.Services
{
    public class EventService : IEventService
    {
        private readonly HomiesDbContext dBContext;

        public EventService(HomiesDbContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<IEnumerable<AllEventsViewModel>> GetAllEventsAsync()
        {
            return await dBContext
                .Events
                .Select(e => new AllEventsViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start.ToString("dd/MM/yyyy H:mm"),
                    Type = e.Type.Name,
                    Organiser = e.Organiser.UserName
                })
                .ToListAsync(); 
        }

        public Task<DetailsViewModel?> GetDetails(int id)
        {
            return dBContext.Events
                .Where(e => e.Id == id)
                .Select(e => new DetailsViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start.ToString("dd/MM/yyyy H:mm"),
                    End = e.End.ToString("dd/MM/yyyy H:mm"),
                    Organiser = e.Organiser.UserName,
                    Type = e.Type.Name,
                    Description = e.Description,
                    CreatedOn = e.CreatedOn.ToString("dd/MM/yyyy H:mm")
                })
                .FirstOrDefaultAsync();
        }
       

        public async Task<IEnumerable<AllEventsViewModel>> GetMyJoinedEventsAsync(string userId)
        {
            return await dBContext.EventsParticipants
                 .Where(x => x.HelperId == userId)
                 .Select(e => new AllEventsViewModel
                 {
                     Id = e.Event.Id,
                     Name = e.Event.Name,
                     Start = e.Event.Start.ToString("dd-MM-yyyy H:mm"),
                     Type = e.Event.Type.Name
                 })
                 .ToListAsync();

        }


    }

}
