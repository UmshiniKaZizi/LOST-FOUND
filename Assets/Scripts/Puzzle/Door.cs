using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    [Header("Delay before door disappears (seconds)")]
    public float delay = 5f;

    private bool isOpening = false;

    public void OpenDoor()
    {
        if (!isOpening)
        {
            isOpening = true;
            StartCoroutine(OpenDoorWithDelay());
        }
    }

    private IEnumerator OpenDoorWithDelay()
    {
        Debug.Log("Door will disappear in " + delay + " seconds");
        yield return new WaitForSeconds(delay);

        Debug.Log("Door is now inactive!");
        gameObject.SetActive(false);
    }
}
