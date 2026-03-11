using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public bool IsPlaced { get; private set; }

    private Rigidbody rb;
    private Collider col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void Place(Transform snapPoint)
    {
        IsPlaced = true;

        transform.position = snapPoint.position;
        transform.rotation = snapPoint.rotation;
        transform.SetParent(snapPoint);

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (col != null)
            col.enabled = false;
    }

    public void Remove()
    {
        IsPlaced = false;

        transform.SetParent(null);

        if (rb != null)
            rb.isKinematic = false;

        if (col != null)
            col.enabled = true;
    }
}
