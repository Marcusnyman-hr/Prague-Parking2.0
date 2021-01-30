using System;
using System.Collections.Generic;
using System.Text;

namespace PragueParking2._0
{
    public class Config
    {
        public Config(int parkingSpotsAmount, int parkingSpotSize, int bicycleSize, int mcSize, int carSize, int bicyclePrice, int mcPrice, int carPrice, List<dynamic> vehicleTypes)
        {
            this.VehicleTypes = vehicleTypes;
            this.ParkingSpotsAmount = parkingSpotsAmount;
            this.ParkingSpotSize = parkingSpotSize;
            this.BicycleSize = bicycleSize;
            this.McSize = mcSize;
            this.CarSize = carSize;
            this.BicyclePrice = bicyclePrice;
            this.McPrice = mcPrice;
            this.CarPrice = carPrice;
        }
        public List<dynamic> VehicleTypes { get; set; }
        public int ParkingSpotsAmount { get; set; }
        public int ParkingSpotSize { get; set; }
        public int BicycleSize { get; set; }
        public int McSize { get; set; }
        public int CarSize { get; set; }
        public int BicyclePrice { get; set; }
        public int McPrice { get; set; }
        public int CarPrice { get; set; }
    }
}
