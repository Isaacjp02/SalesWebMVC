using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models.Enums;

namespace SalesWebMVC.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }

        //Relatioships Enum
        public SaleStatus Status { get; set; }

        //Many to Any
        public Seller Seller { get; set; }
        public int SellerId { get; set; }

        public SalesRecord()
        {
        }

        public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller, int sellerId)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
            SellerId = sellerId;
        }


    }
}