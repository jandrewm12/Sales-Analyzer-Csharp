using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SalesData
{
    public static class SaleReport
    {
        public static string GenerateText(List<Sale> saleList)
        {
            string report = "* * * Sales Report * * *\n\n";

            if(saleList.Count() < 1)
            {
                report += "No data is available\n\n";

                return report;
            }

            //Q1: All items sold to customers in Australia (stockcode and description)--------------------------------------------------------------------------------------------------------------------------------

            var australiaItems = from Sale in saleList where Sale.Country == "Australia" select new { Sale.StockCode, Sale.Description};

            report += "Items sold to customers in Australia: \n";

            if (australiaItems.Count() > 0)
            {
                foreach(var sale in australiaItems)
                {
                    report += sale + "\n";
                }
                report += "\n";
            }
            else
            {
                report  += "No items sold to Australian customers. \n\n";
            }


            //Q2: How many individual sales were there?--------------------------------------------------------------------------------------------------------------------------------
            var invoiceNumbers = from Sale in saleList select Sale.InvoiceNo;
            ArrayList uniqueInvoices = new ArrayList();
            int saleNumber = 0;

            if(invoiceNumbers.Count() > 0)
            {
                foreach (var invoice in invoiceNumbers)
                {
                    if(uniqueInvoices.Contains(invoice))
                    {
                        continue;
                    }
                    else
                    {
                        uniqueInvoices.Add(invoice);
                    }
                }

                foreach (var invoice in uniqueInvoices)
                {
                    saleNumber++;
                }

                report += $"There were {saleNumber} individual sales.\n\n";
            }
            else
            {
                report += "There were no sales.";
            }
            

            //Q3: What is the sales total for invoice no. 536365? (quantity * unit price)--------------------------------------------------------------------------------------------------------------------------------
            double totalSales = 0;

            var quantityTimesPrice = from Sale in saleList where Sale.InvoiceNo == "536365" select ((int)Sale.Quantity*(double)Sale.UnitPrice);

            if(quantityTimesPrice.Count() > 0)
            {
                foreach(var sale in quantityTimesPrice)
                {
                    totalSales += sale;
                }
            report += $"The sales total for Invoice No. 536365 is {totalSales}.\n\n";
            }
            else
            {
                report += "Invoice no. 536365 does not exist/has no sales.";
            }


            //Q4: List the total sales by country.--------------------------------------------------------------------------------------------------------------------------------
            var countries = from Sale in saleList select Sale.Country;
            ArrayList individualCountries = new ArrayList();
            if(countries.Count() > 0)
            {
                foreach (var country in countries)
                {       
                    if(individualCountries.Contains(country))
                    {
                        continue;
                    }
                    else
                    {
                        individualCountries.Add(country);
                    }
                }

                foreach(var country in individualCountries)
                {
                    var totalSalesbyCountryIndividual = from Sale in saleList where Sale.Country == country.ToString() select ((int)Sale.Quantity*(double)Sale.UnitPrice);
                    double countrySalesTotal = 0;
                    foreach(var sales in totalSalesbyCountryIndividual)
                    {
                        countrySalesTotal += sales;
                    }
                    report += $"{country.ToString()} total sales: ${Math.Round(countrySalesTotal, 2)}\n";
                }
                report += "\n";
            }
            else
            {
                report += "There were no countries with sales.\n\n";
            }


            //Q5: Which customer has spent the most money during the period?--------------------------------------------------------------------------------------------------------------------------------
            var allcustomerIDs = from Sale in saleList select Sale.CustomerID;
            List<string> individualCustomerList = new List<string>();
            List<double> amountsSpentbyCustomers = new List<double>();
            double totalSpentCustomerID = 0;

            if(allcustomerIDs.Count() > 0)
            {
                

                foreach(var customer in allcustomerIDs)
                {
                    if(individualCustomerList.Contains(customer))
                    {
                        continue;
                    }
                    else
                    {       
                        if(customer == "")
                        {
                            continue;
                        }
                        else
                        {
                            individualCustomerList.Add(customer);
                        }
                    
                    }
                }

                foreach(var customer in individualCustomerList)
                {
                    var amountSpent = from Sale in saleList where Sale.CustomerID == customer select ((int)Sale.Quantity*(double)Sale.UnitPrice);
                    totalSpentCustomerID = 0;

                foreach(var amount in amountSpent)
                {
                    totalSpentCustomerID += amount;
                }

                    amountsSpentbyCustomers.Add(totalSpentCustomerID);
                }

                var maxAmountSpent = amountsSpentbyCustomers.Max();
                var indexofMaxAmount = amountsSpentbyCustomers.IndexOf(maxAmountSpent);

                var biggestSpender = individualCustomerList[indexofMaxAmount];

                report += $"CustomerID: {biggestSpender}. Amount spent: ${Math.Round(maxAmountSpent, 2)}\n\n";

            }
            else
            {
                report += "No customer IDs available.";
            }

            //Q6: What are the total sales to customer 15311?--------------------------------------------------------------------------------------------------------------------------------

            var customerAmountSpent15311 = from Sale in saleList where Sale.CustomerID == 15311.ToString() select ((int)Sale.Quantity*(double)Sale.UnitPrice);
            double customer15311dollars = 0;

            if(customerAmountSpent15311.Count() > 0)
            {
                foreach(var amount in customerAmountSpent15311)
                {
                    customer15311dollars += (double)amount;
                }

                report += $"Customer 15311 spent ${Math.Round(customer15311dollars, 2)}\n\n";
            }
            else
            {
                report += "No sales for customer 15311 / customer unavailable.";
            }

            

            //Q7: How many units of "HAND WARMER UNION JACK" were sold?--------------------------------------------------------------------------------------------------------------------------------
            var unionJackQuantities = from Sale in saleList where Sale.Description == "HAND WARMER UNION JACK" select Sale.Quantity;
            var unionJackQuantityTotal = 0;


            if(unionJackQuantities.Count() > 0)
            {
                
                foreach(var quantity in unionJackQuantities)
                {
                    unionJackQuantityTotal += quantity;
                }

                report += $"{unionJackQuantityTotal} units of 'HAND WARMER UNION JACK' were sold\n\n";
            }
            else
            {
                report += "No 'HAND WARMER UNION JACK' were sold.";
            }

            //Q8: What was the total value of the "HAND WARMER UNION JACK" sales?--------------------------------------------------------------------------------------------------------------------------------
            var unionJackValue = from Sale in saleList where Sale.Description == "HAND WARMER UNION JACK" select ((int)Sale.Quantity*(double)Sale.UnitPrice);
            double unionJackSalestotal = 0;

            if(unionJackValue.Count() > 0)
            {
                
                foreach(var sales in unionJackValue)
                {
                    unionJackSalestotal += sales;
                }

                report += $"Total value of 'HAND WARMER UNION JACK': ${Math.Round(unionJackSalestotal, 2)}\n\n";
            }
            else
            {
                report += "No 'HAND WARMER UNION JACK' sales available.";
            }


            //Q9: Which product has the highest unit price?-------------------------------------------------------------------------------------------------------------------------------

            var productsU = from Sale in saleList select Sale.Description;
            List<string> individualProducts = new List<string>();
            List<double> individualPrices = new List<double>();


            if(productsU.Count() > 0)
            {
                
                foreach(var product in productsU)
                {
                    if(individualProducts.Contains(product))
                    {
                        continue;
                    }
                    else
                    {
                        if(product == "")
                        {
                            continue;
                        }
                        else
                        {
                            individualProducts.Add(product);
                        }
                    }
                }

                foreach(var product in individualProducts)
                {
                    var productUnitPrices = from Sale in saleList where Sale.Description == product select Sale.UnitPrice;
                    var amountOfPricesCounter = 0;
                    double totalUnitPrices = 0;
                    double actualUnitPrice = 0;


                    foreach(var price in productUnitPrices)
                    {
                        totalUnitPrices += price;
                        amountOfPricesCounter++;
                    }
                    actualUnitPrice = (totalUnitPrices/amountOfPricesCounter);
                    individualPrices.Add(actualUnitPrice);
                }

                var maxUnitPrice = individualPrices.Max();
                var indexOfMaxUnitPrice = individualPrices.IndexOf(maxUnitPrice);

                var biggestUnitPrice = individualProducts[indexOfMaxUnitPrice];

                var stockCodeOfHighestUnitPrice = from Sale in saleList where Sale.Description == biggestUnitPrice select Sale.StockCode;
                var stockcode = "";

                foreach(var code in stockCodeOfHighestUnitPrice)
                {
                    stockcode = code;
                }

                report += $"Product with highest price: Stock Code: {stockcode}, Description: {biggestUnitPrice}\n\n";
            }
            else
            {
                report += "No products available.";
            }


            //Q10: total sales? --------------------------------------------------------------------------------------------------------------------------------
            var totalSalesDataset = from Sale in saleList select ((int)Sale.Quantity*(double)Sale.UnitPrice);
            double datasetSalestotal = 0;

            if(totalSalesDataset.Count() > 0)
            {

                foreach(var sale in totalSalesDataset)
                {
                    datasetSalestotal += sale;
                }

                report += $"Total sales in dataset: ${Math.Round(datasetSalestotal, 2)}\n\n";
            }
            else
            {
                report += "No sales available.";
            }

            //Q11: Which invoice had the highest sales?--------------------------------------------------------------------------------------------------------------------------------

            var invoices = from Sale in saleList select Sale.InvoiceNo;
            List<string> listOfInvoices = new List<string>();
            List<double> listOfAmountsSpentPerInvoice = new List<double>();
            double saleTotal;

            if(invoices.Count() > 0)
            {
                foreach(var invoice in invoices)
                {
                    if(listOfInvoices.Contains(invoice))
                    {
                        continue;
                    }
                    else
                    {
                        if(invoice == "")
                        {
                            continue;
                        }
                        else
                        {
                            listOfInvoices.Add(invoice);
                        }
                    }
                }

                foreach(var invoice in listOfInvoices)
                {
                    var salesforInvoice = from Sale in saleList where Sale.InvoiceNo == invoice select ((int)Sale.Quantity*(double)Sale.UnitPrice);
                    saleTotal = 0;

                    foreach(var sale in salesforInvoice)
                    {
                        saleTotal += sale;
                    }
                    listOfAmountsSpentPerInvoice.Add(saleTotal);
                }

                double maxInvoiceAmountSpent = listOfAmountsSpentPerInvoice.Max();
                int indexOfMaxInvoiceAmountSpent = listOfAmountsSpentPerInvoice.IndexOf(maxInvoiceAmountSpent);

                var biggestInvoiceAmountSpent = listOfInvoices[indexOfMaxInvoiceAmountSpent];

                report += $"Highest Invoice: {biggestInvoiceAmountSpent}, Total: ${Math.Round(maxInvoiceAmountSpent, 2)}\n\n";
            }
            else
            {
                report += "No invoices available.";
            }
            
            //Q12: Which product sold the most units? --------------------------------------------------------------------------------------------------------------------------------
            var products = from Sale in saleList select Sale.Description;
            List<string> listOfProducts = new List<string>();
            List<int> listOfQuantitiesSold = new List<int>();
            var unitsSold = 0;

            if(products.Count() > 0)
            {
                foreach(var product in products)
                {
                    if(listOfProducts.Contains(product))
                    {
                        continue;
                    }
                    else
                    {
                        if(product == "")
                        {
                            continue;
                        }
                        else
                        {
                            listOfProducts.Add(product);
                        }
                    }   
                }

                foreach(var product in listOfProducts)
                {
                    var amountsOfProductSold = from Sale in saleList where Sale.Description == product select Sale.Quantity;
                    unitsSold = 0;

                    foreach(var quantity in amountsOfProductSold)
                    {
                        unitsSold += quantity;
                    }

                    listOfQuantitiesSold.Add(unitsSold);            
                }

                var maxQuantitySold = listOfQuantitiesSold.Max();
                var indexofMaxQuantitySold = listOfQuantitiesSold.IndexOf(maxQuantitySold);

                var bestSeller = listOfProducts[indexofMaxQuantitySold];

                report += $"Product with most units sold: {bestSeller}, {maxQuantitySold} units.\n\n";
            }
            else
            {
                report += "No product information available.";
            }

            report += "* * * END OF REPORT * * *";
            return report;
        }
    }
}