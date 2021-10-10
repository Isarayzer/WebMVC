using SalesWebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Services.Exceptions;

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
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        //Metodo para inserir novos dados no banco de dados
        public async Task InsertAsync(Seller obj)
        {
            //obj.Department = _context.Department.First();  //Se caso nao tem um departamento, inseri o primeiro sempre.
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        //Metodo achar por ID
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id); //Faz o join da tabela do banco.
        }

        //Metodo para remover por ID
        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);//Primeiro acha por id
            _context.Seller.Remove(obj); //depois remove o id encontrado.
           await _context.SaveChangesAsync(); //Confirma a alteracao
        }

        //Metodo para fazer o update dos dados no banco
        public async Task UpdateAsync(Seller obj)
        {
            //Verfica se o ID existe no banco
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            
            if (!hasAny)
            {
                throw new NotFoundException(obj.Id + "Not found");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();//salva no banco.
            }
            catch (DbUpdateConcurrencyException e) //se caso der erro nos dados do banco quando for salvar.
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
