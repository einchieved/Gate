using UnityEngine;
using static IPortable;

/// <summary>
/// Tells the object entering to finish its teleporting process (if it meets the requirements)
/// </summary>
public class PortingCancel : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        IPortable portable = other.GetComponent<IPortable>();
        if (portable.CurrentPortingState == PortingState.InProgress)
        {
            portable.CurrentPortingState = PortingState.Ending;
        }
    }
}
