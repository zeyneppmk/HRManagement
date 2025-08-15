using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagement.Entitites;

namespace HRManagement.Data.Repository.IRepository
{
    public interface IEventRepository : IRepository<Event>
    {
        void Update(Event eventItem);
    }
}


