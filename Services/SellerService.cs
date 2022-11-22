using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;
using SalesWebMVC.Data;
using SalesWebMVC.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly AppDbContext _context;

        public SellerService(AppDbContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {   
            // To list tem referencia ao system Linq
            return _context.Seller
            .Include(d => d.Department)
            .ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }




        public SellerFormViewModel GetDropdownValues()
        {
            var departments = _context.Department.OrderBy(x => x.Name).ToList();
            var data = new SellerFormViewModel(){
                Departments = departments
            };

            return data;
        }

    }
}