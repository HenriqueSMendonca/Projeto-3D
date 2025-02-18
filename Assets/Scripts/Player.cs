using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject nuke;
    [SerializeField] private InputAction press;
    private Rigidbody rb;
    private Vector2 moveInput;
    public float jumpPower = 5f;
    public Transform point;
    public float shootForce;

    private void Awake()
    {
        press.Enable();
        press.performed += _ => { Shoot(); };
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Andando();
    }

    void Andando()
    {
        Vector3 move = cam.transform.forward * moveInput.y + cam.transform.right * moveInput.x;
        move.y = 0;
        rb.AddForce(move.normalized * speed, ForceMode.VelocityChange);
    }
    public void Onmove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (rb.velocity.y == 0)
        {
            rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }
    public void Shoot()
    {
        Ray ray =  cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        } else
        {
            targetPoint = ray.GetPoint(75);
        }

        Vector3 direction = targetPoint - point.position;
      GameObject missile =  Instantiate(nuke, point.position, Quaternion.identity);
        missile.transform.forward = direction.normalized;
        missile.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);
    }
}
