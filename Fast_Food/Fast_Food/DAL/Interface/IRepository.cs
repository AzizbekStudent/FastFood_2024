namespace Fast_Food.DAL.Interface
{
    // Students ID: 00013836, 00014725, 00014896
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<int> Create (T entity);

        Task<int> Update(T entity);

        void Delete(int Id);
    }
}
