using UnityEngine;

public class UpInTheAir : MonoBehaviour
{

    void Start()
    {
        Rigidbody rb;
        if (TryGetComponent<Rigidbody>(out rb))
        {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
        else
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }
}
