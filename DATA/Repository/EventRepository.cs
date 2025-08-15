using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagement.Data.Repository.IRepository;
using HRManagement.Entitites;

namespace HRManagement.Data.Repository
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Event eventItem)
        {
            var objFromDb = _context.Events.FirstOrDefault(e => e.EventId == eventItem.EventId);

            if (objFromDb != null)
            {
                objFromDb.Title = eventItem.Title;
                objFromDb.Description = eventItem.Description;
                objFromDb.StartDate = eventItem.StartDate;
                objFromDb.EndDate = eventItem.EndDate;
                objFromDb.IsFullDay = eventItem.IsFullDay;
                objFromDb.IsRepeatable = eventItem.IsRepeatable;
                objFromDb.Type = eventItem.Type;
            }
        }
    }
}
