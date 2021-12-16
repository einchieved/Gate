using UnityEngine;

/// <summary>
/// Destroys a gameobject if its y-coordinate is below the specified value.
/// </summary>
public class KillY : MonoBehaviour
{
    public float maxY = -100f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -100)
        {
            Destroy(gameObject);
        }
    }
}
