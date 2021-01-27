using System;
using System.Collections.Generic;
using System.Text;

namespace PragueParking2._0
{
    public class ParkingSpot
    {
        public ParkingSpot(int parkingSpotNumber, int size)
        {
            this.FreeSpace = size;
            this.ParkingSpotNumber = parkingSpotNumber;
        }
        public void UseParking(int vehicleSize, Vehicle vehicle) 
        {
            if(this.FreeSpace >= vehicleSize)
            {
                this.FreeSpace = this.FreeSpace - vehicleSize;
                ParkedVehiclesOnSpot.Add(vehicle);
            }
        }
        public List<Vehicle> ListParkedVehicles()
        {
            return ParkedVehiclesOnSpot;
        }
        public int FreeSpace { get; set; }
        public int ParkingSpotNumber { get; set; }
        public List<Vehicle> ParkedVehiclesOnSpot = new List<Vehicle>();
    }
}
