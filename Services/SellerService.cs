using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;
using SalesWebMVC.Data;

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
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            // Adicionando o primeiro ID do departamento para tratar erro de chave estrngeira
            obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}