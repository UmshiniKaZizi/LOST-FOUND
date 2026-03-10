using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;
    private Player player;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        player = GetComponent<Player>();

        // Movement
        onFoot.Jump.performed += ctx =>
        {
            Debug.Log("Jump pressed");
            motor.Jump();
        };

        onFoot.Crouch.performed += ctx =>
        {
            Debug.Log("Crouch pressed");
            motor.Crouch();
        };

        onFoot.Sprint.performed += ctx =>
        {
            Debug.Log("Sprint pressed");
            motor.Sprint();
        };

        // Interactions
        onFoot.PickUp.performed += ctx =>
        {
            Debug.Log("E pressed - PickUp triggered");
            player.PickUp();
        };

        onFoot.Drop.performed += ctx =>
        {
            Debug.Log("Drop pressed");
            player.Drop();
        };

        //onFoot.Use.performed += ctx => player.Use();
    }

    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        Debug.Log("Input Enabled");
        onFoot.Enable();
    }

    private void OnDisable()
    {
        Debug.Log("Input Disabled");
        onFoot.Disable();
    }
}
