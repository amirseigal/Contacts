using MyContacts.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace MyContacts
{
    internal class ContactsRepository : IContactsRepository
    {
        string connectionString = "Data Source = .; Initial Catalog = Contact_DB; Integrated Security = true";
        
        public bool Delete(int contactID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                string query = "DELETE FROM Contacts WHERE ContactID = @contactID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("contactID", contactID);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            
        }

        public bool Insert(string firstName, string lastName, string phoneNumber, string email, int age, string address)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                string query = "INSERT INTO Contacts (FirstName, LastName, PhoneNumber, Email, Age, Address) VALUES (@firstName, @lastName, @phoneNumber, @email, @age, @address)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@age", age);
                command.Parameters.AddWithValue("@address", address);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {

                connection.Close();
            }
        }

        public DataTable Search(string parameter)
        {
            string query = "SELECT * FROM Contacts WHERE FirstName LIKE @parameter OR LastName LIKE @parameter OR PhoneNumber LIKE @parameter";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.SelectCommand.Parameters.AddWithValue("@parameter", "%" +  parameter + "%");
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        public DataTable SelectAll()
        {
            string query = "SELECT * FROM Contacts";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query , connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        public DataTable SelectRow(int contactID)
        {
            string query = "SELECT * FROM Contacts WHERE ContactID = " + contactID;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        public bool Update(int contactID, string firstName, string lastName, string phoneNumber, string email, int age, string address)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                string query = "UPDATE Contacts SET FirstName = @firstName, LastName = @lastName, PhoneNumber = @phoneNumber, Email = @Email, Age = @age, Address = @address WHERE ContactID = " + contactID;
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@age", age);
                command.Parameters.AddWithValue("@address", address);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}