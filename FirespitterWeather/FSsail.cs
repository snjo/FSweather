using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirespitterWeather
{
    class FSsail : PartModule
    {        
        [KSPField]
        public string sailTransformName = string.Empty;
        [KSPField]
        public bool useDynamicForce = false;
        [KSPField(isPersistant = true)]
        public float deployedAmount = 0f;
        [KSPField]
        public float baseDrag = 1f;
    }
}
