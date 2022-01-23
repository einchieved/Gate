/// <summary>
/// Provides Possibility to collect a specified amount of states
/// </summary>
public class StateCollectionPUN
{
    private readonly int size = 600;
    private int pointer = 0;
    private StatePUN[] states;

    public StateCollectionPUN()
    {
        states = new StatePUN[size];
    }

    /// <summary>
    /// Add Recorded State to Collection
    /// </summary>
    /// <param name="state">state to be stored</param>
    public void Push(StatePUN state)
    {
        StatePUN oldState = states[Mod(pointer - 1, size)];

        if (oldState == null || !oldState.Equals(state))
        {
            states[pointer] = state;
            pointer = (pointer + 1) % size;
        }
    }

    /// <summary>
    /// Retrieve the last recorded state out of the collection
    /// </summary>
    /// <returns>the last recorded state</returns>
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

    /// <summary>
    /// Check if there is a state recorded before the current one
    /// </summary>
    /// <returns>true if a state is available else false</returns>
    public bool Peak()
    {
        if (states[Mod((pointer - 1),size)] != null)
        {
            return true;
        }
        return false;
    }
}
