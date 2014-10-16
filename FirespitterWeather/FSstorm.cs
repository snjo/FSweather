using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FirespitterWeather
{
    public class FSstorm
    {
        public string stormName = "Daffy"; // storm name list needed  
        public float latitude;
        public float longitude;
        public float altitude;
        public Vector2 speed; // moving across lat and long at speed per ... something time.
        public float radius;
        public float strength; // windspeed
        public float precipitation;
        public float cloudsDensity; // or just no of clouds
        public float temperature;        
        public float lifetime; // in seconds                        
        public float rateOfChange;
        public float windGustPotential;
        public float windGustChance;
        public float lightningChance; // public float thunderChance;        
    }
}
