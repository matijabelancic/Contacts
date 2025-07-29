using System.Data;
using API.Entities;
using API.Interfaces;
using API.Utilities;
using Microsoft.Data.SqlClient;

namespace API.Repositories;

public class ContactsRepo(string connectionString) : IContactsRepo
{
    public Contact? GetById(Guid id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand("dbo.GetContactById", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@Id", id);
        connection.Open();
        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Contact
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Surname = reader.GetString(reader.GetOrdinal("Surname")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                UpdatedAt = reader.IsDBNull(reader.GetOrdinal("UpdatedAt"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
            };
        }

        return null;
    }

    public PaginationResponse<Contact> GetAll(RequestParameters request)
    {
        var response = new PaginationResponse<Contact>
        {
            CurrentPage = request.PageNumber,
            PageSize = request.PageSize,
        };

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand("dbo.GetContactsPaged", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@PageNumber", request.PageNumber);
        command.Parameters.AddWithValue("@PageSize", request.PageSize);
        command.Parameters.AddWithValue("@SortColumn", request.SortBy);
        command.Parameters.AddWithValue("@SortDirection", request.SortByAscending ? 1 : 0);
        command.Parameters.AddWithValue("@SearchTerm", (object?)request.SearchTerm ?? DBNull.Value);

        connection.Open();
        using var reader = command.ExecuteReader();
        
        if (reader.Read())
        {
            response.TotalCount = reader.GetInt32(reader.GetOrdinal("TotalCount"));
            response.TotalPages = (int)Math.Ceiling(response.TotalCount / (double)request.PageSize);
        }
        
        if (reader.NextResult())
        {
            var items = new List<Contact>();
            while (reader.Read())
            {
                items.Add(new Contact
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Surname = reader.GetString(reader.GetOrdinal("Surname")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UpdatedAt = reader.IsDBNull(reader.GetOrdinal("UpdatedAt"))
                        ? null
                        : reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
                });
            }

            response.Items = items;
        }
        else
        {
            response.Items = new List<Contact>();
        }

        return response;
    }


    public void Create(Contact contact)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand("dbo.CreateContact", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@Id", contact.Id);
        command.Parameters.AddWithValue("@Name", contact.Name);
        command.Parameters.AddWithValue("@Surname", contact.Surname);
        command.Parameters.AddWithValue("@Email", contact.Email);
        command.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);
        command.Parameters.AddWithValue("@UpdatedAt", DBNull.Value);
       
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Update(Contact contact)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand("dbo.UpdateContact", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@Id", contact.Id);
        command.Parameters.AddWithValue("@Name", contact.Name);
        command.Parameters.AddWithValue("@Surname", contact.Surname);
        command.Parameters.AddWithValue("@Email", contact.Email);
        command.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(Guid id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand("dbo.DeleteContact", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        command.ExecuteNonQuery();
    }
}