namespace Fast_Food.DAL.Interface
{
    // Students ID: 00013836, 00014725, 00014896
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        T GetByIdAsync(int id);

        int Create (T entity);

        void Update(T entity);

        void Delete(int Id);
    }
}
