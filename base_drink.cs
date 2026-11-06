using System;

namespace Vending
{
    public abstract class BaseDrink
    {
        private static int s_createdCount = 0;
        private int _id;
        private string _name;
        private Recipe _recipe;
        public int Id => _id;
        public string Name => _name;
        public Recipe Recipe => _recipe;
        public bool IsValid => _recipe.Count > 0;
        public static int CreatedCount => s_createdCount;
        protected string Header() => $"{GetCategory()} \"{_name}\""; // protected
        private static int NextId() => ++s_createdCount;

        // === ctors ===
        protected BaseDrink(BaseDrink other)
        {
            _id = NextId();
            _name = other._name;
            _recipe = new Recipe(other._recipe); // глубокая копия
            Console.WriteLine($"[BaseDrink] copy ctor: \"{_name}\"");
        }

        public BaseDrink(string name, Recipe recipe)
        {
            _id = NextId();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("[BaseDrink] Ошибка: имя напитка не может быть пустым. Установлено имя \"unknown\".");
                _name = "unknown";
            }
            else
            {
                _name = name;
            }
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe));
            }
            _recipe = recipe;
            Console.WriteLine($"[BaseDrink] ctor: \"{_name}\"");
        }

        public BaseDrink(string name, string recipeName) : this(name, new Recipe(recipeName)) { }


        // === абстрактные методы ===
        public abstract string GetCategory();
        public abstract BaseDrink Clone();

        // === виртуальные методы ===
        public virtual string Describe()
        {
            return $"Напиток: {Header()}";
        }

        // === поверхностная копия ===
        public BaseDrink ShallowCopy()
        {
            var copy = (BaseDrink)MemberwiseClone(); 
            copy._id = NextId();
            Console.WriteLine($"[BaseDrink] shallow copy: \"{copy._name}\"");                 
            return copy;
        }

        // === recipe ===
        public bool AddIngredient(Ingredient? ingredient, int amount) => _recipe.AddIngredient(ingredient, amount);

        // === Equals/GetHashCode ===
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not BaseDrink other) return false;
            return _name == other._name && GetCategory() == other.GetCategory();
        }

        public override int GetHashCode() => HashCode.Combine(_name, GetCategory());

        public override string ToString() => Describe();
    }
}