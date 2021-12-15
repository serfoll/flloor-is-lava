using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField]
    GameObject stairs;
    [SerializeField]
    float fallSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stairs.transform.Translate(Vector3.down * fallSpeed);
        }
    }
}
