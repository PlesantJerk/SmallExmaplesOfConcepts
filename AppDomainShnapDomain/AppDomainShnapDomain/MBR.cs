using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomainShnapDomain
{
    // For two appdomains to communicate they must call methods
    // on objects that live in the other domain.  A MarshalByRefObject is an
    // object that doesn't serialize across the network, but rather it serializes
    // a pointer to itself.  This same ability makes it useful for AppDomains to "talk"
    public class MBR : MarshalByRefObject
    {
        private static int mIndex = 0;
        private Fail mFailObject = new Fail();
        private Pass mPassObject = new Pass();
        public int Index
        {
            get => mIndex;
            set => mIndex = value;
        }

        public void SayHello()
        {
            Console.WriteLine("I'm in {0} AppDomain, my index is {1}", AppDomain.CurrentDomain.FriendlyName, Index);
        }

        // this cal won't work, the object is in a different domain, and its memory cant be accesss
        // this object eithere needs to inherit off MarshalByrefObject or it must be serializable
        public Fail GetFail()
        {
            return mFailObject;
        }

        // Even though you can't serialize it as a result, you can interact with it through the
        // MBR
        public void SetFailID(string id)
        {
            mFailObject.ID = id;
        }

        // can return this because primative types are serializable
        public string GetFailID()
        {
            return mFailObject.ID;
        }

        public Pass GetPass()
        {
            return mPassObject;
        }
    }

    public static class MarshalByRefExtensions
    {
        public static T CreateInstance<T>(this AppDomain src)
            where T : class
        {
            return (T)src.CreateInstanceAndUnwrap(typeof(T).Assembly.FullName, typeof(T).FullName);
        }
    }

    public class Fail
    {
        public Fail()
        {
            When = DateTime.Now;
            ID = "Ben";
        }
        public DateTime When
        {
            get;
            set;
        }

        public string ID
        {
            get;
            set;
        }
    }

    [Serializable]
    public class Pass
    {
        public Pass()
        {
            When = DateTime.Now;
            ID = "Ben";
        }
        public DateTime When
        {
            get;
            set;
        }

        public string ID
        {
            get;
            set;
        }
    }
}
