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
            portable.CurrentPortingState = PortingState.Porting;
            //coll = other;
            //Debug.LogError("Enter");
            Debug.LogError("transfer control");
        }
    }

    private void OnTriggerExit(Collider other)
    {/*
        Debug.LogError("pre exit");
        IPortable portable = other.GetComponent<IPortable>();
        if (other == coll && portable.CurrentPortingState == PortingState.InProgress)
        {
            portable.CurrentPortingState = PortingState.Porting;
            Debug.LogError("exit");
        }*/
    }

    private bool CurrentStateOK(IPortable testSubject)
    {
        return //testSubject.CurrentPortingState == PortingState.NoPorting ||
            testSubject.CurrentPortingState == PortingState.Started ||
            testSubject.CurrentPortingState == PortingState.InProgress;
    }
}
