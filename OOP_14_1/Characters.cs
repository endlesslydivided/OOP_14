using System;
using System.Collections;
using System.Collections.Generic;
using static System.Console;

namespace OOP_14_1
{
    [Serializable] public class Characters
    {
        public int Attack;
        public int Defence;
        [NonSerialized] public string Username;
        public string SuperPower;
          
        public Characters() { }
        public Characters(int att,int def, string UN,string SP)
        {
            Attack = att;
            Defence = def;
            Username = UN;
            SuperPower = SP;
        }
        }

    public class Warrior : Characters, IActionable
    {
        public void Forward()
        {
            WriteLine("Только вперёд!");
        }

        void IActionable.Attack()
        {
            WriteLine("Вперёд, в атаку!");
        }
    }

    public class Hunter : Characters, IActionable
    {
        public void Forward()
        {
            WriteLine("Главное, не спугнуть добычу...");
        }

        void IActionable.Attack()
        {
            WriteLine("Зарядить ружья!");
        }
    }

        public class Shaman : Characters, IActionable
    {
        public void Forward()
        {
            WriteLine("Выдвигаемся.");
        }

        void IActionable.Attack()
        {
            WriteLine("Вперёд, покемоны!");
        }
    }

        public class Archer : Characters, IActionable
    {
        public void Forward()
        {
            WriteLine("Быстрее!");
        }

        void IActionable.Attack()
        {
            WriteLine("Нет ничего быстрее  моей стрелы");
        }
    }

        public class Physic : Characters, IActionable
    {
        public void Forward()
        {
            WriteLine("Зачем идти пешком, если можно телепоритроваться...");
        }

        void IActionable.Attack()
        {
            WriteLine("Твой разум будет подвластен мне");
        }
    }

}
