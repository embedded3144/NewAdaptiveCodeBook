using Chapter03._3_21;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03._3_22
{
    class Program
    {
        static void Main(string[] args)
        {
            var swan = new Swan();
            DoDuckLikeThings(swan);
            Console.ReadKey();
        }

        static void DoDuckLikeThings(dynamic duckish)
        {
            if(duckish != null)
            {
                duckish.Walk();
                duckish.Swim();
                duckish.Quack();
            }
        }
    }
}
