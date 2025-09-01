namespace Biblioteca.Data
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        T? Get(int id);
        IEnumerable<T> GetAll();
        bool Update(T entity);
        bool Delete(int id);
        void SaveChanges();
    }
}