using System.Data;


namespace HenriksHobbyLager.Interfaces;

public interface IDatabase
{
    Task<IDbConnection> GetConnectionAsync();
    string GetConnectionString();
}