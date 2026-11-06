using System;
using static System.Console;

namespace Vending
{
    using DrinkCatalog = System.Collections.Generic.List<BaseDrink>; // псевдоним типа

    class Program
    {
        static void Main()
        {
            OutputEncoding = System.Text.Encoding.UTF8;

            var coffee  = new Ingredient("Кофе",     Ingredient.IngredientType.Base);
            var milk    = new Ingredient("Молоко",   Ingredient.IngredientType.Base);
            var caramel = new Ingredient("Карамель", Ingredient.IngredientType.Topping);

            // === Конструкторы ===
            WriteLine("=== Конструкторы и порядок вызова ===");
            {
                var latte      = new Coffee("Латте", "Рецепт Латте");   // с параметрами
                var defaultCup = new Coffee();                          // по умолчанию
                var latteCopy  = new Coffee(latte);                     // копирования (глубоко)
                WriteLine();
            }

            // === Поверхностная копия ===
            WriteLine("=== Поверхностная копия (MemberwiseClone) ===");
            {
                var a = new Coffee("Кофе A", "Общий рецепт");
                a.AddIngredient(coffee, 10);
                WriteLine($"Ингредиентов у A: {a.Recipe.Count}");

                var b = (Coffee)a.ShallowCopy();     // делит _recipe с a
                b.AddIngredient(milk, 20);
                WriteLine($"После B.AddIngredient — ингредиентов у A: {a.Recipe.Count}");
                WriteLine($"Ингредиентов у B: {b.Recipe.Count}");
                WriteLine();
            }

            // === Глубокая копия ===
            WriteLine("=== Глубокая копия (copy ctor / Clone) ===");
            {
                var original = new Coffee("Американо", "Рецепт Американо");
                original.AddIngredient(coffee, 15);

                var deepCopy = new Coffee(original);
                deepCopy.AddIngredient(milk, 50);

                BaseDrink asBase = original;
                var clone = asBase.Clone();
                clone.AddIngredient(milk, 50);
                clone.AddIngredient(caramel, 5);

                WriteLine($"Оригинал: {original.Recipe.Count}, копия: {deepCopy.Recipe.Count}, клон: {clone.Recipe.Count}");
                WriteLine();
            }

            // === this / Equals / GetHashCode ===
            WriteLine("=== Equals/GetHashCode и this ===");
            {
                var x = new Coffee("X", "Рецепт X");
                var y = new Coffee("X", "Рецепт X");
                var z = x;

                WriteLine($"x.Equals(y) = {x.Equals(y)}  (равны по содержимому)");
                WriteLine($"ReferenceEquals(x, y) = {ReferenceEquals(x, y)}");
                WriteLine($"ReferenceEquals(x, z) = {ReferenceEquals(x, z)}  (z = x)");
                WriteLine($"x.GetHashCode() == y.GetHashCode() = {x.GetHashCode() == y.GetHashCode()}");
                WriteLine();
            }

            // === Статическое поле ===
            WriteLine($"Создано напитков (статический счётчик): {BaseDrink.CreatedCount}\n");

            // === Массив BaseDrink и override describe ===
            WriteLine("=== Массив BaseDrink ===");
            {
                DrinkCatalog drinks =
                [
                    new Coffee("Американо",   "Р A"),
                    new Tea   ("Зелёный чай", "Р Ч"),
                    new Coffee("Латте",       "Р Л"),
                    new Tea   ("Чёрный чай",  "Р ЧЧ")
                ];

                foreach (var d in drinks)
                {
                    WriteLine($"--- id = {d.Id}, категория = {d.GetCategory()} ---");
                    WriteLine(d.Describe());
                    WriteLine();
                }
                WriteLine($"Всего напитков в массиве: {drinks.Count}");
            }

            WriteLine($"Итого создано напитков: {BaseDrink.CreatedCount}");
            
            WriteLine("\n=== Конец программы ===");
        }
    }
}