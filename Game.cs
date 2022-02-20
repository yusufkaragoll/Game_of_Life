using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Game_of_Life
{
    public class Game
    {
        private static int _rowCount;
        private static int _colCount;
        private const int MaxRowCount = 50;
        private const int MaxColCount = 200;
        
        private static int _livingCellCount;
        private static List<string> _lineList;

        

        private static string path = "C:\\Users\\yusuf\\Desktop\\game of life\\save.txt";
        
        

        public static void Main(string[] args)
        {
            Generation generation;
            
            
            if (DoesFileExist(path))
            {
                Console.WriteLine("Loading`````");
                Thread.Sleep(1000); 
                generation = LoadGame();
            }
            
            else
            {
                Console.WriteLine("Write positive rowcount should be less than 50 ( max_row) = ");
                _rowCount = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Write positive colcount (should be less than 200 ( max_col) = ");
                _colCount = Convert.ToInt32(Console.ReadLine());
                while (_rowCount > MaxRowCount || _colCount > MaxColCount || _rowCount <= 0 || _colCount <= 0)
                {
                    Console.WriteLine("Re-write rowcol values");
                    Console.WriteLine("write positive rowcount (should be less than 50 ( max_row) = ");
                    _rowCount = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Write non-negative colcount should be less than 200 max_col) = ");
                    _colCount = Convert.ToInt32(Console.ReadLine());
                }
                Console.Clear();
                Console.WriteLine("Write positive livingCellCount (should be less or equal than board size, which is " + _rowCount * _colCount);
                _livingCellCount = Convert.ToInt32(Console.ReadLine());
                while (_livingCellCount > _rowCount * _colCount || _livingCellCount < 0)
                {
                    Console.WriteLine("Re-write livingCellCount value.");
                    Console.WriteLine("Write positive livingCellCount (should be less or equal than board size, which is " + _rowCount * _colCount);
                    _livingCellCount = Convert.ToInt32(Console.ReadLine());
                }
                Console.Clear();
                generation = new Generation(_rowCount, _colCount, _livingCellCount);
                Console.WriteLine("Your data being prepared, it might take a while``````.......``````");
                Thread.Sleep(3000); 
            }
            
            

            
            
            
            
            
            
            Console.CancelKeyPress += (sender, cancelEventArgs) =>
            {
                Console.WriteLine("\n... Saving \n" +
                                  " ~ It might take a while ~ "); 
                SaveGame(generation, path);
            };
            
            Console.Clear();
            generation.GenerateBoard(100000);
        }


        
        static void SaveGame(Generation generation, string path)          
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                
                _rowCount = generation.SizeRow;
                _colCount = generation.SizeCol;
                writer.Write(_rowCount + ",");
                writer.Write(_colCount + "\n");
                for (int i = 0; i < _rowCount; i++)
                {
                    for (int j = 0; j < _colCount; j++)
                    {
                        if (generation.Board[i, j].CellState == State.Alive)
                        {
                            writer.Write("1 ");
                        }
                        else
                        {
                            writer.Write("0 ");

                        }
                    }
                    writer.Write("\n");
                }
            }
        }

            
        static Generation LoadGame()
        {
            IEnumerable<string> saveFile = File.ReadLines(path);
            _lineList = new List<string>();
            bool firstIteration = true;
            foreach (string line in saveFile)
            {
                
                
                if (firstIteration)
                {
                    string input = line;
                    string[] splitString = input.Split(',');
                    _rowCount = Convert.ToInt32(splitString[0]);
                    Console.Write(_rowCount);
                    _colCount = Convert.ToInt32(splitString[1]);
                    Console.WriteLine(_colCount);
                    firstIteration = false;
                    continue;
                }
                _lineList.Add(line);
            }

            SetLivingCellCount(_lineList);
            Generation generation = new Generation(_rowCount, _colCount, _livingCellCount);
            

            int rowInd = 0;
            int colInd = 0;
            for (int i = 0; i < _lineList.Count; i++)
            {
                for (int j = 0; j < _lineList[0].Length; j++)
                {
                    
                    if (_lineList[i][j] == '1')
                    {
                        generation.Board[rowInd, colInd].CellState = State.Alive;
                        colInd++;
                    } else if (_lineList[i][j] == '0')
                    {
                        generation.Board[rowInd, colInd].CellState = State.Dead;
                        colInd++;
                    }
                    
                }

                rowInd++;
                colInd = 0;
            }

            rowInd = 0;
            colInd = 0;
            return generation;


        }
       
        static void SetLivingCellCount(List<string> list)
        {
            int rowInd = 0;
            int colInd = 0;
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[0].Length; j++)
                {
                    
                    if (_lineList[i][j] == '1')
                    {
                        _livingCellCount++;
                    }
                    colInd++;
                    
                }

                rowInd++;
                colInd = 0;
            }

            rowInd = 0;
            colInd = 0;
        }
        //author Yusuf Karagol


        static bool DoesFileExist(string path)
        {
            return File.Exists(path);
        }
    }
    
    
}