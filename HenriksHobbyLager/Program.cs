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


namespace HenriksHobbyLager
{
    static class Program
    {
        static void Main(string[] args)
        {
            ConsoleMenuHandler.ShowMainMenu();
        }
    }
}




