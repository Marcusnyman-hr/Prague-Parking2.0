using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PragueParking2._0
{
    public class PriceList
    {

        public int BicyclePrice { get; set; }
        public int McPrice { get; set; }
        public int CarPrice { get; set; }
        public void GetPriceListFile()
        {
            string filePath = @"C:\pricelist.txt";
            List<string> lines = File.ReadAllLines(filePath).ToList();
            Console.WriteLine("loaded file");
            foreach (var line in lines)
            {
                string[] entries = line.Split(':');
                if(entries[0].Equals("BicyclePrice"))
                {
                    this.BicyclePrice = int.Parse(entries[1]);
                }
                if (entries[0].Equals("McPrice"))
                {
                    this.McPrice= int.Parse(entries[1]);
                }
                if (entries[0].Equals("CarPrice"))
                {
                    this.CarPrice = int.Parse(entries[1]);
                }
            }
            Console.WriteLine($"Bike: {BicyclePrice}CZK");
            Console.WriteLine($"Mc: {McPrice}CZK");
            Console.WriteLine($"Car: {CarPrice}CZK");
            Console.ReadLine();
        }
    }


}
