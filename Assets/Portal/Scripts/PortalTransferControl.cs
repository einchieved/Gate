using UnityEngine;
using static IPortable;

public class PortalTransferControl : MonoBehaviour
{
    private Collider coll;

    private void OnTriggerEnter(Collider other)
    {
        IPortable portable = other.GetComponent<IPortable>();
        if (portable != null && CurrentStateOK(portable))
        {
            coll = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IPortable portable = other.GetComponent<IPortable>();
        if (other == coll && portable.CurrentPortingState == PortingState.InProgress)
        {
            portable.CurrentPortingState = PortingState.EndingPositive;
        }
    }

    private bool CurrentStateOK(IPortable testSubject)
    {
        return testSubject.CurrentPortingState == PortingState.Ended || testSubject.CurrentPortingState == PortingState.InProgress;
    }
}
