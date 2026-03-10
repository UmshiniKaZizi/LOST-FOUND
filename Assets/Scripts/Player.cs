using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask pickableLayerMask;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private GameObject pickUpUI;

    [SerializeField] [Min(1)]
    private float hitRange = 3f;

    [SerializeField] private Transform pickUpParent;
    [SerializeField] private GameObject inHandItem;

    //[SerializeField] private AudioSource pickUpSource;

    private RaycastHit hit;

    public void AddHealth(int healthBoost)
    {
        Debug.Log($"Health boosted by {healthBoost}");
    }

    void Update()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);

        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            pickUpUI.SetActive(false);
        }

        if (inHandItem != null)
            return;

        if (Physics.Raycast(
            playerCameraTransform.position,
            playerCameraTransform.forward,
            out hit,
            hitRange,
            pickableLayerMask))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            pickUpUI.SetActive(true);
        }
    }

   public void PickUp()
{
    if (hit.collider != null && inHandItem == null)
    {
        IPickable pickableItem = hit.collider.GetComponentInParent<IPickable>();

        if (pickableItem != null)
        {
            //pickUpSource.Play();

            inHandItem = pickableItem.PickUp();

            inHandItem.transform.SetParent(pickUpParent);
            inHandItem.transform.localPosition = Vector3.zero;
            inHandItem.transform.localRotation = Quaternion.identity;

            Rigidbody rb = inHandItem.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true;
        }
    }
}


    public void Drop()
    {
        if (inHandItem != null)
        {
            Rigidbody rb = inHandItem.GetComponent<Rigidbody>();

            inHandItem.transform.SetParent(null);

            if (rb != null)
                rb.isKinematic = false;

            inHandItem = null;
        }
    }

    public void Use()
    {
        if (inHandItem != null)
        {
            IUsable usable = inHandItem.GetComponent<IUsable>();

            if (usable != null)
            {
                usable.Use(this.gameObject);
            }
        }
    }
}
