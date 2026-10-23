using System;
using System.Collections.Generic;

namespace Vending
{
    public class Recipe
    {
        private readonly List<Requirement> _requirements = [];
        public string Name { get; }
        public int Count => _requirements.Count;

        public Recipe() : this("unknown") { }

        public Recipe(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("[Recipe] Ошибка: имя рецепта не может быть пустым. Установлено имя \"unknown\".");
                Name = "unknown";
            }
            else
            {
                Name = name;
            }
        }
        public bool AddIngredient(Ingredient? ingredient, int amount)
        {
            if (ingredient == null)
            {
                Console.WriteLine("[Recipe] Отказ: ингредиент не задан (null).");
                return false;
            }

            if (amount <= 0)
            {
                Console.WriteLine($"[Recipe] Отказ: количество ингредиента \"{ingredient.Name}\" должно быть больше нуля (получено {amount}).");
                return false;
            }

            if (HasIngredient(ingredient))
            {
                Console.WriteLine($"[Recipe] Отказ: ингредиент \"{ingredient.Name}\" уже присутствует в рецепте.");
                return false;
            }

            _requirements.Add(new Requirement { Ingredient = ingredient, Amount = amount });
            return true;
        }

        public bool HasIngredient(Ingredient ingredient)
        {
            foreach (var req in _requirements)
            {
                if (req.Ingredient == ingredient)
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            string result = $"Рецепт \"{Name}\": {_requirements.Count} ингредиент(ов)\n";
            foreach (var req in _requirements)
            {
                result += $"  - {req.Ingredient.Name} x {req.Amount}\n";
            }
            return result.TrimEnd();
        }
    }
}