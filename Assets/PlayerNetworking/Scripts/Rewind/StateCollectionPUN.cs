public class StateCollectionPUN
{
    private readonly int size = 600;
    private int pointer = 0;
    private StatePUN[] states;

    public StateCollectionPUN()
    {
        states = new StatePUN[size];
    }
    
    
    public void Reset()
    {
        states = new StatePUN[size];
        pointer = 0;
    }
    


    public void Push(StatePUN state)
    {
        StatePUN oldState = states[Mod(pointer - 1, size)];

        if (oldState == null || !oldState.Equals(state))
        {
            states[pointer] = state;
            pointer = (pointer + 1) % size;
        }
    }

    public StatePUN Pop()
    {
        int lastStatePos = Mod((pointer - 1), size);
        
        StatePUN state = states[lastStatePos];
        
        states[lastStatePos] = null;
        pointer = lastStatePos;
        
        return state;
    }
    
    int Mod(int x, int m) {
        return (x % m + m) % m;
    }

    public bool Peak()
    {
        if (states[Mod((pointer - 1),size)] != null)
        {
            return true;
        }
        return false;
    }
}
