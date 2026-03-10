using UnityEngine;

public class Item : MonoBehaviour, IPickable
{
    public bool KeepWorldPosition => false;

    public GameObject PickUp()
    {
        return gameObject;
    }
}
