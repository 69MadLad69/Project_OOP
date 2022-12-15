using System;
using System.Collections.Generic;

namespace Project_OOP.Properties
{
    public class Writer
    {
        public int TableWidth = 161;

        public void Seperator(){
            Console.WriteLine(new string('-', TableWidth));
        }

        public void PrintTitle(String line)
        {
            Seperator();
            Console.Write("|");
            AlignCenterTitle(line,TableWidth-1);
            Console.Write("|\n");
            Seperator();
        }
        
        public void PrintOptions(params string[] options)
        {
            foreach (var option in options)
            {
                Console.Write("|");
                AlignCenterColStart();
                Console.Write(option);
                AlignCenterColEnd(option);
                Console.Write("|\n");
            }
            Seperator();
        }

        public void PrintOptionsRow(params string[] options)
        {   
            int lineSize = TableWidth / options.Length;
            Console.Write("|");
            foreach (var option in options)
            {
                AlignCenterChoises(option,lineSize);
            }
            Console.Write("|\n");
            Seperator();
        }
        
        private void AlignCenterTitle(String line, int lineLength){
            for (int i = 0; i < (lineLength-1-line.Length)/2; i++){
                Console.Write(" ");
            }
            Console.Write(line);
            for (int i = 0; i < (lineLength-line.Length)/2; i++){
                Console.Write(" ");
            }
        }
        
        private void AlignCenterChoises(String line, int lineLength)
        {
            for (int i = 0; i < (lineLength+1-line.Length)/2; i++){
                Console.Write(" ");
            }
            Console.Write(line);
            for (int i = 0; i < (lineLength-line.Length)/2; i++){
                Console.Write(" ");
            }
        }

        private void AlignCenterColStart()
        {
            for (int i = 0; i < TableWidth/2; i++){
                Console.Write(" ");
            }
        }

        private void AlignCenterColEnd(String line)
        {
            for (int i = 0; i < TableWidth/2-line.Length; i++){
                Console.Write(" ");
            }
        }  
    }
}