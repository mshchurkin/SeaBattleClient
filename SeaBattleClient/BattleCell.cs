using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleClient
{
    public class BattleCell
    {
       public int RowNum;
       public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string E { get; set; }
        public string F { get; set; }
        public string G { get; set; }
        public string H { get; set; }
        public string I { get; set; }
        public string J { get; set; }

        public BattleCell( int RowNum,string A,
        string B,
        string C,
        string D,
        string E,
        string F,
        string G,
        string H,
        string I,
        string J)
        {
            this.RowNum = RowNum;
            this.A = A;
            this.B = B;
            this.C = C;
            this.D = D;
            this.E = E;
            this.F = F;
            this.G = G;
            this.H = H;
            this.I = I;
            this.J = J;
        }
    }
}
