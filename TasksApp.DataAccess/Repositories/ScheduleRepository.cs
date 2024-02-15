using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;

namespace TasksApp.DataAccess.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly AppDbContext _context;

        public ScheduleRepository(AppDbContext context) 
        {
            _context = context;
        }

        public void AddItem(ScheduleItemEntity item)
        {
            if (item != null)
            {
                item.Id = Guid.NewGuid().ToString();
                _context.ScheduleItems.Add(item);
                _context.SaveChanges();
            }
            else throw new ArgumentNullException("schedule_item");
        }

        public void DeleteItem(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var item = _context.ScheduleItems.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    _context.ChangeTracker.Clear();
                    _context.ScheduleItems.Remove(item);
                    _context.SaveChanges();
                }
                else throw new Exception("Item doesn't exist with id: " + id);
            }
            else throw new ArgumentNullException("schedule_item_id");
        }

        public IEnumerable<ScheduleItemEntity> GetItems()
        {
            return _context.ScheduleItems;
        }

        public void UpdateItem(ScheduleItemEntity item)
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
