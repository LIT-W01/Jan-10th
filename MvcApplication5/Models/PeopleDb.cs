using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcApplication5.Models
{

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class PeopleDb
    {
        private readonly string _connectionString;

        public PeopleDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Customer> GetAll()
        {
            List<Customer> result = new List<Customer>();
            using (var sqlConnection = new SqlConnection(_connectionString))
            using (var cmd = sqlConnection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Customers";
                sqlConnection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Customer c = new Customer();
                    c.Id = (int)reader["Id"];
                    c.Name = (string)reader["Name"];
                    c.Address = (string)reader["Address"];
                    result.Add(c);
                }
            }

            return result;
        }

        public void Add(Customer c)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            using (var cmd = sqlConnection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Customers VALUES (@name, @address)";
                cmd.Parameters.AddWithValue("@name", c.Name);
                cmd.Parameters.AddWithValue("@address", c.Address);
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Customer FindById(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            using (var cmd = sqlConnection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Customers WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                sqlConnection.Open();
                var reader = cmd.ExecuteReader();
                reader.Read();
                Customer c = new Customer();
                c.Id = (int)reader["Id"];
                c.Name = (string)reader["Name"];
                c.Address = (string)reader["Address"];
                return c;
            }
        }

        public void Update(Customer c)
        {
             using (var sqlConnection = new SqlConnection(_connectionString))
             using (var cmd = sqlConnection.CreateCommand())
             {
                 cmd.CommandText = "UPDATE Customers SET name = @name, address = @address WHERE Id = @id";
                 cmd.Parameters.AddWithValue("@name", c.Name);
                 cmd.Parameters.AddWithValue("@address", c.Address);
                 cmd.Parameters.AddWithValue("@id", c.Id);
                 sqlConnection.Open();
                 cmd.ExecuteNonQuery();
             }
        }

        public void Delete(int id)
        {
             using (var sqlConnection = new SqlConnection(_connectionString))
             using (var cmd = sqlConnection.CreateCommand())
             {
                 cmd.CommandText = "DELETE FROM Customers WHERE Id = @id";
                 cmd.Parameters.AddWithValue("@id", id);
                 sqlConnection.Open();
                 cmd.ExecuteNonQuery();
             }
        }

    }
}