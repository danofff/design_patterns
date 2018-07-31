using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace observer
{
    class Program
    {
        //наблюдатель
        interface IObserver
        {
            void Update(object ob);
        }
        //наблюдаемый
        interface IObservable
        {
            void RegisterObserver(IObserver o);
            void RemoveObserver(IObserver o);
            void NotifyObservers();
        }
               
        //Вспомогательный класс информация о котировках
        class StockInfo
        {
            public int USD { get; set; }
            public int Euro { get; set; }
        }

        //Конкретная реализация интерфейса наблюдаемого
        class Stock:IObservable
        {
            StockInfo Info;
            List<IObserver> observers;

            public Stock()
            {
                observers = new List<IObserver>();
                Info = new StockInfo();
            }
            public void RegisterObserver(IObserver o)
            {
                observers.Add(o);
            }

            public void RemoveObserver(IObserver o)
            {
                observers.Remove(o);
            }

            public void NotifyObservers()
            {
                foreach (var observer in observers)
                {
                    observer.Update(Info);
                }
            }
            public void Market()
            {
                Random rnd = new Random();
                Info.USD = rnd.Next(320, 360);
                Info.Euro = rnd.Next(380, 410);
                NotifyObservers();
            }
        }

        //конкретная реализация интерфейса наблюдателя, брокер
        class Broker : IObserver
        {
            public string Name { get; set; }
            IObservable stock;
            public Broker(string name, IObservable obs)
            {
                Name = name;
                stock = obs;
                stock.RegisterObserver(this);
            }
            public void Update(object ob)
            {
                StockInfo sInfo = ob as StockInfo;
                if (sInfo.USD > 340)
                {
                    Console.WriteLine("Брокер {0} продает доллары;  Курс доллара: {1}", Name, sInfo.USD);                  
                }
                else
                {
                    Console.WriteLine("Брокер {0} покупает доллары;  Курс доллара: {1}", Name, sInfo.USD);
                }
            }
            public void StopTrade()
            {
                stock.RemoveObserver(this);
                stock = null;
            }
        }

        //Кокретная реализация интерфейса наблюдателя, банк
        class Bank : IObserver
        {
            public string Name { get; set; }
            IObservable stock;

            public Bank(string name,IObservable obs)
            {
                Name = name;
                stock = obs;
                stock.RegisterObserver(this);
            }
            public void Update(object ob)
            {
                StockInfo sInfo = ob as StockInfo;
                if (sInfo.Euro > 395)
                {
                    Console.WriteLine("Брокер {0} продает евро;  Курс евро: {1}", Name, sInfo.Euro);
                }
                else
                {
                    Console.WriteLine("Брокер {0} покупает евро;  Курс евро: {1}", Name, sInfo.Euro);
                }
            }
        }

        static void Main(string[] args)
        {
            Stock stock = new Stock();
            Bank bank = new Bank("Halyk Bank", stock);
            Broker broker = new Broker("Alina", stock);
            stock.Market();

            Console.ReadKey();
        }
    }
}
