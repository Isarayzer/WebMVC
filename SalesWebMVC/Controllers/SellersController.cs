using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Services;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll(); //Retorna a lista do banco.
            return View(list);
        }

        //Metodo para criar/cadastrar o vendendor
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();//Procura no banco de dados todos os vendedores cadastrados.
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]  //Notação que o metodo é um POST
        [ValidateAntiForgeryToken]  //Notação de seguranca quando recebe o token
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); //Redireciona a pagina para o index novamente.
        }
    }
}
