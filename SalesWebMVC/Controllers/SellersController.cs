using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Services;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services.Exceptions;

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

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);  //Busca os dados no banco de dados.

            if (obj == null)
            {
                return NotFound();
            }

            //Se os dados foram encontrados retorna os valores
            return View(obj);
        }

        //Criando a ação deletar usando o POST
        [HttpPost]  //Notação que o metodo é um POST
        [ValidateAntiForgeryToken]  //Notação de seguranca quando recebe o token
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        //Metodo para o detalhe
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);  //Busca os dados no banco de dados.

            if (obj == null)
            {
                return NotFound();
            }

            //Se os dados foram encontrados retorna os valores
            return View(obj);
        }

        //Metodo para editar os dados do vendedor.
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        //Criando a ação editar usando o POST
        [HttpPost]  //Notação que o metodo é um POST
        [ValidateAntiForgeryToken]  //Notação de seguranca quando recebe o token
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return BadRequest();
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}
