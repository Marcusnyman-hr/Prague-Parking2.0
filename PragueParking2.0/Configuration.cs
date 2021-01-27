using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PragueParking2._0
{
    public class Configuration
    {
        public int ParkingSpotsAmount { get; set; }
        public int ParkingSpotSize { get; set; }
        public int BicycleSize{ get; set; }
        public int  McSize{ get; set; }
        public int CarSize { get; set; }
    }
}
