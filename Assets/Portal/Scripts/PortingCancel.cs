using UnityEngine;
using static IPortable;

public class PortingCancel : MonoBehaviour
{
    private Collider coll;

    private void OnTriggerExit(Collider other)
    {
        IPortable portable = other.GetComponent<IPortable>();
        if (portable.CurrentPortingState == PortingState.InProgress)
        {
            portable.CurrentPortingState = PortingState.Ending;
            Debug.LogError("cancled");
        }
    }
}
