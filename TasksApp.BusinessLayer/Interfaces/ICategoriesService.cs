using TasksApp.BusinessLogic.Models;

namespace TasksApp.BusinessLogic.Interfaces
{
    public interface ICategoriesService
    {
        void AddCategory(string categoryName);
        void DeleteCategory(CategoryModel category);
        void RenameCategory(CategoryModel category, string newName);
        List<CategoryModel> GetList();
        CategoryModel GetCategory(string id);
        CategoryModel GetCategoryWithProjects(string id);
    }
}
