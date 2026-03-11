using System.Collections;
using UnityEngine;

public class SimplePlatformTutorial : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] float changeDirectionDelay = 1f;

    private Transform destinationTarget;
    private Transform departTarget;

    private float startTime;
    private float journeyLength;
    private bool isWaiting;

    public Vector3 DeltaMovement { get; private set; }
    private Vector3 previousPosition;

    void Start()
    {
        departTarget = startPoint;
        destinationTarget = endPoint;

        startTime = Time.time;
        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position);
        previousPosition = transform.position;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (isWaiting)
        {
            DeltaMovement = Vector3.zero;
            return;
        }

        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;

        Vector3 newPosition = Vector3.Lerp(departTarget.position, destinationTarget.position, fractionOfJourney);
        DeltaMovement = newPosition - previousPosition;
        transform.position = newPosition;
        previousPosition = newPosition;

        if (fractionOfJourney >= 1f)
        {
            isWaiting = true;
            StartCoroutine(ChangeDelay());
        }
    }

    void ChangeDestination()
    {
        if (destinationTarget == endPoint)
        {
            departTarget = endPoint;
            destinationTarget = startPoint;
        }
        else
        {
            departTarget = startPoint;
            destinationTarget = endPoint;
        }
    }

    IEnumerator ChangeDelay()
    {
        yield return new WaitForSeconds(changeDirectionDelay);
        ChangeDestination();
        startTime = Time.time;
        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position);
        isWaiting = false;
    }
}