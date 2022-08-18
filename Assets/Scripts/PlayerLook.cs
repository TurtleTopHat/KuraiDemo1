using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public Transform playerBody;
    public float sensitivity = 100f;
    private float xRotation = 0f;

    PlayerControls controls;
    Vector2 look;

    private void Awake()
    {
        controls = new PlayerControls();

        //Look.
        controls.Gameplay.Look.performed += ctx => look = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => look = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Is my controller taking input?
        //Debug.Log("X(" + move.x + "," + move.y + ") " + "Y(" + look.x + "," + look.y + ")");
        float lookX = look.x * sensitivity * Time.deltaTime;
        float lookY = look.y * sensitivity * Time.deltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * lookX);
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
