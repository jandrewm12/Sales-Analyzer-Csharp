using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SalesData
{
    public static class SaleLoader
    {
        private static int NumItemsInRow = 8;

        public static List<Sale> Load(string csvFilePath)
        {
            List<Sale> saleList = new List<Sale>();

            try
            {
                using (var reader = new StreamReader(csvFilePath))
                {
                    int lineNumber = 0;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        lineNumber++;
                        if(lineNumber == 1) continue;


                        Regex csvparser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                        var values = csvparser.Split(line);

                        if (values.Length != NumItemsInRow)
                        {
                            throw new Exception($"Row {lineNumber} contains {values.Length} values. It should contain {NumItemsInRow}.");
                        }
                        try
                        {
                            string invoiceno = (values[0].ToString());
                            string stockcode = (values[1].ToString());
                            string description = (values[2].ToString());
                            int quantity = Int32.Parse(values[3]);
                            string invoicedate = (values[4].ToString());
                            double unitprice;
                            Double.TryParse(values[5], out unitprice);
                            string customerid = (values[6].ToString());
                            string country = (values[7].ToString());
                            Sale sales = new Sale(invoiceno, stockcode, description, quantity, invoicedate, unitprice, customerid, country);
                            saleList.Add(sales);
                        }
                        catch (FormatException e)
                        {
                            throw new Exception($"Row {lineNumber} contains invalid data. ({e.Message})");
                        }
                        
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to open {csvFilePath} ({e.Message})");
            }

            return saleList;
        }
    }
}
