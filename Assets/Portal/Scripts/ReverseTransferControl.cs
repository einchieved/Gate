using UnityEngine;
using static IPortable;

public class ReverseTransferControl : MonoBehaviour
{
    private Collider coll;

    private void OnTriggerEnter(Collider other)
    {
        IPortable portable = other.GetComponent<IPortable>();
        if (portable != null && portable.CurrentPortingState == PortingState.InProgress)
        {
            portable.CurrentPortingState = PortingState.Porting;
            /*
            if (coll == null)
            {
                coll = other;
            }
            else if (coll == other)
            {
                portable.CurrentPortingState = PortingState.EndingPositive;
                coll = null;
            }*/
        }
    }
}
