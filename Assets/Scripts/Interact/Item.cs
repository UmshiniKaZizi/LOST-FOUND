using UnityEngine;

public class Item : MonoBehaviour, IPickable
{
    public bool KeepWorldPosition => false;

    private Rigidbody rb;
    private bool isPlaced = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public GameObject PickUp()
    {
        if (isPlaced) return null;
        return gameObject;
    }

    public void Place(Transform snapPoint)
    {
        isPlaced = true;

        transform.position = snapPoint.position;
        transform.rotation = snapPoint.rotation;

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
