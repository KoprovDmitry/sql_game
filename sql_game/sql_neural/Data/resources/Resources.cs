using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sql_neural.Data.resources
{
    class Resources
    {
        private int metall = 0;
        private int crystall = 0;
        private int gas = 0;

        public Resources(int metall, int crystall, int gas)
        {
            this.metall = metall;
            this.crystall = crystall;
            this.gas = gas;
        }

        public int Metall { get { return this.metall; } set { this.metall += value; if(this.metall < 0) { this.metall = 0; } } }
        public int Crystall { get { return this.crystall; } set { this.crystall += value; if (this.crystall < 0) { this.crystall = 0; } } }
        public int Gas { get { return this.gas; } set { this.gas += value; if (this.gas < 0) { this.gas = 0; } } }
    }
}
