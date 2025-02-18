using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity;
    private Vector2 mouseInput;
    private float pitch;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(Vector3.up, mouseInput.x * sensitivity * Time.deltaTime);
        pitch -= mouseInput.y * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -75f, 75f);
        transform.localEulerAngles = new Vector3(pitch, transform.localEulerAngles.y, 0f);

    }

    public void OnMouseMove (InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }
}
