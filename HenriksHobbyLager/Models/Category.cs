using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HenriksHobbyLager.Repository;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Facade;


namespace HenriksHobbyLager.Models
{
    public class Category
    {
        public int Id { get; set; } // Primärnyckel
        public string? Name { get; set; } // Namn på kategorin
    }
}
