using Photon.Pun;
using UnityEngine;

public class GroundCheck : MonoBehaviourPun
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine && animator != null)
        {
            animator.SetBool("isGrounded", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (photonView.IsMine && animator != null)
        {
            animator.SetBool("isGrounded", false);
        }
    }
}
