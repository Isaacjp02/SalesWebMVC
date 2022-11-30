using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesWebMVC.Services.Exceptions;
using System.Diagnostics;



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

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        //Get Create
        public async Task<IActionResult> Create()
        {
            var dataDropDownList = await _sellerService.GetDropdownValuesAsync();
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
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var dataDropDownList = await _sellerService.GetDropdownValuesAsync();
                ViewBag.Departments = new SelectList(dataDropDownList.Departments, "Id", "Name");
                var viewModel = new SellerFormViewModel()
                {
                    Seller = seller,
                    Departments = dataDropDownList.Departments

                };

                return View(viewModel);

            }

            // Não e necessario modificar o insert para referenciar outras classes
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        // Get Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        // Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {

                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // Get Detals
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" }); ;
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        //Get Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            var dataDropDownList = await _sellerService.GetDropdownValuesAsync();
            ViewBag.Departments = new SelectList(dataDropDownList.Departments, "Id", "Name");
            var data = new SellerFormViewModel()
            {
                Seller = obj,
                Departments = dataDropDownList.Departments
            };


            return View(data);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {

            if (!ModelState.IsValid)
            {
                var dataDropDownList = await _sellerService.GetDropdownValuesAsync();
                ViewBag.Departments = new SelectList(dataDropDownList.Departments, "Id", "Name");
                var viewModel = new SellerFormViewModel()
                {
                    Seller = seller,
                    Departments = dataDropDownList.Departments

                };

                return View(viewModel);

            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {

                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        // Message Error
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

    }
}