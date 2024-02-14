using System.Dynamic;
using TasksApp.DataAccess.Entities;

namespace TasksApp.DataAccess.Interfaces
{
    public interface ICategoriesRepository
    {
        void Create(string name);
        void Delete(CategoryEntity category);
        void Update(CategoryEntity category);
        CategoryEntity Get(string id);
        IEnumerable<CategoryEntity> GetAll();
    }
}
