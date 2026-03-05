using UnityEngine;

public class PlayerLook : MonoBehaviour
{   
    public Camera cam;
    public float xRotation = 0;
    public float ySensetivity = 30f;
    public void ProcessLook(Vector2 input )
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime) * ySensetivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(xRotation,0,0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime)* ySensetivity);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
