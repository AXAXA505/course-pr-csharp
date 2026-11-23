using System;

namespace Vending
{
    public class Coffee : BaseDrink
    {
        public Coffee() : base("кофе по умолчанию", "рецепт по умолчанию")
        {
            Console.WriteLine("[Coffee] default ctor");
        }

        public Coffee(string name, string recipeName) : base(name, recipeName)
        {
            Console.WriteLine($"[Coffee] ctor: \"{Name}\"");
        }

        public Coffee(string name, Recipe recipe) : base(name, recipe)
        {
            Console.WriteLine($"[Coffee] ctor (shared recipe): \"{Name}\"");
        }

        public Coffee(Coffee other) : base(other)
        {
            Console.WriteLine("[Coffee] copy ctor");
        }

        public override string GetCategory() => "Кофе";

        public override BaseDrink Clone() => new Coffee(this);

        public override string Describe()
        {
            return base.Describe() + "\n  [Coffee] содержит кофеин, употреблять в умеренном количестве.";
        }
    }
}