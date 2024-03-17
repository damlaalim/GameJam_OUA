using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator animController;
    public float moveSpeed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private void Start()
    {
        if(animController == null)
        {
            animController = GetComponentInChildren<Animator>();
        }
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            direction *= 2;
            Direction(direction);
            float speed = direction.magnitude;
            animController.SetFloat("Speed", speed);
            Debug.Log(speed);
        }
        else
        {
            Direction(direction);
            float speed = direction.magnitude;
            animController.SetFloat("Speed", speed);
            Debug.Log(speed);
        }
    }
    public void Direction(Vector3 direction)
    {
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            if(direction.magnitude == 2)
            {
                controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime * 2);
            }
            else
            {
                controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
            }
        }
    }
}
