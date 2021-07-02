using System;
using System.Collections.Generic;

namespace SalesData
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Sales Data <data csv fil path> <report file path>");
                Environment.Exit(1);
            }

            string csvFilePath = args[0];
            string reportFilePath = args[1];

            List<Sale> saleList = null;
            try
            {
                saleList = SaleLoader.Load(csvFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(2);
            }

            var report = SaleReport.GenerateText(saleList);

            try
            {
                System.IO.File.WriteAllText(reportFilePath, report);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(3);
            }


        }

    }
}
