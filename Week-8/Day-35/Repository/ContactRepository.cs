using Dapper;
using webapp25.Models;
using System.Data;

namespace webapp25.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly DapperContext _context;

        public ContactRepository(DapperContext context)
        {
            _context = context;
        }

        public List<ContactInfo> GetAllContacts()
        {
            string query = @"
            SELECT c.*, comp.CompanyName, d.DepartmentName
            FROM ContactInfo c
            INNER JOIN Company comp ON c.CompanyId = comp.CompanyId
            LEFT JOIN Department d ON c.DepartmentId = d.DepartmentId";

            using (var connection = _context.CreateConnection())
            {
                return connection.Query<ContactInfo>(query).ToList();
            }
        }

        public ContactInfo GetContactById(int id)
        {
            string query = "SELECT * FROM ContactInfo WHERE ContactId = @Id";

            using (var connection = _context.CreateConnection())
            {
                return connection.QueryFirstOrDefault<ContactInfo>(query, new { Id = id });
            }
        }

        public void AddContact(ContactInfo contact)
        {
            string query = @"
            INSERT INTO ContactInfo
            (FirstName, LastName, EmailId, MobileNo, Designation, CompanyId, DepartmentId)
            VALUES
            (@FirstName, @LastName, @EmailId, @MobileNo, @Designation, @CompanyId, @DepartmentId)";

            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query, contact);
            }
        }

        public void UpdateContact(ContactInfo contact)
        {
            string query = @"
            UPDATE ContactInfo
            SET FirstName=@FirstName,
                LastName=@LastName,
                EmailId=@EmailId,
                MobileNo=@MobileNo,
                Designation=@Designation,
                CompanyId=@CompanyId,
                DepartmentId=@DepartmentId
            WHERE ContactId=@ContactId";

            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query, contact);
            }
        }

        public void DeleteContact(int id)
        {
            string query = "DELETE FROM ContactInfo WHERE ContactId=@Id";

            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query, new { Id = id });
            }
        }

        public List<Company> GetCompanies()
        {
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<Company>("SELECT * FROM Company").ToList();
            }
        }

        public List<Department> GetDepartments()
        {
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<Department>("SELECT * FROM Department").ToList();
            }
        }
    }
}