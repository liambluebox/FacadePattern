using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    class item {
        public string Serial { get; set; }
        public string ModelNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Price { get; set; }
        public string Photo { get; set; }
        public bool IsAvailable { get; set; }
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
                            Serial = "123456",
                            IsAvailable = true
                        }
                    };
                    break;
                case "New York":
                    inventory = new List<item> {
                        new item{
                            Serial = "654321",
                            IsAvailable = true
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
        public bool ItemIsAvailable(item item)
        {
            Console.WriteLine("Checking serial number for availability: " + item.Serial);
            return item.IsAvailable;
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
            List<item> inventory = new List<item>{};
            string result = "Results:\n";
            inventory.AddRange(inv.GetInventoryForLocation("Dallas"));
            inventory.AddRange(inv.GetInventoryForLocation("New York"));
            foreach (item item in inventory) {
                if (!inv.ItemIsAvailable(item)){
                    continue;
                }
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
            // Without Facade
            InventorySubSystem inv = new InventorySubSystem();
            MarketingSubSystem mkting = new MarketingSubSystem();
            List<item> inventory = new List<item>{};
            string result = "Results:\n";
            inventory.AddRange(inv.GetInventoryForLocation("Dallas"));
            inventory.AddRange(inv.GetInventoryForLocation("New York"));
            foreach (item item in inventory) {
                if (!inv.ItemIsAvailable(item)){
                    continue;
                }
                item.ModelNumber = inv.GetModelNumberForSerialNumber(item.Serial);
                item.Photo = mkting.GetPhotoForModelNumber(item.ModelNumber);
                item.Price = mkting.GetPriceForModelNumber(item.ModelNumber);
                result += item.ModelNumber + ", " + item.Photo + ", $" + item.Price + "\n";
            }
            Console.WriteLine(result);

            // With Facade
            Facade facade = new Facade();
            facade.printAvailableProducts();

        }
    }
}
/* Output
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
