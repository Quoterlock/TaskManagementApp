using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;
using TasksApp;

namespace TasksApp.DataAccess.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly AppDbContext _context;
        public CategoriesRepository(AppDbContext context) 
        {
            _context = context;
        }

        public void Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _context.Categories.Add(new CategoryEntity { Id = Guid.NewGuid().ToString(), Name = name });
                _context.SaveChanges();
            }
            else throw new ArgumentNullException("category_name");
        }

        public void Delete(CategoryEntity category)
        {
            if(category != null)
            {
                if (!string.IsNullOrEmpty(category.Id))
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                }
                else throw new ArgumentNullException(nameof(category.Id));
            } 
            else throw new ArgumentNullException(nameof(category));
        }

        public void Update(CategoryEntity category)
        {
            if (category != null)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
            }
            else throw new ArgumentNullException(nameof(category));
        }

        public CategoryEntity Get(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var category = _context.Categories.FirstOrDefault(c => c.Id == id);
                if (category != null)
                    return category;
                else
                    throw new Exception("Category not found with id: " + id);
            }
            else throw new ArgumentNullException("category_id");
        }

        public IEnumerable<CategoryEntity> GetAll()
        {
            return _context.Categories ?? Enumerable.Empty<CategoryEntity>();
        }
    }
}
