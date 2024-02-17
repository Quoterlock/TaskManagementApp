using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;

namespace TasksApp.DataAccess.Repositories
{
    public class ScheduleSchemeRepository : IScheduleSchemeRepository
    {
        private readonly AppDbContext _context;

        public ScheduleSchemeRepository(AppDbContext context) 
        {
            _context = context;
        }

        public void Add(ScheduleItemEntity item)
        {
            if (item != null)
            {
                item.Id = Guid.NewGuid().ToString();
                _context.ScheduleItems.Add(item);
                _context.SaveChanges();
            }
            else throw new ArgumentNullException("schedule_item");
        }

        public void Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var item = _context.ScheduleItems.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    _context.ScheduleItems.Remove(item);
                    _context.SaveChanges();
                }
                else throw new Exception("Item doesn't exist with id: " + id);
            }
            else throw new ArgumentNullException("schedule_item_id");
        }

        public ScheduleItemEntity Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ScheduleItemEntity> GetAll()
        {
            return _context.ScheduleItems ?? Enumerable.Empty<ScheduleItemEntity>();
        }

        public void Update(ScheduleItemEntity item)
        {
            if (item != null)
            {
                _context.ChangeTracker.Clear();
                _context.ScheduleItems.Update(item);
                _context.SaveChanges();
            }
            else throw new ArgumentNullException("schedule_item");
        }
    }
}
