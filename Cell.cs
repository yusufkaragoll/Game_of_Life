namespace Game_of_Life
{

    

    public enum State
    {
        Dead = 0,
        Alive = 1
    }
    
   
    
    public class Cell
    {
        public State CellState;
        
        public Cell(State cellState)
        {
            CellState = cellState;           
        }

        public Cell()
        {
            CellState = State.Dead;
        }
        
    }
}