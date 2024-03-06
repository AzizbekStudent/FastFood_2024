using Fast_Food.DAL.Data;
using Fast_Food.DAL.Interface;
using Fast_Food.DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
        public async Task<int> Create(Employee entity)
        {
            try
            {
                var result = await _dbContext.Database.ExecuteSqlRawAsync("exec pEmployee_Create @FName, @LName, @Telephone, @Job, @Age, @Salary, @HireDate, @Image, @FullTime, @Errors OUT",
                    new SqlParameter("@FName", entity.FName),
                    new SqlParameter("@LName", entity.LName),
                    new SqlParameter("@Telephone", entity.Telephone ?? (object)DBNull.Value),
                    new SqlParameter("@Job", entity.Job),
                    new SqlParameter("@Age", entity.Age),
                    new SqlParameter("@Salary", entity.Salary ?? (object)DBNull.Value),
                    new SqlParameter("@HireDate", entity.HireDate),
                    new SqlParameter("@Image", entity.Image ?? (object)DBNull.Value),
                    new SqlParameter("@FullTime", entity.FullTime),
                    new SqlParameter("@Errors", SqlDbType.NVarChar, 1000) { Direction = ParameterDirection.Output });

                if (result > 0)
                {
                    return 1; // employee created
                }
                else
                {
                    return 0; // something went wrong
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating employee: {ex.Message}");
                return 0;
            }
        }

        // Delete
        public async void Delete(int Id)
        {
            try
            {
                var result = await _dbContext.Database.ExecuteSqlRawAsync("exec pEmployeeDelete @EmployeeID", Id);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured when deleting an employee: {ex.Message}");
            }
        }

        // Get All
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            try
            {
                var employees = await _dbContext.Employees.FromSqlRaw("exec pEmployee_GetAll").ToListAsync();

                return employees;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting employees: {ex.Message}");
                return new List<Employee>();
            }
        }

        // Get By Id
        public async Task<Employee> GetByIdAsync(int id)
        {
            try
            {
                var employee = await _dbContext.Employees.FromSqlRaw("exec pEmployee_GetById @Employee_ID", id).FirstOrDefaultAsync();

                return employee;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting employee by id: {ex.Message}");

                return null;
            }
        }

        // Update
        public async Task<int> Update(Employee entity)
        {
            try
            {
                var result = await _dbContext.Database.ExecuteSqlRawAsync("exec pEmployee_Update @EmployeeID, @FName, @LName, @Telephone, @Job, @Age, @Salary, @HireDate, @Image, @FullTime, @Errors OUT",
                    new SqlParameter("@EmployeeID", entity.Employee_ID),
                    new SqlParameter("@FName", entity.FName),
                    new SqlParameter("@LName", entity.LName),
                    new SqlParameter("@Telephone", entity.Telephone ?? (object)DBNull.Value),
                    new SqlParameter("@Job", entity.Job),
                    new SqlParameter("@Age", entity.Age),
                    new SqlParameter("@Salary", entity.Salary ?? (object)DBNull.Value),
                    new SqlParameter("@HireDate", entity.HireDate),
                    new SqlParameter("@Image", entity.Image ?? (object)DBNull.Value),
                    new SqlParameter("@FullTime", entity.FullTime),
                    new SqlParameter("@Errors", SqlDbType.NVarChar, 1000) { Direction = ParameterDirection.Output });

                if (result > 0)
                {
                    return 1; // Updated
                }
                else
                {
                    return 0; // something went wrong
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine($"Error updating employee: {ex.Message}");
                return 0;
            }
        }
    }
}
