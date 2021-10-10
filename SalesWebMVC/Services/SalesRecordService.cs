using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMVCContext _context;

        public SalesRecordService(SalesWebMVCContext context)
        {
            _context = context;
        }

        //Funcao para achar por data
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            //Construindo o objeto
            var result = from obj in _context.SalesRecords select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return await result
                .Include(x => x.Seller) //faz o join das tabelas
                .Include(x => x.Seller.Department) //faz o join das tabelas
                .OrderByDescending(x => x.Date) //Ordena por data
                .ToListAsync();
        }

        //Funcao para achar por grupo
        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupAsync(DateTime? minDate, DateTime? maxDate)
        {
            //Construindo o objeto
            var result = from obj in _context.SalesRecords select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return await result
                .Include(x => x.Seller) //faz o join das tabelas
                .Include(x => x.Seller.Department) //faz o join das tabelas
                .OrderByDescending(x => x.Date) //Ordena por data
                .GroupBy(x => x.Seller.Department) //Agrupando por departamento
                .ToListAsync();
        }
    }
}
