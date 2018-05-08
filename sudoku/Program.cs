using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("{0} arguments recus", args.Length);
            
               

            int[] grid;
            if (args.Length > 0) {
                 grid = parseGrid(args[0]);
            }else{
                grid = getNullGrid();
            }                      

            Console.WriteLine("grille entrée");
            printGrid(grid);
            bool rs = solve(grid);
            
            //code pour trouver l'index de la premiere case de la celle en fonction d'un index donné.
            //on sent bien qu'une simplification est possible quelque part, mais je vois pas trop comment.
            /*
            for (int i=0 ; i<81;i++){
                //grid[i] = i/3%3;
                //grid[i] = i/9/3;
                grid[i] = 3*(i/3%3)+27*(i/9/3);
            }
            printLargeGrid(grid);
            */

        }

        private static int[] parseGrid(string input)
        {
            if (input.Length == 81 && Regex.IsMatch(input,@"^[0-9]+$") )
            {
                int[] grid = new int[81];
                for (int i=0 ; i < 81 ; i++)
                {
                    grid[i] = (int)char.GetNumericValue(input[i]);
                    
                }
                return grid;
            }else{
                return getNullGrid();
            }
            
        }

        private static bool solve(int[] grid){
            for (int i = 0; i < grid.Length; i++)
            {
                
                if (grid[i] == 0)//on doit trouver cette case.
                {
                    for (int t = 1; t < 10; t++)//on essai toutes les valeures
                    {
                        if (isValid(grid, i, t))
                        {
                            int[] newGrid = (int[]) grid.Clone();
                            newGrid[i] = t;
                        
                           if (solve(newGrid)){
                               return true;
                           }
                        }
                    }
                    return false; //toutes valeures testées, sans solution.
                }
            }
            printGrid(grid);//plus de case a trouver, on est bon :)
            return true;
        }//fin solve

        private static bool isValid(int[] grid, int p , int t){
            
            foreach (int i in getRowIds(p))//test sur la ligne
            {
                if (grid[i] == t) { return false; }
            }

            foreach (int i in getColIds(p))//test sur la colone
            {
                if (grid[i] == t) { return false; }
            }

            foreach (int i in getCellIds(p))//test sur la cell (sous case de 3*3)
            {
                if (grid[i] == t) { return false; }
            }

            return true;
        }

        private static int[] getRowIds(int p){
            int line = (p / 9)*9;
            return new int[9] {line, line+1 , line+2, line+3, line+4, line+5, line+6, line+7, line+8};
        }
        
        private static int[] getColIds(int p)
        {
            int col = p % 9;
            return new int[9] {col, col+9, col+18, col+27, col+36, col+45, col+54, col+63, col+72};
        }

        private static int[] getCellIds(int p)
        {
            int cell = 3*(p/3%3)+27*(p/9/3);
            return new int[9] {cell, cell+1, cell+2, cell+9, cell+10,cell+11, cell+18,cell+19,cell+20};
        }


        private static int[] getNullGrid()
        {
            int[] newGrid = new int[81];
            for (int i = 0; i < 81; i++) 
            {
                newGrid[i] = 0;
            }
            return newGrid;
        }

        private static int[] parseGrid(){
            return null;
        }


        private static int[] doubleGrid(int[] grid){
            for (int i=0 ;i<grid.Length;i++)
            {
                grid[i]*= 2;
            }
            return grid;
        }


        private static void printGrid(int[] grid)
        {
            for (int i = 0; i < 81; i++)
            {
                if (i % 9 == 0)
                {
                    Console.Write("\n{0:0} ", grid[i]);
                }
                else
                {
                    Console.Write("{0:0} ", grid[i]);
                }
                
            }
            Console.Write("\n");
        }

        private static void printLargeGrid(int[] grid)
        {
            for (int i = 0; i < 81; i++)
            {
                if (i % 9 == 0)
                {
                    Console.Write("\n{0:00} ", grid[i]);
                }
                else
                {
                    Console.Write("{0:00} ", grid[i]);
                }

            }
            Console.Write("\n");
        }

    }
}
