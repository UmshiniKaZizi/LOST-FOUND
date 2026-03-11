using UnityEngine;

public class PlatformPlayerCarrier : MonoBehaviour
{
    private SimplePlatformTutorial platform;
    private PlayerMotor playerMotor;
    private bool playerOnPlatform;

    void Start()
    {
        platform = GetComponentInParent<SimplePlatformTutorial>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerMotor = other.GetComponent<PlayerMotor>();
            playerOnPlatform = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
            playerMotor = null;
        }
    }

    public Vector3 GetDelta()
    {
        return playerOnPlatform ? platform.DeltaMovement : Vector3.zero;
    }
}