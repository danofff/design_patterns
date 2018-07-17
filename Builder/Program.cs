using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    class Program
    {
        public enum screenSize { small, middle, wide };
        public enum OS { windows, ios, android};       
        public enum Battery { a2000, a3000, a4000};

        class Phone
        {
            public screenSize Size { get; set; }
            public OS  Os { get; set; }
            public Battery Battery { get; set; }
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Screen size: " + Size + "\nOS: " + Os +
                    "\nBattery: " + Battery);
                return sb.ToString();
            }
        }


        abstract class PhoneBuilder
        {
            public Phone phone { get; private set; }

            public void CreatePhone()
            {
                phone = new Phone();
            }
            public abstract void setSize();
            public abstract void setOS();
            public abstract void setBattery();
        }

        class Manufacturer
        {
            public Phone Produce(PhoneBuilder builder)
            {
                builder.CreatePhone();
                builder.setSize();
                builder.setOS();
                builder.setBattery();
                return builder.phone;
            }
        }
        class AndroidPhoneBuilder : PhoneBuilder
        {
            public override void setBattery()
            {
                this.phone.Battery=Battery.a4000;
            }

            public override void setOS()
            {
                this.phone.Os = OS.android;
            }

            public override void setSize()
            {
                this.phone.Size = screenSize.wide;
            }
        }

        class IPhoneBuilder : PhoneBuilder
        {
            public override void setBattery()
            {
                this.phone.Battery = Battery.a2000;
            }

            public override void setOS()
            {
                this.phone.Os = OS.ios;
            }

            public override void setSize()
            {
                this.phone.Size=screenSize.small;
            }
        }

        static void Main(string[] args)
        {
            Manufacturer manuf = new Manufacturer();

            AndroidPhoneBuilder aBuilder = new AndroidPhoneBuilder();
            Phone aPhone = manuf.Produce(aBuilder);
            Console.WriteLine(aPhone.ToString());

            IPhoneBuilder iBuilder = new IPhoneBuilder();
            Phone iPhone = manuf.Produce(iBuilder);
            Console.WriteLine(iPhone.ToString());

            Console.ReadKey();
        }
    }
}
