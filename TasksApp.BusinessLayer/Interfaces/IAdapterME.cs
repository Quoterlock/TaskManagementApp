using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksApp.BusinessLogic.Interfaces
{
    public interface IAdapterME<TModel, TEntity>
    {
        TEntity ModelToEntity(TModel model);
        TModel EntityToModel(TEntity entity);
    }
}
