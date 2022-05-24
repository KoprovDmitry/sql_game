using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sql_neural.Data.Flots
{
    interface FoundationFlots
    {
        double Speed { get; set; }
        double Attack { get; set; }
        double HeadPoint { get; set; }
        double Defense { get; set; }
        double Shield { get; set; }
    }
}
