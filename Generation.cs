using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Game_of_Life
{
    public class Generation
    {
        public int SizeRow;
        public int SizeCol;
        private Random _random;

        private Cell[] _cells;
        public Cell[,] Board;

        public char cellChar = '■';
        public char deadCellChar = ' ';
        
        
        
        
        public Generation(int sizeRow, int sizeCol, int livingCellCount)
        {
            _random = new Random();
            SizeRow = sizeRow;
            SizeCol = sizeCol;
            Board = new Cell[sizeRow, sizeCol];
            
            
           
            FillCells();

            
            
            if (_cells == null)
            {
                return;
            }
            
            
            foreach (Cell cell in _cells)
            {
                if (livingCellCount > 0)
                {
                    int state = _random.Next(2);
                    if (state == 0)
                    {
                        cell.CellState = State.Dead;
                    }
                    else
                    {
                        cell.CellState = State.Alive;
                        livingCellCount--;
                    }

                }
                else
                {
                    cell.CellState = State.Dead;
                }
            }

            
            FillBoard();
        }

        
        public Generation(int sizeRow, int sizeCol, double density)
        {
            if (density < 0.0)
            {
                density = 0;
            }

            if (density > 1.0)
            {
                density = 1.0;
            }
            
            _random = new Random();
            SizeRow = sizeRow;
            SizeCol = sizeCol;
            Board = new Cell[sizeRow, sizeCol];
            
            
           
            FillCells();
            
            
            
            
            int area = SizeRow * SizeCol;
            int livingCellCount = (int) (density * area);


            if (_cells == null)
            {
                return;
            }
            
            foreach (Cell cell in _cells)
            {
                if (livingCellCount > 0)
                {
                    int state = _random.Next(2);
                    if (state == 0)
                    {
                        cell.CellState = State.Dead;
                    }
                    else
                    {
                        cell.CellState = State.Alive;
                    }
                    livingCellCount--;

                }
                else
                {
                    cell.CellState = State.Dead;
                }
            }

            
            FillBoard();
        }
        
          
        public Generation(List<string> stringBoard)
        {   
            _random = new Random();
            
            SizeRow = getRowSizeFromString(stringBoard);
            SizeCol = getColSizeFromString(stringBoard);
            Board = new Cell[SizeRow, SizeCol];
            
            
            FillCells();
            FillBoard();
            
            if (_cells == null)
            {
                return;
            }

            
            
            int rowIndex = 0;
            int colIndex = 0;
            foreach (string str in stringBoard)
            {
                int index = 0;
                while (index < SizeCol)
                {
                    if (IsValidAliveChar(str[index]))
                    {
                        Board[rowIndex, colIndex].CellState = State.Alive;
                    }
                    else if(IsValidDeadChar(str[index]))
                    {
                        Board[rowIndex, colIndex].CellState = State.Dead;
                    }

                    index++;
                    colIndex++;
                }

                colIndex = 0;
                rowIndex++;
                
            }
        }
        
        
        
        public Generation()
        {
            _random = new Random();
        }
        
        
        
        
        
        
        
        
        
        
        
        public void NextGeneration()
        {
            Cell[,] nextBoard = new Cell[SizeRow, SizeCol];
            
            
            FillGenerationDefault(nextBoard, SizeRow, SizeCol);

            for (int rowIndex = 1; rowIndex < SizeRow - 1; rowIndex++)
            {
                for (int columnIndex = 1; columnIndex < SizeCol - 1; columnIndex++)
                {
                    
                    
                    
                    
                    int aliveNeighbors = 0;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (Board[rowIndex + i, columnIndex + j].CellState == State.Alive)
                            {
                                aliveNeighbors += 1;
                            }
                        }
                    }

                    
                    Cell currentCell = Board[rowIndex, columnIndex];
                    if (currentCell.CellState == State.Alive)
                    {
                        aliveNeighbors -= 1;

                    }

                    
                    
                    switch (currentCell.CellState)
                    {
                        
                        case State.Alive:
                            if (aliveNeighbors == 2 || aliveNeighbors == 3)
                            {
                                nextBoard[rowIndex,columnIndex].CellState = State.Alive;
                            }
                            
                            else
                            {
                                nextBoard[rowIndex,columnIndex].CellState = State.Dead;
                            }
                            break;
                        case State.Dead:
                            
                            if (aliveNeighbors == 3)
                            {
                                nextBoard[rowIndex,columnIndex].CellState = State.Alive;
                            }
                            
                            break;
                            
                    }
                }
            }
            
            
            Board = nextBoard;
        }
        
        
        private void FillBoard()
        {
            int arrayIndex = 0;
            for (int i = 0; i < SizeRow; i++)
            {
                for (int j = 0; j < SizeCol; j++)
                {
                    Board[i,j] = _cells[arrayIndex];
                    arrayIndex++;
                }
            }
        }
        
        
        private void FillCells()
        {
            _cells = new Cell[SizeRow * SizeCol];
            for (int i = 0; i < SizeRow * SizeCol; i++)
            {
                _cells[i] = new Cell();
            }
        }

        
        private void FillGenerationDefault(Cell[,] generation, int sizeRow, int sizeCol)
        {
            for (int i = 0; i < sizeRow; i++)
            {
                for (int j = 0; j < sizeCol; j++)
                {
                    generation[i, j] = new Cell();
                }
            }
        }
        
        
        
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            Cell[,] otherBoard = ((Generation) obj).Board;
            bool isEqual = false;
            for (int i = 0; i < SizeRow && !isEqual; i++)
            {
                for (int j = 0; j < SizeCol; j++)
                {
                    if (this.Board[i, j].CellState == otherBoard[i, j].CellState)
                    {
                        isEqual = true;
                        break;
                    }
                }
                
            }

            return isEqual;
        }
        
        private int getColSizeFromString(List<string> stringBoard)
        {
            return stringBoard[0].Length;
        }

        private int getRowSizeFromString(List<String> stringBoard)
        {
            return stringBoard.Count;
        }
        
        
        public void GenerateBoard(int simCount)
        {
            while (simCount > 0)
            {
                Console.Write(this);
                Thread.Sleep(50);
                this.NextGeneration();
                simCount--;
            }
        }

        
        
        private bool IsValidAliveChar(char character)
        {
            return character == '■' || character == '*';
        }

        private bool IsValidDeadChar(char character)
        {
            return character == ' ' || character == '-';
        }
        
        
        public override string ToString()
        {
            
            StringBuilder stringBuilder = new StringBuilder();
            for (int row = 0; row < this.SizeRow; row++)
            {
                for (int column = 0; column < this.SizeCol; column++)
                {
                    Cell cell = this.Board[row, column];
                    if (cell.CellState == State.Alive)
                    { 
                        stringBuilder.Append(cellChar);
                    }
                    else 
                    {
                        stringBuilder.Append(deadCellChar);
                    }
                }
                
                stringBuilder.Append("\n");
            }
            
            
            Console.CursorVisible = false; 
            Console.SetCursorPosition(0, 0); 
            return stringBuilder.ToString();
                
            
        }
    }
}