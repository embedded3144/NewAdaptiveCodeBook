using Chapter03._3_21;
using ImpromptuInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03._3_23
{
    class Program
    {
        static void Main(string[] args)
        {
            var swan = new Swan();
            var swanAsDuck = Impromptu.ActLike<IDuck>(swan);
            if(swanAsDuck != null)
            {
                swanAsDuck.Walk();
                swanAsDuck.Swim();
                swanAsDuck.Quack();
            }
            Console.ReadKey();
        }
    }
}
