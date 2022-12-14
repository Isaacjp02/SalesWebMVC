using System.ComponentModel.DataAnnotations;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage ="{0} size shold be between {2} and {1}")]
        public string Name { get; set; }
        [Required(ErrorMessage ="{0} required")]
        [EmailAddress(ErrorMessage ="Esse {0} não e valido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage ="{0} required")]
        [Display(Name ="Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage ="{0} required")]
        [Range(100.0, 50000.0, ErrorMessage ="{0} must be from {1} to {2}")]
        [Display(Name ="Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double BaseSalary { get; set; }

        //Relationships Many to Any
        public Department Department { get; set; }
        public int DepartmentId { get; set; }

        // Relationships One to Many
        public List<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        // Construtores

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department, int departmentId)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
            DepartmentId = departmentId;
            
        }

        // Metodos

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        // Calcular total de vendas
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr =>sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }

    }
}