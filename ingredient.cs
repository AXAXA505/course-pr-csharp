using System;

namespace Vending
{
    public class Ingredient
    {
        public enum IngredientType
        {
            Base,
            Topping,
            Ice
        }
        public string Name { get; }
        public IngredientType Type { get; }
        public bool IsTopping => Type == IngredientType.Topping;
        public bool IsIce => Type == IngredientType.Ice;
        public Ingredient() : this("unknown", IngredientType.Base) { }
        public Ingredient(string name, IngredientType type)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("[Ingredient] Ошибка: имя ингредиента не может быть пустым. Установлено имя \"unknown\".");
                Name = "unknown";
            }
            else
            {
                Name = name;
            }
            Type = type;
        }
        public override string ToString()
        {
            string typeStr = Type switch
            {
                IngredientType.Base => "основа",
                IngredientType.Topping => "топпинг",
                IngredientType.Ice => "лёд",
                _ => "неизвестно"
            };
            return $"Ингредиент: {Name} ({typeStr})";
        }
    }
}
