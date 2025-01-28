using System;

class Program
{
    static void Main(string[] args)
    {
        // Vraag de gebruiker om het eerste getal in te voeren
        Console.WriteLine("Voer het eerste getal in:");
        double num1 = Convert.ToDouble(Console.ReadLine());

        // Vraag de gebruiker om een operator in te voeren (+, -, *, /)
        Console.WriteLine("Voer een operator in (+, -, *, /):");
        string op = Console.ReadLine();

        // Vraag de gebruiker om het tweede getal in te voeren
        Console.WriteLine("Voer het tweede getal in:");
        double num2 = Convert.ToDouble(Console.ReadLine());

        double result = 0;

        // Voer de bewerking uit op basis van de ingevoerde operator
        switch (op)
        {
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
            case "*":
                result = num1 * num2;
                break;
            case "/":
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                else
                {
                    // Toon een foutmelding bij poging tot delen door nul
                    Console.WriteLine("Kan niet delen door nul.");
                    return;
                }
                break;
            default:
                // Toon een foutmelding bij een ongeldige operator
                Console.WriteLine("Ongeldige operator.");
                return;
        }

        // Toon het resultaat van de berekening
        Console.WriteLine("Het resultaat is: " + result);
    }
}
