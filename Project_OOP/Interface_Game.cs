using System;
using System.Threading;

namespace Project_OOP;

public class InterfaceGame
{
    public void ShowSignsOrder(string player, string opponent ,int order)
    {
        switch (order)
        {
            case 1:
            {
                Console.WriteLine("As X plays - "+ player); 
                Console.WriteLine("As 0 plays - "+ opponent);
                break;
            }

            case 2:
            {
                Console.WriteLine("As X plays - "+ opponent); 
                Console.WriteLine("As 0 plays - "+ player);
                break;
            }
        }
        Thread.Sleep(2000);
    }

    public int AskForRowInput(BasicGameAccount player, char[][] ticTacToe, char playerSign)
    {
        PrintTickTakToe(ticTacToe);
        Console.WriteLine(player.UserName+" input row, where you want to place "+playerSign+" (1-3):");
        int row = IntValidator.ParseChoiceToInt(Console.ReadLine());
        while(1 > row || row > 3)
        {
            Console.Clear();
            PrintTickTakToe(ticTacToe);
            Console.WriteLine("Please input numbers in a range 1-3");
            row = IntValidator.ParseChoiceToInt(Console.ReadLine());
        }
        return row;
    }
    
    public int AskForColInput(BasicGameAccount player, char[][] ticTacToe, char playerSign)
    {
        Console.WriteLine(player.UserName+" input column, where you want to place "+playerSign+" (1-3):");
        int col = IntValidator.ParseChoiceToInt(Console.ReadLine());
        while(1 > col || col > 3)
        {
            Console.Clear();
            PrintTickTakToe(ticTacToe);
            Console.WriteLine("Please input numbers in a range 1-3");
            col = IntValidator.ParseChoiceToInt(Console.ReadLine());
        }
        return col;
    }
    
    public void PrintTickTakToe(char[][] ticTacToe)
    {
        for (int i = 0; i < ticTacToe.Length; i++)
        {
            Console.Write("| ");
            for (int j = 0; j < ticTacToe[0].Length; j++)
            {
                Console.Write(ticTacToe[i][j] + " | ");
            }
            Console.Write("\n");
        }
    }
}