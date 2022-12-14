using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;


namespace SalesWebMVC.Services
{
    public class SalesRecordsServices
    {
        private readonly AppDbContext _context;

        public SalesRecordsServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindBydateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var data = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                data = data.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                data = data.Where(x => x.Date <= maxDate.Value);
            }
            return await data
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }


        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }
    }
}