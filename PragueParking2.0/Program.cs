using Newtonsoft.Json;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PragueParking2._0
{

    class Program
    {
        //initiate parkinglot
        public static List<ParkingSpot> ParkingLot = new List<ParkingSpot>();
        static void Main(string[] args)
        {
            //Load config file
            Configuration config = LoadConfigFile();
            //Create the accurate amount of parkingspots
            for (int i = 0; i < config.ParkingSpotsAmount; i++ )
            {
                ParkingLot.Add(new ParkingSpot(i, config.ParkingSpotSize));
            }
            //Load storagefile with stored vehicles
            LoadVehicles(config);
            //Main menu
            bool menuIsActive = true;
            while (menuIsActive)
            {
                Console.Clear();
                var appHeading = new Rule("PRAGUE PARKING 2.0");
                AnsiConsole.Render(appHeading);
                var menuOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What do you want to do?")
                        .PageSize(10)
                        .AddChoices(new[] {
                            "Park vehicle", 
                            "Repark vehicle",
                            "Depark Vehicle",  
                            "Load config",
                            "Show map",
                            "Show pricelist",
                            "Load vehicles",
                            "Save vehicles",
                            "temp list",
                            "test write"
                        }));
                switch(menuOption)
                {
                    case "Park vehicle":
                        ParkVehicle(config);
                        break;
                    case "Repark vehicle":
                        Console.WriteLine("Reparking vehicle");
                        DateTime now = DateTime.Now;
                        Console.WriteLine(now);
                        Console.ReadLine();
                        break;
                    case "Depark Vehicle":
                        Console.WriteLine("Deparking vehicle");
                        DeparkVehicle(new Car("asdsssss", "Nisse persson", "volvo", "740", 2, new DateTime(2021, 01, 27, 16, 25, 00)));
                        Console.ReadLine();
                        break;
                    case "Load config":
                        Console.WriteLine("Loading config");
                        config = LoadConfigFile();
                        Console.WriteLine(ParkingLot.Count);
                        Console.ReadLine();
                        break;
                    case "Show map":
                        ShowMap();
                        break;
                    case "Show pricelist":
                        PriceList priceList = new PriceList();
                        priceList.GetPriceListFile();
                        break;
                    case "Load vehicles":
                        LoadVehicles(config);
                        break;
                    case "Save vehicles":
                        SaveVehicles();
                        break;
                    case "test write":
                        WriteToStorage();
                        break;
                    case "temp list":
                        
                        foreach(ParkingSpot pspot in ParkingLot)
                        {
                            if (pspot.FreeSpace != 4)
                            {
                                foreach (Vehicle vehicle in pspot.ListParkedVehicles())
                                {
                                    if (vehicle is Bicycle)
                                    {
                                        Console.WriteLine($"{vehicle.Owner} has a bicycle stored at: {pspot.ParkingSpotNumber} since {vehicle.ParkedSince}");
                                    }
                                    if(vehicle is Mc)
                                    {
                                        Console.WriteLine($"{vehicle.Owner} has a mc with {vehicle.RegistrationNumber} stored at: {pspot.ParkingSpotNumber} since {vehicle.ParkedSince}");
                                    }
                                    if(vehicle is Car)
                                    {
                                        Console.WriteLine($"{vehicle.Owner} has a car with {vehicle.RegistrationNumber} stored at: {pspot.ParkingSpotNumber} since {vehicle.ParkedSince}");
                                    }
                                }
                            }
                        }
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Faulty choice");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public static void SaveVehicles()
        {
            foreach (ParkingSpot pspot in ParkingLot)
            {
                if (pspot.FreeSpace != 4)
                {
                    //string jsonData = JsonConvert.SerializeObject(pspot);
                    foreach (Vehicle vehicle in pspot.ListParkedVehicles())
                    {
                        if (vehicle is Bicycle)
                        {
                            string jsonData2 = JsonConvert.SerializeObject(ParkingLot);
                            Console.WriteLine(jsonData2);
                            string jsonData = JsonConvert.SerializeObject(vehicle);
                            Console.WriteLine($"{vehicle.Owner} has a bicycle stored at: {pspot.ParkingSpotNumber}");
                            Console.WriteLine($"Json {jsonData}");
                            Bicycle newBike = JsonConvert.DeserializeObject<Bicycle>(jsonData);
                            Console.WriteLine(newBike);
                        }
                        if (vehicle is Mc)
                        {
                            Console.WriteLine($"{vehicle.Owner} has a mc with {vehicle.RegistrationNumber} stored at: {pspot.ParkingSpotNumber}");
                        }
                        if (vehicle is Car)
                        {
                            Console.WriteLine($"{vehicle.Owner} has a car with {vehicle.RegistrationNumber} stored at: {pspot.ParkingSpotNumber}");
                        }
                    }
                }
            }
            Console.ReadLine();
        }
        public static void WriteToStorage()
        {
            string vehicleJsonData = JsonConvert.SerializeObject(ParkingLot);
            string filePath = @"C:\vehiclestorage.json";
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(vehicleJsonData);
            sw.Close();
        }

        public static string validateRegInfo(string regInfo)
        {
            if (regInfo.Length < 7)
            {
                Console.WriteLine("Your registration info should contain atleast 7 characters");
                return "error";
            }
            else if (regInfo.Length > 10)
            {
                Console.WriteLine("Your registration info should contain Maximum 10 characters");
                return "error";
            }
            else if (!Regex.IsMatch(regInfo, "^[a-zA-Z0-9À-ž]*$"))
            {
                Console.WriteLine("Your registration info should only contain letters and numbers");
                return "error";
            }
            else
            {
                return regInfo;
            }
        }
        public static void LoadVehicles(Configuration config)
        {
            using (StreamReader reader = new StreamReader(@"C:\vehiclestorage.json"))
            {
                string json = reader.ReadToEnd();
                dynamic dynJson = JsonConvert.DeserializeObject(json);
                
                for(int i = 0; i < dynJson.Count; i++)
                {
                    if (dynJson[i].ParkedVehiclesOnSpot.Count > 0)
                    {
                        foreach(var vehicle in dynJson[i].ParkedVehiclesOnSpot)
                        {
                            if(vehicle.identifier == "bicycle")
                            {
                                string owner = vehicle.Owner;
                                string color = vehicle.Color;
                                int parkedAt = vehicle.ParkedAt;
                                DateTime parkedSince = vehicle.ParkedSince;
                                string token = vehicle.Token;
                                ParkBicycle(new Bicycle(owner, color, parkedAt, parkedSince, token), parkedAt, config);
                            }
                            if(vehicle.identifier == "mc")
                            {
                                string brand = vehicle.Brand;
                                string model = vehicle.Model;
                                string registrationNumber = vehicle.RegistrationNumber;
                                string owner = vehicle.Owner;
                                int parkedAt = vehicle.ParkedAt;
                                DateTime parkedSince = vehicle.ParkedSince;
                                string token = vehicle.Token;
                                ParkMc(new Mc(registrationNumber, owner, brand, model,  parkedAt, parkedSince, token), parkedAt, config);
                            }
                            if (vehicle.identifier == "car")
                            {
                                string brand = vehicle.Brand;
                                string model = vehicle.Model;
                                string registrationNumber = vehicle.RegistrationNumber;
                                string owner = vehicle.Owner;
                                int parkedAt = vehicle.ParkedAt;
                                DateTime parkedSince = vehicle.ParkedSince;
                                string token = vehicle.Token;
                                ParkCar(new Car(registrationNumber, owner, brand, model, parkedAt, parkedSince, token), parkedAt, config);
                            }
                        }
                    }
                        
                }
                reader.Close();
                Console.WriteLine("Storage loaded...");
            }
        }
        public static float DeparkVehicle(Vehicle vehicle)
        {
            foreach(ParkingSpot pspot in ParkingLot)
            {
                foreach(Vehicle testVehicle in pspot.ListParkedVehicles())
                {
                    if(testVehicle.Owner == "Nisse")
                    {
                        Console.WriteLine(testVehicle);
                    }
                }
            }
            //DateTime now = DateTime.Now;
            //DateTime parkedSince = vehicle.ParkedSince;
            //var expiredMinutes = (now - parkedSince).TotalMinutes;
            //Console.WriteLine(expiredMinutes);
            Console.ReadKey();
            return 23.23f;
        }
        public static Configuration LoadConfigFile()
        {
            using (StreamReader reader = new StreamReader(@"C:\configuration.json"))
            {
                string json = reader.ReadToEnd();
                Configuration config = JsonConvert.DeserializeObject<Configuration>(json);
                return config;
            }
        }
        public static void ParkVehicle(Configuration config)
        {
            //RENDER MENU
            var menuOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What type of vehicle?")
                        .PageSize(10)
                        .AddChoices(new[] {
                            "Bicycle",
                            "Mc",
                            "Car",
                            "Bus"
                        }));
            if(menuOption == "Bicycle")
            {
                Console.WriteLine("Whats the owners name?");
                string bikeOwner = Console.ReadLine();
                Console.WriteLine("Whats the color of the bike?");
                string bikeColor = Console.ReadLine();
                int bikeSpot = findFreeSpot("bicycle", config);
                ParkBicycle(new Bicycle(bikeOwner, bikeColor, bikeSpot, DateTime.Now), bikeSpot, config);

                Console.WriteLine($"Successfully parked the bicycle at: {bikeSpot +1}"); 
                Console.ReadKey();
            } else
            {
                Console.WriteLine("Please enter the registration number:");
                string registrationNumber = validateRegInfo(Console.ReadLine());
                if (registrationNumber == "error") {
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine(registrationNumber);
                Console.WriteLine("Whats the owners name?");
                string owner = Console.ReadLine();
                Console.WriteLine("What brand is the vehicle?");
                string brand = Console.ReadLine();
                Console.WriteLine("What model is the vehicle?");
                string model = Console.ReadLine();

                switch(menuOption)
                {
                    case "Mc":
                        int mcSpot = findFreeSpot("mc", config);
                        Console.WriteLine(mcSpot);
                        ParkMc(new Mc(registrationNumber, owner, brand, model, mcSpot, DateTime.Now), mcSpot, config);
                        Console.ReadLine();
                        break;
                    case "Car":
                        int carSpot = findFreeSpot("car", config);
                        ParkCar(new Car(registrationNumber, owner, brand, model, carSpot, DateTime.Now), carSpot, config);
                        Console.ReadLine();
                        break;
                }
            }
            WriteToStorage();
        }
        public static int ParkBicycle(Bicycle bicycle, int spot, Configuration config)
        {
            ParkingLot[spot].UseParking(config.BicycleSize, bicycle);
            return spot +1;
        }
        public static int ParkCar(Car car, int spot, Configuration config)
        {
            ParkingLot[spot].UseParking(config.CarSize, car);
            return spot + 1;
        }
        public static int ParkMc(Mc mc, int spot, Configuration config)
        {
            ParkingLot[spot].UseParking(config.McSize, mc);
            return spot + 1;
        }
        public static int findFreeSpot(string vehicleType, Configuration config)
        {
            
            int size = 0;
            if(vehicleType == "bicycle")
            {
                size = config.BicycleSize;
            }
            if(vehicleType == "mc")
            {
                size = 2;
            }
            if(vehicleType == "car")
            {
                size = 4;
            }
            if(vehicleType == "bus")
            {
                size = 16;
            }
            ParkingSpot res = ParkingLot.Find(spot => spot.FreeSpace >= size);
            if (res == null)
            {
                return 999;
            } else
            {
                return res.ParkingSpotNumber;
            }
        }
        public static string SerializeVehicle(object vehicle)
        {
            string json = JsonConvert.SerializeObject(vehicle);
            return $"{json}";
        }
        public static void ShowMap()
        {
            Console.Clear();
            var heading = new Rule("Parkinglot map");
            AnsiConsole.Render(heading);
            Console.WriteLine("Showing map");
        }
        public static void LoadPriceList()
        {
            string filePath = @"C:\pricelist.txt";
            List<string> priceList = File.ReadAllLines(filePath).ToList();

            foreach(string instance in priceList)
            {
                Console.WriteLine(instance);
            }
            Console.ReadLine();
        }
        

    }
}
