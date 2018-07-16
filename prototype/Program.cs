using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace prototype
{
    class Program
    {
        public interface ICloneable
        {
            object Clone();
            object DeepClone();
        }

        [Serializable]
        public class FullName
        {
            public string FirstName { get; set; }
            public string FamilyName { get; set; }

            public override string ToString()
            {
                return FirstName + " " + FamilyName;
            }
        }
        [Serializable]
        public class MrSmith : ICloneable
        {
            public FullName Name { get; set; }
            public int Age { get; set; }

            public MrSmith() { }

            //light clone without cloning reference objects
            public object Clone()
            {
                return new MrSmith() { Name = this.Name, Age = this.Age };
            }
            //deep clone using binary serialization
            public object DeepClone()
            {
                object clone;
                using (MemoryStream stream= new MemoryStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter(null,
                        new StreamingContext(StreamingContextStates.Clone));
                    formatter.Serialize(stream, this);
                    stream.Seek(0, SeekOrigin.Begin);
                    clone = formatter.Deserialize(stream);
                }
                return clone;
            }
        }
        static void Main(string[] args)
        {
            //Creating first instanse of MrSmith;
             MrSmith s1 = new MrSmith() { Name = new FullName() { FirstName = "Mister", FamilyName = "Smith" }
            , Age = 37};
            //Cloninig first instance using light cloninig
            MrSmith s2 = s1.Clone() as MrSmith;

            //Cloninig first instance using deep cloninig
            MrSmith s3 = s1.DeepClone() as MrSmith;

            //changing FirsName in clone, which used light 
            //cloninnig supose change Name in first instance
            s2.Name.FirstName = "Missis";
            //changing FirsName in clone, which used light 
            //cloninnig supose not changing Name in first instance
            s3.Name.FirstName = "Mr";
            Console.WriteLine("s1 " + s1.Name);
            Console.WriteLine("s2 " + s2.Name);
            Console.WriteLine("s3 " + s3.Name);
            Console.ReadKey();
        }
    }
}
