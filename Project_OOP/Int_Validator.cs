using System;

namespace Project_OOP;

public static class IntValidator
{
    public static int ParseChoiceToInt(string choiсeString)
    { 
        int choice = 0;
        try
        {
            choice = int.Parse(choiсeString ?? string.Empty);
        }
        catch (FormatException)
        {
        }

        return choice;
    }
}