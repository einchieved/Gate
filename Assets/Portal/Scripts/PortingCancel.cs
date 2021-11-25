using UnityEngine;
using static IPortable;

public class PortingCancel : MonoBehaviour
{
    private Collider coll;

    private void OnTriggerEnter(Collider other)
    {
        IPortable portable = other.GetComponent<IPortable>();
        if (portable != null && portable.CurrentPortingState == PortingState.InProgress)
        {
            coll = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IPortable portable = other.GetComponent<IPortable>();
        if (other == coll && portable.CurrentPortingState == PortingState.InProgress)
        {
            portable.CurrentPortingState = PortingState.EndingNegative;
        }
    }
}
