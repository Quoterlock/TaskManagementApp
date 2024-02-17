namespace TasksApp.DataAccess.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(string id);
        T Get(string id);
        IEnumerable<T> GetAll();
    }
}
