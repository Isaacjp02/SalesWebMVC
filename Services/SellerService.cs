using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;
using SalesWebMVC.Data;
using SalesWebMVC.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly AppDbContext _context;

        public SellerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            // To list tem referencia ao system Linq
            return await _context.Seller
            .Include(d => d.Department)
            .ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            //Pegar o primeiro reultado que for igual a comparação
            // Eager loading
            return await _context.Seller
            .Include(d => d.Department)
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {

                throw new DbConcurrencyException(e.Message);
            }

        }

        // Metodo para selecionar Dropsows de classes relacionadas
        public async Task<SellerFormViewModel> GetDropdownValuesAsync()
        {
            var departments = await _context.Department.OrderBy(x => x.Name).ToListAsync();
            var data = new SellerFormViewModel()
            {
                Departments = departments
            };

            return data;
        }

    }
}