using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;

namespace TasksApp.BusinessLogic.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IProjectsService _projectsService;
        private readonly IAdapterME<CategoryModel, CategoryEntity> _categoryAdapter;
        public CategoriesService(ICategoriesRepository categoriesRepository, IProjectsService projectsService,
            IAdapterME<CategoryModel, CategoryEntity> categoryAdapter)
        {
            _categoriesRepository = categoriesRepository;
            _projectsService = projectsService;
            _categoryAdapter = categoryAdapter;
        }

        public void RenameCategory(CategoryModel category, string newName)
        {
            if (category != null)
            {
                if (!string.IsNullOrEmpty(newName))
                {
                    try
                    {
                        category.Name = newName;
                        _categoriesRepository.Update(_categoryAdapter.ModelToEntity(category));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else throw new Exception("New category_name is null or empty");
            }
            else throw new ArgumentNullException(nameof(category));
        }

        public void AddCategory(string categoryName)
        {
            if (!string.IsNullOrEmpty(categoryName))
            {
                try
                {
                    _categoriesRepository.Create(categoryName);
                } 
                catch(Exception ex) 
                {
                    throw new Exception(ex.Message); 
                }
            }
        }

        public void DeleteCategory(CategoryModel category)
        {
            if(category != null)
            {
                try
                {
                    var projects = _projectsService.GetProjectsList();
                    foreach (var project in projects.Where(p=>p.CategoryId == category.Id))
                        _projectsService.DeleteProject(project.Id);
                    _categoriesRepository.Delete(_categoryAdapter.ModelToEntity(category));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public CategoryModel GetCategoryWithProjects(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    var entity = _categoriesRepository.Get(id);
                    var categoryModel = _categoryAdapter.EntityToModel(entity);
                    
                    categoryModel.Projects = _projectsService.GetProjectsList()
                        .Where(p=>p.CategoryId == categoryModel.Id)
                        .ToList();

                    return categoryModel;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else throw new ArgumentNullException("category_id");
        }

        public List<CategoryModel> GetList()
        {
            var entities = _categoriesRepository.GetAll();
            var categories = new List<CategoryModel>();
            foreach (var entity in entities)
                categories.Add(_categoryAdapter.EntityToModel(entity));
            return categories;
        }
    }
}
