using UnityEngine;

public class Table : MonoBehaviour
{
    [Header("Correct Item for this table")]
    public GameObject correctItem;

    [Header("RigidBody of the correct item")]
    [SerializeField] private Rigidbody correctItemRigidbody;

    [Header("Visuals")]
    public Renderer tableRenderer;
    public Color highlightColor = Color.green;

    [Header("Snap point for the item (center of table)")]
    public Transform holdPoint;

    private Color originalColor;
    private bool hasCorrectItem = false;

    private void Start()
    {
        if (tableRenderer != null)
            originalColor = tableRenderer.material.color;

        // If no holdPoint assigned, default to table center
        if (holdPoint == null)
            holdPoint = this.transform;

        // Ensure Rigidbody is assigned
        if (correctItemRigidbody == null)
            Debug.LogWarning("Please assign the correct item's Rigidbody in the inspector for " + name);
    }

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == correctItem)
        {
            hasCorrectItem = true;
            SetGlow(true);

            // Snap the item to the holdPoint
            other.transform.SetParent(holdPoint);
            other.transform.localPosition = Vector3.zero;
            other.transform.localRotation = Quaternion.identity;

            // Only disable physics for the assigned Rigidbody
            if (correctItemRigidbody != null)
            {
                correctItemRigidbody.isKinematic = true;
                correctItemRigidbody.velocity = Vector3.zero;
                correctItemRigidbody.angularVelocity = Vector3.zero;
            }

            TableManager.Instance.CheckAllTables();
            Debug.Log("Item snapped and physics disabled for this item only!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == correctItem)
        {
            hasCorrectItem = false;
            SetGlow(false);

            if (correctItemRigidbody != null)
                correctItemRigidbody.isKinematic = false;

            TableManager.Instance.CheckAllTables();
        }
    }

    public bool HasCorrectItem()
    {
        return hasCorrectItem;
    }

    private void SetGlow(bool enable)
    {
        if (tableRenderer != null)
        {
            tableRenderer.material.color = enable ? highlightColor : originalColor;
        }
    }
}
