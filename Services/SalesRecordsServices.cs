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
    }
}