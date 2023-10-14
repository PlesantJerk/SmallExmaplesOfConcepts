using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppDomainShnapDomain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDomain ad = AppDomain.CreateDomain("Main Domain");
            AppDomain shadow = AppDomain.CreateDomain("Shadow Domain");
            MBR mbr1 = ad.CreateInstance<MBR>();
            MBR mbr2 = shadow.CreateInstance<MBR>();
            mbr1.Index = 15; // static fields are not shared across appdomains
            mbr2.Index = 2; // they all have their own unique address space
            mbr1.SayHello();
            mbr2.SayHello();
            try
            {
                Fail f = mbr2.GetFail();
                Console.WriteLine(f.ID);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to get the return value on GetFail: {0}", ex.Message);
            }
            mbr2.SetFailID("10");
            mbr1.SetFailID("50");
            Console.WriteLine("MBR1 Fail ID is {0}", mbr1.GetFailID());
            Console.WriteLine("MBR2 Fail ID is {0}", mbr2.GetFailID());
            Pass p1 = mbr1.GetPass();
            Console.WriteLine("Pass id is {0}", p1.ID);

            // now, because its serialzied its just a copy, its disconnected from the 
            // source of the copy, so:
            p1.ID = "6213";
            Pass p1a = mbr1.GetPass();
            Console.WriteLine("Pass id for first copy is {0} and second instance {1}", p1.ID, p1a.ID);

        }

        
    }
}
