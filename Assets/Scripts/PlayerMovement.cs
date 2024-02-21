using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 2.0f;

    // dirty
    public GameObject UIManagerObj;
    private UIManager Manager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Manager = UIManagerObj.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.GamePlaying)
        {
            // up down left right
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddRelativeForce(Vector3.forward * moveSpeed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddRelativeForce(Vector3.back * moveSpeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddRelativeForce(Vector3.left * moveSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddRelativeForce(Vector3.right * moveSpeed);
            }
            // stop movement on no keys
            if (!Input.GetKey(KeyCode.W) &&
                !Input.GetKey(KeyCode.S) &&
                !Input.GetKey(KeyCode.A) &&
                !Input.GetKey(KeyCode.D))
            {
                rb.velocity = Vector3.zero;
            }

            // to avoid infinite acceleration
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }
    }
}