using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesWebMVC.Services.Exceptions;



namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        // Injeção de dependencia
        private readonly SellerService _sellerService;


        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;

        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        //Get Create
        public IActionResult Create()
        {
            var dataDropDownList = _sellerService.GetDropdownValues();
            // using Microsoft.AspNetCore.Mvc.Rendering; para chamar o selectlist
            ViewBag.Departments = new SelectList(dataDropDownList.Departments, "Id", "Name");
            return View();
        }

        //Get Create
        //public IActionResult Create()
        //{
        //var departments = _departmentService.FindAll();
        //var viewModel = new SellerFormViewModel();
        //viewModel.Departments = departments;
        //return View(viewModel);
        //}

        // Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            // Não e necessario modificar o insert para referenciar outras classes
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        // Get Delete
        public IActionResult Delete(int? id)
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

            return View(obj);
        }

        // Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        // Get Detals
        public IActionResult Details(int? id)
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

            return View(obj);
        }

        //Get Edit
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

            var data = new SellerFormViewModel()
            {
                Seller = obj,
            };
            var dataDropDownList = _sellerService.GetDropdownValues();
            ViewBag.Departments = new SelectList(dataDropDownList.Departments, "Id", "Name");

            return View(data);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
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