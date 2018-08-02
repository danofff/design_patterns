using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template
{
    class Program
    {
        abstract class Education
        {
            public void Learn()
            {
                Enter();
                Study();
                PassExam();
                GetDocument();
            }
            public abstract void Enter();
            public abstract void Study();
            public virtual void PassExam()
            {
                Console.WriteLine("Сдаем выпускные экзамены");
            }
            public abstract void GetDocument();
        }


        class School : Education
        {
            public override void Enter()
            {
                Console.WriteLine("Идем в первый класс!");
            }

            public override void GetDocument()
            {
                Console.WriteLine("Получаем аттестат!");
            }

            public override void Study()
            {
                Console.WriteLine("Посещаем уроки, делаем домашние задания!");
            }
        }

        class University : Education
        {
            public override void Enter()
            {
                Console.WriteLine("Сдаем вступительные экзамены, поступаем в ВУЗ!");
            }

            public override void GetDocument()
            {
                Console.WriteLine("Получаем диплом!");
            }

            public override void Study()
            {
                Console.WriteLine("Посещаем пары!");
            }

            public override void PassExam()
            {
                Console.WriteLine("Сдаем экзамены по специальности!");
            }
        }
        static void Main(string[] args)
        {
            Education school = new School();
            Education universuty = new University();

            school.Learn();
            Console.WriteLine("*****************");
            universuty.Learn();

            Console.ReadKey();
        }

    }
}
