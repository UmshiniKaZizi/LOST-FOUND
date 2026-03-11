using UnityEngine;

public class Table : MonoBehaviour
{
    [Header("Correct Item")]
    public GameObject correctItem;

    [Header("Snap Point")]
    public Transform holdPoint;

    [Header("Visual")]
    public Renderer tableRenderer;
    public Color highlightColor = Color.green;

    private Color originalColor;
    private bool hasCorrectItem = false;

    private void Start()
    {
        if (tableRenderer != null)
            originalColor = tableRenderer.material.color;

        if (holdPoint == null)
            holdPoint = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == correctItem)
        {
            hasCorrectItem = true;
            SetGlow(true);

            Item item = other.GetComponent<Item>();

            if (item != null)
                item.Place(holdPoint);

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
            tableRenderer.material.color = enable ? highlightColor : originalColor;
    }
}
