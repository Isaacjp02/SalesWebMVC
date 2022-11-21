using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
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

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department, int departmentId, List<SalesRecord> sales)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
            DepartmentId = departmentId;
            Sales = sales;
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