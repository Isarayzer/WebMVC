using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>(); //Inicializando a lista

        public Department()
        {
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        //Adicionando um vendedor
        public void AddSeler(Seller seller)
        {
            Sellers.Add(seller);
        }

        //Total de vendas por um periodo
        public double TotalSales(DateTime inicial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(inicial, final));
        } 
    }
}
