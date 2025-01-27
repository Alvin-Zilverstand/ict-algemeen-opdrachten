using System;

class Program
{
    static void Main(string[] args)
    {
        // Ask the user to enter the first number
        Console.WriteLine("Enter the first number:");
        double num1 = Convert.ToDouble(Console.ReadLine());

        // Ask the user to enter an operator (+, -, *, /)
        Console.WriteLine("Enter an operator (+, -, *, /):");
        string op = Console.ReadLine();

        // Ask the user to enter the second number
        Console.WriteLine("Enter the second number:");
        double num2 = Convert.ToDouble(Console.ReadLine());

        double result = 0;

        // Perform the operation based on the entered operator
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
                    // Display an error message if attempting to divide by zero
                    Console.WriteLine("Cannot divide by zero.");
                    return;
                }
                break;
            default:
                // Display an error message if the entered operator is invalid
                Console.WriteLine("Invalid operator.");
                return;
        }

        // Display the result of the calculation
        Console.WriteLine("The result is: " + result);
    }
}
