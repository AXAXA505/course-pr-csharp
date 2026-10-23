using System;
using System.Runtime.CompilerServices;

namespace Vending
{
    class Program
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        static WeakReference<Drink> CreateTemporaryDrink(Ingredient coffee)
        {
            var temp = new Drink("Пробный напиток", "Пробный рецепт");
            temp.AddIngredient(coffee, 3);
            return new WeakReference<Drink>(temp);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        static void TryPrintAndForget(WeakReference<Drink> w, string prefix)
        {
            if (w.TryGetTarget(out Drink? d))
            {
                Console.WriteLine(prefix);
                Console.WriteLine(d);
            }
            else
            {
                Console.WriteLine(prefix + " — объект уже уничтожен");
            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Старт программы. Создаем базовые ингредиенты ===");
            var coffee = new Ingredient("Кофе", Ingredient.IngredientType.Base);
            var milk = new Ingredient("Молоко", Ingredient.IngredientType.Base);
            var caramel = new Ingredient("Карамель", Ingredient.IngredientType.Topping);
            var ice = new Ingredient("Лёд", Ingredient.IngredientType.Ice);


            Console.WriteLine("\n--- Демонстрация работы классов ---");
            var latte = new Drink("Латте", "Рецепт Латте");

            latte.AddIngredient(coffee, 20);
            latte.AddIngredient(milk, 150);
            latte.AddIngredient(caramel, 22);

            // Проверки некорректного ввода
            latte.AddIngredient(ice, 0);    // Некорректное количество
            latte.AddIngredient(coffee, 5); // Дубликат
            latte.AddIngredient(null, 10);  // Передача null

            Console.WriteLine("\nТекущее состояние напитка:");
            Console.WriteLine(latte);


            Console.WriteLine("\n=== Ссылки и копии (класс) ===");
            var drinkA = new Drink("Американо", "Рецепт Американо");
            drinkA.AddIngredient(coffee, 15);

            var drinkB = drinkA;   // копируется ссылка, объект один и тот же
            Console.WriteLine($"drinkA и drinkB — один объект: {ReferenceEquals(drinkA, drinkB)}");

            drinkB.AddIngredient(milk, 30);
            Console.WriteLine("\nЧерез drinkB добавили молоко. Печатаем drinkA:");
            Console.WriteLine(drinkA);


            Console.WriteLine("\n=== Ссылки и копии (структура) ===");
            var portionA = new Requirement { Ingredient = coffee, Amount = 20 };
            var portionB = portionA; // копируется значение целиком

            portionB.Amount = 999;           // меняем копию
            Console.WriteLine($"portionA.Amount = {portionA.Amount} (не изменилось)");
            Console.WriteLine($"portionB.Amount = {portionB.Amount}");
            Console.WriteLine($"Ссылка на Ingredient внутри общая: {ReferenceEquals(portionA.Ingredient, portionB.Ingredient)}");


            Console.WriteLine("\n=== Освобождение ресурса: IDisposable и using ===");
            using (var log = new PreparationLog("Приготовление Латте"))
            {
                PreparationLog.Write("добавлен кофе");
                PreparationLog.Write("добавлено молоко");
                PreparationLog.Write("добавлена карамель");
            }
            // Здесь Dispose вызван автоматически - журнал закрыт.
            Console.WriteLine("После блока using журнал закрыт (Dispose вызван автоматически).");

            Console.WriteLine("\n=== Сборщик мусора ===");
            WeakReference<Drink> watcher = CreateTemporaryDrink(coffee);

            TryPrintAndForget(watcher, "Сразу после создания объект жив:");

            Console.WriteLine("\nПросим сборщик мусора собрать мусор...");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            if (watcher.TryGetTarget(out _))
            {
                Console.WriteLine("После GC объект ещё жив.");
            }
            else
            {
                Console.WriteLine("После GC объект уничтожен (ссылок на него не осталось).");
            }

            Console.WriteLine("\n=== Завершение программы ===");
        }
    }
}