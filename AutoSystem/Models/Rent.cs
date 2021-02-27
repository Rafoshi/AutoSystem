using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSystem.Models
{
    public class Rent
    {
        public int ID { get; set; }
        public int VehicleID { get; set; }
        public Vehicle Vehicle { get; set; }
        public string VehicleModel { get; set; }
        public string ValueModel { get; set; }

        public Rent()
        {
            Vehicle = new Vehicle();
        }
    }
}