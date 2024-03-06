using Fast_Food.DAL.Data;
using Fast_Food.DAL.Interface;
using Fast_Food.DAL.Models;

namespace Fast_Food.DAL.Repositories
{
    // Students ID: 00013836, 00014725, 00014896
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly FastFood_DbContext _dbContext;

        public EmployeeRepository(FastFood_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Main functions
        // Create
        public int Create(Employee entity)
        {
            throw new NotImplementedException();
        }

        // Delete
        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        // Get All
        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        // Get By Id
        public Employee GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        // Update
        public void Update(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
