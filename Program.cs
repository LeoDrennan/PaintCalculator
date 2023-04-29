﻿using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

internal class Program
{
    private static void Main(string[] args)
    {
        // Greet user
        Console.WriteLine("Welcome to the paint calculator!");
        Console.WriteLine("____________________________________\n");


        // Ask user to choose which units of measurement that they wish to use
        Console.WriteLine("Please choose your desired unit of measurement.");
        Console.WriteLine("Please type either metres(m), centimetres(cm), or millimetres(mm) below:");
        Console.WriteLine();
        string unitMode = Console.ReadLine();


        // Prompt user for number of walls
        Console.WriteLine("\nPlease enter the number of walls that you are painting:");
        int wallInt = checkInputIsInteger();

        double paintableArea = 0f;

        // Calculate total paintable area
        for (int i = 1; i < wallInt + 1; i++)
        {
            // Prompt user for wall dimensions and calculate area
            Console.WriteLine("\nPlease enter the dimensions of wall {0}:", i);
            double wallArea = calculateArea();

            // Prompt user for number of windows or doors on wall i
            Console.WriteLine("\nPlease enter the number of windows and doors on wall {0}:", i);
            int gapInt = checkInputIsInteger();

            // Prompt user for window/door dimensions and calculate area to be subtracted from the wall
            for (int j = 1; j < gapInt + 1; j++)
            {
                Console.WriteLine("\nPlease enter the dimensions of window/door {0} on wall {1} in metres:", j, i);
                double gapArea = calculateArea();
                wallArea -= gapArea;
            }

            paintableArea += wallArea;
        }

        // Load the most up to date version of the paint colour catalogue
        Dictionary<string, int> colours = paintCatalogue();

        // Format and display paint options to the user
        Console.WriteLine("\nPaint selection:\n");
        foreach (string colour in colours.Keys)
        {

            Console.WriteLine('|' + $"{char.ToUpper(colour[0]) + colour.Substring(1)}".PadRight(10) + "| £" + colours[$"{colour}"]);
        }

        // Take user input for paint colour
        Console.WriteLine("\nPlease input the colour of paint you wish to use:");
        string paintType;
        bool validPaint;

        // Check input is a valid colour of paint
        do
        {
            paintType = Console.ReadLine().ToLower();
            validPaint = colours.ContainsKey($"{paintType}");

            if (!validPaint)
            {
                Console.WriteLine("\nPaint colour is not in the catalogue, please choose another: ");
            }
        }
        while (!validPaint);

        // Find cost per litre of chosen paint
        double litreCost = colours[$"{paintType}"];

        // Take user input for number of coats of paint
        Console.WriteLine("\nPlease enter the number of coats of paint that you wish to apply:");
        int coatInt = checkInputIsInteger();
        double coatFloat = Convert.ToSingle(coatInt);

        // Calculate total area of paint
        if (paintableArea <= 0)
        {
            Console.WriteLine("\nArea to be painted is zero or less, please start again.");
            return;
        }

        // Area being painted
        double totalLitres = paintableArea * coatFloat / 2.5f;
        double roundLitres = (int)Math.Ceiling(totalLitres);

        // Calculate paint that will be left over and format for response
        double remainingPaint = roundLitres - totalLitres;
        string remainingString = remainingPaint.ToString("#.##");

        // Calculate amount of paint needed
        double totalCost = roundLitres * litreCost;

        // Output
        Console.WriteLine("\n\nJOB COSTINGS");
        Console.WriteLine("_______________________\n");
        Console.WriteLine("You will need {0} litres of paint, which will cost £{1}.", roundLitres, totalCost);
        Console.WriteLine("You will have approximately 0{0} litres of paint remaining.", remainingString);
    }

    // Takes user input and ensures that it is an integer, repeating until a valid integer is provided.
    static int checkInputIsInteger()
    {
        string inputString;
        bool validInput = false;
        int validatedInput;

        do
        {
            inputString = Console.ReadLine();
            if (int.TryParse(inputString, out validatedInput) && validatedInput > -1)
            {
                validInput = true;
            }
            else
            {
                Console.WriteLine("\nInput must be an integer value, please try again:");
            }
        }
        while (!validInput);
        return validatedInput;
    }

    //  Takes user input and ensures that it is an double, repeating until a valid input is provided.
    static double checkInputIsNumerical(string measurement)
    {
        string inputString;
        bool validInput = false;
        double validatedInput;

        do
        {
            inputString = Console.ReadLine();
            if (double.TryParse(inputString, out validatedInput) && validatedInput > 0)
            {
                validInput = true;
            }
            else
            {
                Console.WriteLine(String.Format("Input must be a positive value, please input {0} again:", measurement));
            }
        }
        while (!validInput);
        return validatedInput;
    }

    // Takes user input for height and width, and outputs the resulting area as a double.
    static double calculateArea()
    {
        Console.WriteLine("\nHeight:");
        double height = checkInputIsNumerical("height");

        Console.WriteLine("\nWidth:");
        double width = checkInputIsNumerical("width");

        double area = height * width;
        return area;
    }


    // Loads the current catalogue of paints that are available
    static Dictionary<string, int> paintCatalogue()
    {
        var colours = new Dictionary<string, int>();
        colours.Add("white", 5);
        colours.Add("black", 5);
        colours.Add("brown", 3);
        colours.Add("red", 3);
        colours.Add("green", 3);
        colours.Add("blue", 3);
        colours.Add("yellow", 3);
        colours.Add("purple", 6);
        colours.Add("orange", 6);

        return colours;
    }
}