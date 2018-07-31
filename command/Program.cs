using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command
{
    class Program
    {
        //интерфейс команды
        interface ICommand
        {
            void Execute();
            void Undo();
        }


        //получатель команды 1
        class TV
        {
            public void On()
            {
                Console.WriteLine("Телевизор включен!");
            }
            public void Off()
            {
                Console.WriteLine("Телевизор выключен!");
            }
        }

        //получатель команды 2
        class Microwave
        {
            public void StartHeating(int time)
            {
                Console.WriteLine("Подогреваем еду");
                Task.Delay(time).GetAwaiter().GetResult();
            }

            public void StopHeating()
            {
                Console.WriteLine("Еда подогрета!");
            }
        }

        //получатель команды 3
        class Volume
        {
            public const int Off = 0;
            public const int Hight = 20;
            private int level;
            public Volume()
            {
                level = Off;
            }

            public void RaiseVolumeLevel()
            {
                if (level < Hight)
                {
                    level++;
                }
                Console.WriteLine($"Уровень звука {level}");
            }
            public void DropVolumeLevel()
            {
                if (level >Off)
                {
                    level--;
                }
                Console.WriteLine($"Уровень звука {level}");
            }
        }

        //конкретная реализация команды для включения телевизора
        class TvOnCommand : ICommand
        {
            TV Tv;

            public TvOnCommand(TV tv)
            {
                Tv = tv;
            }
            public void Execute()
            {
                Tv.On();
            }

            public void Undo()
            {
                Tv.Off();
            }
        }

        //конкретная реализация команды для микроволновки
        class MicriwaveHeatCommand : ICommand
        {
            Microwave Microvawe;
            int Time;

            public MicriwaveHeatCommand(Microwave microvawe,int time)
            {
                Microvawe = microvawe;
                Time = time;
            }
            public void Execute()
            {
                Microvawe.StartHeating(Time);
                Microvawe.StopHeating();
            }

            public void Undo()
            {
                Microvawe.StopHeating();
            }
        }

        //Конкретная реализация команды увеличения и уменьшения звука
        class VolumeLevelCommand : ICommand
        {
            Volume vol;
            public VolumeLevelCommand(Volume v)
            {
                vol = v;
            }

            public void Execute()
            {
                vol.RaiseVolumeLevel();
            }

            public void Undo()
            {
                vol.DropVolumeLevel();
            }
        }


        public class NoCommand : ICommand
        {
            public void Execute()
            {
                
            }

            public void Undo()
            {
                
            }
        }


        //Инициатор команды
        class Remote
        {
            ICommand command;
            public Remote()
            {
                command = new NoCommand();   
            }
            public void SetCommand(ICommand com)
            {
                command = com;
            }

            public void PressButton()
            {
                command.Execute();
            }
            public void PressUndo()
            {
                command.Undo();
            }
        }
        
        static void Main(string[] args)
        {
            Remote remote = new Remote();
            TV tv = new TV();
            remote.SetCommand(new TvOnCommand(tv));
            remote.PressButton();
            remote.PressUndo();

            Microwave micro = new Microwave();
            remote.SetCommand(new MicriwaveHeatCommand(micro, 2000));

            remote.PressButton();
            remote.PressUndo();
           
            Volume volume = new Volume();
            remote.SetCommand(new VolumeLevelCommand(volume));
   
            remote.PressButton();
            remote.PressButton();
            remote.PressButton();
            remote.PressButton();
            remote.PressButton();
            remote.PressButton();
            remote.PressButton();

            remote.PressUndo();

            Console.ReadKey();

        }
    }
}
