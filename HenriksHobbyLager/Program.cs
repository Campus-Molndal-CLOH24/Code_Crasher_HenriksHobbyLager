using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Sqlite;
using HenriksHobbyLager.Database;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using HenriksHobbyLager.UIs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using HenriksHobbyLager.Facade;
using HenriksHobbyLager.Repository;
using HenriksHobbyLager.Interfaces;


namespace HenriksHobbyLager
{
    static class Program
    {
        static void Main(string[] args)
        {
            // Load configuration
        var _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // Create the DatabaseFactory using the configuration
        var databaseFactory = new DatabaseFactory(_configuration);

        // Get repository through DatabaseMenu
        var databaseMenu = new DatabaseMenu(databaseFactory);
        var repository = databaseMenu.GetSelectedRepository();

        // Create facade with repository
        var productFacade = new ProductFacade(repository);

        // Instantiate ConsoleMenuHandler with dependencies
        var menuHandler = new ConsoleMenuHandler(_configuration, productFacade, databaseFactory);

        // Show the main menu
        menuHandler.ShowMainMenu();
        }
    }
}




