using SalesWebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

        //Metodo para achar o find all
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        //Metodo para inserir novos dados no banco de dados
        public void Insert(Seller obj)
        {
            //obj.Department = _context.Department.First();  //Se caso nao tem um departamento, inseri o primeiro sempre.
            _context.Add(obj);
            _context.SaveChanges();
        }

        //Metodo achar por ID
        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id); //Faz o join da tabela do banco.
        }

        //Metodo para remover por ID
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);//Primeiro acha por id
            _context.Seller.Remove(obj); //depois remove o id encontrado.
            _context.SaveChanges(); //Confirma a alteracao
        }
    }
}
