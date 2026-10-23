using System;

namespace Vending
{
    public class Drink
    {
        private readonly Recipe _recipe;

        public string Name { get; }
        public bool IsValid => _recipe.Count > 0;

        public Drink() : this("unknown", "unknown recipe") { }

        public Drink(string name, string recipeName)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("[Drink] Ошибка: имя напитка не может быть пустым. Установлено имя \"unknown\".");
                Name = "unknown";
            }
            else
            {
                Name = name;
            }
            _recipe = new Recipe(recipeName);
        }
        public bool AddIngredient(Ingredient? ingredient, int amount)
        {
            return _recipe.AddIngredient(ingredient, amount);
        }
        public override string ToString()
        {
            string status = IsValid ? "готов к использованию" : "пустой, приготовление невозможно";
            return $"Напиток: {Name}\n{_recipe}\n  Статус рецепта: {status}";
        }
    }
}