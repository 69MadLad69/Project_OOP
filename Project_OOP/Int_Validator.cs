using System;

namespace Project_OOP;

public static class IntValidator
{
    public static int ParseChoiseToInt(string choiseString)
    { 
        int choice = 0;
        try
        {
            choice = int.Parse(choiseString ?? string.Empty);
        }
        catch (FormatException)
        {
        }

        return choice;
    }
}