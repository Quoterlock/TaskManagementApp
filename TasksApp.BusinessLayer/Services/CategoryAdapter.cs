using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.DataAccess.Entities;

namespace TasksApp.BusinessLogic.Services
{
    public class CategoryAdapter : IAdapterME<CategoryModel, CategoryEntity>
    {
        public CategoryModel EntityToModel(CategoryEntity entity)
        {
            return new CategoryModel { 
                Id = entity.Id, 
                Name = entity.Name 
            };
        }

        public CategoryEntity ModelToEntity(CategoryModel model)
        {
            return new CategoryEntity 
            { 
                Id = model.Id, 
                Name = model.Name, 
            };
        }
    }
}
