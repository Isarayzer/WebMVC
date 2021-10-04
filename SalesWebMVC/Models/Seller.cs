using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)] //Notation para trasformar o email em um link
        public string Email { get; set; }

        [Display(Name = "Birth Date")]//Notation para alterar o cabeçalho da tela do sellers
        [DataType(DataType.Date)] //Notation para ignorar a hora
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] //Notation para inverter a data para o padrao brasileiro        
        public DateTime BirthDate { get; set; }

        [Display(Name = "Base Salary")]//Notation para alterar o cabeçalho da tela do sellers
        [DisplayFormat(DataFormatString = "{0:F2}")]//Notation para separa as casas decimais
        public double BaseSalary { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; } //Garante que o ID deve existir para nao ficar null.
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>(); //Inicializando a lista

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        //Adicionando as vendas
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        //Removendo a venda
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        //Total de vendas por periodo
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
