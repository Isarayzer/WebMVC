using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMVCContext _context;

        public DepartmentService(SalesWebMVCContext context)
        {
            _context = context;
        }

        //Metodo para retornar todos os departmententos
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        } 
    }
}
