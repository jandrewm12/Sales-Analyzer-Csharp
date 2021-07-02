using System;

namespace SalesData
{
    public class Sale
    {
        public string InvoiceNo;
        public string StockCode;
        public string Description;
        public int Quantity;
        public string InvoiceDate;
        public double UnitPrice;
        public string CustomerID;
        public string Country;

        public Sale(string invoiceno, string stockcode, string description, int quantity, string invoicedate, double unitprice, string customerid, string country)
        {
            InvoiceNo = invoiceno;
            StockCode = stockcode;
            Description = description;
            Quantity = quantity;
            InvoiceDate = invoicedate;
            UnitPrice = unitprice;
            CustomerID = customerid;
            Country = country;
        }

    
    }
}