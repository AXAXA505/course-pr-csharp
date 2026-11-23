using System;

namespace Vending
{
    public sealed class Tea : BaseDrink
    {
        public Tea() : base("чай по умолчанию", "рецепт по умолчанию")
        {
            Console.WriteLine("[Tea] default ctor");
        }

        public Tea(string name, string recipeName) : base(name, recipeName)
        {
            Console.WriteLine($"[Tea] ctor: \"{Name}\"");
        }

        public Tea(string name, Recipe recipe) : base(name, recipe)
        {
            Console.WriteLine($"[Tea] ctor (shared recipe): \"{Name}\"");
        }

        public Tea(Tea other) : base(other)
        {
            Console.WriteLine("[Tea] copy ctor");
        }

        public override string GetCategory() => "Чай";

        public override BaseDrink Clone() => new Tea(this);
        public override string Describe()
        {
            return $"ЧАЙ: {Header()}\n  Ингредиентов: {Recipe.Count}";
        }
    }
}