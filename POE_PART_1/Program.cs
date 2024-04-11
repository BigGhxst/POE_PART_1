using System;
using System.Collections;

namespace RecipeManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Recipe Manager!");
            //Prompt the user to enter their name
            Console.Write("Please enter your name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Welcome, {0}, to Recipe Manager!", name);

            RecipeManager recipeManager = new RecipeManager();
            bool exit = false;
            // by using a while loop and a switch case for the option menuu
            while (!exit)
            {
                Console.WriteLine("Recipe Manager Menu:");
                Console.WriteLine("1. Enter Recipe Details");
                Console.WriteLine("2. Display Recipe");
                Console.WriteLine("3. Scale Recipe");
                Console.WriteLine("4. Reset Quantities");
                Console.WriteLine("5. Clear Data");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        recipeManager.EnterRecipeDetails();
                        break;
                    case "2":
                        recipeManager.DisplayRecipe();
                        break;
                    case "3":
                        recipeManager.ScaleRecipe();
                        break;
                    case "4":
                        recipeManager.ResetQuantities();
                        break;
                    case "5":
                        recipeManager.ClearData();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }

    class RecipeManager
    {
        //declaring arrays for ingredients and steps where data would be store there
        private ArrayList ingredients = new ArrayList();
        private ArrayList steps = new ArrayList();
        //store original ingredient data
        private string[] originalIngredients;

        //method to enter recipe datails 
        public void EnterRecipeDetails()
        {
            Console.Write("Enter the number of ingredients: ");
            int ingredientCount = int.Parse(Console.ReadLine());

            //storing original ingredient
            originalIngredients = new string[ingredientCount];

            for (int i = 0; i < ingredientCount; i++)
            {
                Console.Write("Enter ingredient {0} name: ", i + 1);
                string name = Console.ReadLine();

                Console.Write("Enter quantity for {0}: ", name);
                string quantity = Console.ReadLine();

                Console.Write("Enter unit of measurement for {0}: ", name);
                string unit = Console.ReadLine();

                ingredients.Add(string.Format("{0}: {1} {2}", name, quantity, unit));
            }

            Console.Write("Enter the number of steps: ");
            int stepCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < stepCount; i++)
            {
                Console.Write("Enter step {0}: ", i + 1);
                steps.Add(Console.ReadLine());
            }

            Console.WriteLine("Recipe details entered successfully.");
        }

        //Methoiddiplay the details
        public void DisplayRecipe()
        {
            if (ingredients.Count == 0 || steps.Count == 0)
            {
                Console.WriteLine("Recipe details are not entered yet.");
                return;
            }

            Console.WriteLine("Recipe:");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in ingredients)
            {
                Console.WriteLine(ingredient);
            }
            Console.WriteLine("Steps:");
            foreach (var step in steps)
            {
                Console.WriteLine(step);
            }
        }
        //method to scale the recipe
        public void ScaleRecipe()
        {
            if (ingredients.Count == 0 || steps.Count == 0)
            {
                Console.WriteLine("Recipe details are not entered yet.");
                return;
            }

            Console.Write("Enter scaling factor (0.5, 2, or 3): ");
            double factor;
            while (!double.TryParse(Console.ReadLine(), out factor) || (factor != 0.5 && factor != 2 && factor != 3))
            {
                Console.WriteLine("Invalid input. Please enter 0.5, 2, or 3.");
            }

            for (int i = 0; i < ingredients.Count; i++)
            {
                string[] parts = ((string)ingredients[i]).Split(':');
                string[] quantityParts = parts[1].Split(' ');
                double quantity = double.Parse(quantityParts[1]);
                double scaledQuantity = quantity * factor;
                ingredients[i] = string.Format("{0}: {1} {2}", parts[0], scaledQuantity, quantityParts[2]);
            }

            Console.WriteLine("Recipe scaled by a factor of {0}.", factor);
        }

        //Method to reset quantities
        public void ResetQuantities()
        {
            if (originalIngredients == null)
            {
                Console.WriteLine("No original ingredient data was found. Please enter recipe first. {0}");
                return;
            }

            for (var i = 0; i < ingredients.Count; i++)
            {
                ingredients[i] = originalIngredients[i];
            }
            Console.WriteLine("Quantities reset to original values.");
        }

        //Method to clear recipe data
        public void ClearData()
        {
            ingredients.Clear();
            steps.Clear();
            originalIngredients = null;
            Console.WriteLine("Recipe data cleared.");
        }
    }
}
