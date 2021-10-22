using DefaultNamespace;
using UnityEngine;

public class StateCollection
{
    private readonly int size = 600;
    private int pointer = 0;
    private State[] states;

    public StateCollection()
    {
        states = new State[size];
    }
    

    public void Push(State state)
    {
        State oldState = states[Mod(pointer - 1, size)];

        if (oldState == null || !oldState.Equals(state))
        {
            states[pointer] = state;
            pointer = (pointer + 1) % size;
        }
    }

    public State Pop()
    {
        int lastStatePos = Mod((pointer - 1), size);
        
        State state = states[lastStatePos];
        
        states[lastStatePos] = null;
        pointer = lastStatePos;
        
        return state;
    }
    
    int Mod(int x, int m) {
        return (x%m + m)%m;
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
