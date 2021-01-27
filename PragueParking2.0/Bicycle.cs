using System;
using System.Collections.Generic;
using System.Text;

namespace PragueParking2._0
{
    public class Bicycle : Vehicle
    {
        public Bicycle(string owner, string color, int parkedAt, DateTime parkedSince) : base(owner, parkedAt, parkedSince)
        {
            this.Color = color;
        }
        public Bicycle(string owner, string color, int parkedAt, DateTime parkedSince, string token) : base(owner, parkedAt, parkedSince, token)
        {
            this.Color = color;
        }
        public string Color { get; set; }
        public string identifier { get { return "bicycle"; } }
    }
}
