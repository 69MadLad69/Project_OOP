using System;

namespace Project_OOP.Properties
{
    public class Writer
    {
        private int _tableWidth = 161;

        public void Separator(){
            Console.WriteLine(new string('-', _tableWidth));
        }

        public void ChangeTableWidth(int newWidth)
        {
            _tableWidth = newWidth;
        }

        public void PrintTitle(String line)
        {
            Separator();
            Console.Write("|");
            AlignCenterTitle(line,_tableWidth-1);
            Console.Write("|\n");
            Separator();
        }
        
        public void PrintOptionsRow(params string[] options)
        {   
            int lineSize = _tableWidth / options.Length;
            Console.Write("|");
            foreach (var option in options)
            {
                AlignCenterChoices(option,lineSize);
            }
            Console.Write("|\n");
            Separator();
        }
        
        private void AlignCenterTitle(String line, int spaceLength){
            for (int i = 0; i < (spaceLength-1-line.Length)/2; i++){
                Console.Write(" ");
            }
            Console.Write(line);
            for (int i = 0; i < (spaceLength-line.Length)/2; i++){
                Console.Write(" ");
            }
        }
        
        private void AlignCenterChoices(String line, int lineLength)
        {
            for (int i = 0; i < (lineLength+1-line.Length)/2; i++){
                Console.Write(" ");
            }
            Console.Write(line);
            for (int i = 0; i < (lineLength-line.Length)/2; i++){
                Console.Write(" ");
            }
        }
    }
}