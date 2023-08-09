using Microsoft.Data.SqlClient;
using PersonManagement.Models;
using System.Data;

namespace PersonManagement.DAL
{
    public class PersonDAL
    {
        private readonly string connectionString;

        public PersonDAL(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Person> GetAll()
        {
            List<Person> personlist = new List<Person>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("PersonList", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Person person = new Person();
                    person.Id = Convert.ToInt32(reader["Id"]);
                    person.FirstName = reader["FirstName"].ToString();
                    person.LastName = reader["LastName"].ToString();
                    person.DOB = Convert.ToDateTime(reader["DOB"]).Date;
                    person.Address = reader["Address"].ToString();
                    person.Email = reader["Email"].ToString();
                    personlist.Add(person);
                }

            }
            return personlist;
        }

        public Person GetPersonById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("GetPersonById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Person person = new Person();
                    person.Id = Convert.ToInt32(reader["Id"]);
                    person.FirstName = reader["FirstName"].ToString();
                    person.LastName = reader["LastName"].ToString();
                    person.DOB = Convert.ToDateTime(reader["DOB"]).Date;
                    person.Address = reader["Address"].ToString();
                    person.Email = reader["Email"].ToString();
                    return person;
                }

                return null;
            }
        }

        public Person GetPersonByEmail(string Email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("GetPersonByEmail", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", Email);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Person person = new Person();
                    person.Id = Convert.ToInt32(reader["Id"]);
                    person.FirstName = reader["FirstName"].ToString();
                    person.LastName = reader["LastName"].ToString();
                    person.DOB = Convert.ToDateTime(reader["DOB"]).Date;
                    person.Address = reader["Address"].ToString();
                    person.Email = reader["Email"].ToString();
                    return person;
                }

                return null;
            }
        }

        public bool Insert(Person model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("PersonSave", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", model.FirstName);
                command.Parameters.AddWithValue("@LastName", model.LastName);
                command.Parameters.AddWithValue("@DOB", model.DOB.Date);
                command.Parameters.AddWithValue("@Address", model.Address);
                command.Parameters.AddWithValue("@Email", model.Email);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool Update(Person model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("PersonUpdate", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", model.Id);
                command.Parameters.AddWithValue("@FirstName", model.FirstName);
                command.Parameters.AddWithValue("@LastName", model.LastName);
                command.Parameters.AddWithValue("@DOB", model.DOB.Date);
                command.Parameters.AddWithValue("@Address", model.Address);
                command.Parameters.AddWithValue("@Email", model.Email);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("PersonDelete", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
