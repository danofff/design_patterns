using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strategy
{
    class Program
    {
        interface IMovable
        {
            void Move(Car car);
        }

        class PetrolMove:IMovable
        {
            public void Move(Car car)
            {
                Console.WriteLine(car.Model + " едет на бензине");
            }
        }

        class ElectricMove : IMovable
        {
            public void Move(Car car)
            {
                Console.WriteLine(car.Model+" едет на электричестве");
            }
        }

        class Car
        {
            public int Passengers { get; set; }
            public string Model { get; set; }
            public IMovable Movable { private get; set; }

            public Car(int passengers,string model,IMovable mov)
            {
                Passengers = passengers;
                Model = model;
                Movable = mov;
            }
            public void Move()
            {
                Movable.Move(this);
            }
        }
        static void Main(string[] args)
        {
            Car car1 = new Car(5, "Tesla Model X", new ElectricMove());
            car1.Move();
            Car car2 = new Car(4, "Honda Cr-V", new PetrolMove());
            car2.Move();
            Console.ReadKey();
        }
    }
}
