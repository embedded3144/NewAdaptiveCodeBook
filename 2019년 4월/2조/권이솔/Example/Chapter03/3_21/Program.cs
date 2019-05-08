using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03._3_21
{
    public interface IDuck
    {
        void Walk();
        void Swim();
        void Quack();
    }

    public class Swan
    {
        public void Walk()
        {
            Console.WriteLine("백조가 걷는다.");
        }

        public void Swim()
        {
            Console.WriteLine("백조가 오리처럼 헤어친다.");
        }

        public void Quack()
        {
            Console.WriteLine("백조가 꽥꽥 거린다.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var swan = new Swan();
            var swanAsDuck = swan as IDuck;

            if(swan is IDuck || swanAsDuck != null)
            {
                swanAsDuck.Walk();
                swanAsDuck.Swim();
                swanAsDuck.Quack();
            }
        }
    }
}
