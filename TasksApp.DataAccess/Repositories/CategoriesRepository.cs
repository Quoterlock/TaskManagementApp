using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;
using TasksApp;
using Microsoft.EntityFrameworkCore;

namespace TasksApp.DataAccess.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly AppDbContext _context;

        public CategoriesRepository(AppDbContext context) 
        {
            _context = context;
        }

        public void Add(CategoryEntity entity)
        {
            if (entity != null)
            {
                entity.Id = Guid.NewGuid().ToString();
                _context.Categories.Add(entity);
                _context.SaveChanges();
            }
            else throw new ArgumentNullException("category");
        }

        public void Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var category = _context.Categories.FirstOrDefault(c => c.Id == id);
                if(category != null)
                {
                    _context.Categories.Remove(category);

                    // unlink project from category
                    var projects = _context.Projects.Where(p => p.CategoryId == category.Id);
                    foreach (var project in projects)
                        project.CategoryId = string.Empty;
                    
                    // save changes
                    _context.SaveChanges();
                } 
                else throw new Exception("Category not found with id: " + id);
            }
            else throw new ArgumentNullException("category_id");
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
                var category = _context.Categories.AsNoTracking().FirstOrDefault(c => c.Id == id);
                if (category != null)
                    return category;
                else
                    throw new Exception("Category not found with id: " + id);
            }
            else throw new ArgumentNullException("category_id");
        }

        public IEnumerable<CategoryEntity> GetAll()
        {
            return _context.Categories.AsNoTracking() ?? Enumerable.Empty<CategoryEntity>();
        }
    }
}
