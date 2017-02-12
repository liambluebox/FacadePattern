using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    class item {
        public string Serial { get; set; }
        public string ModelNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Price { get; set; }
        public string Photo {get; set; }
    }
    class InventorySubSystem
    {
        public List<item> GetInventoryForLocation(string location)
        {
            Console.WriteLine("Getting inventory for: " + location);
            List<item> inventory = new List<item>{};
            switch(location)
            {
                case "Dallas":
                    inventory = new List<item> {
                        new item{
                            Serial = "123456"
                        }
                    };
                    break;
                case "New York":
                    inventory = new List<item> {
                        new item{
                            Serial = "654321"
                        }
                    };
                    break;
            }
            return inventory;
        }
        public string GetModelNumberForSerialNumber(string serialNumber)
        {
            Console.WriteLine("Getting model number for: " + serialNumber);
            switch(serialNumber)
            {
                case "123456":
                    return "DAL-123";
                case "654321":
                    return "NYC-123";
            }
            return "";
        }
    }

    class MarketingSubSystem
    {
        public string GetPhotoForModelNumber(string modelNumber)
        {
            Console.WriteLine("Getting photo for: " + modelNumber);
            return "somePhoto";
        }
        public double GetPriceForModelNumber(string modelNumber)
        {
            return 9.99;
        }
    }

    class Facade
    {
        private InventorySubSystem inv;
        private MarketingSubSystem mkting;

        public Facade()
        {
            inv = new InventorySubSystem();
            mkting = new MarketingSubSystem();
        }

        public void printAvailableProducts()
        {
            List<item> availableInventory = new List<item>{};
            string result = "Results:\n";
            Console.WriteLine("\nFacade ---- ");
            availableInventory.AddRange(inv.GetInventoryForLocation("Dallas"));
            availableInventory.AddRange(inv.GetInventoryForLocation("New York"));
            foreach (item item in availableInventory) {
                item.ModelNumber = inv.GetModelNumberForSerialNumber(item.Serial);
                item.Photo = mkting.GetPhotoForModelNumber(item.ModelNumber);
                item.Price = mkting.GetPriceForModelNumber(item.ModelNumber);
                result += item.ModelNumber + ", " + item.Photo + ", $" + item.Price + "\n";
            }
            Console.WriteLine(result);
        }
    }
    // Program to get available products
    public class Program
    {
        public static void Main(string[] args)
        {
            Facade facade = new Facade();

            // Without Facade
            // Query inventory from Dallas server
            // Query inventory from New York server
            // Match serial numbers to model numbers
            // Get photos for evey model number
            // Print photo, model #, and cost

            // With Facade
            facade.printAvailableProducts();

        }
    }
}
/* Output
Facade ---- 
Getting inventory for: Dallas
Getting inventory for: New York
Getting model number for: 123456
Getting photo for: DAL-123
Getting model number for: 654321
Getting photo for: NYC-123
Results:
DAL-123, somePhoto, $9.99
NYC-123, somePhoto, $9.99
*/
