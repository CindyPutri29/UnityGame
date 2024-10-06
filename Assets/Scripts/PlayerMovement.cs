using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    public float lookSpeed;
    public CharacterController controller;
    public Transform cameraTransform;
    public Transform camLookAtPivot;
    public Transform camLookAt;
    public Animator animator;
    public LayerMask groundMask;

    float camLookAtPivotX;
    bool isGraunded;


    private void Start()
    {
        cameraTransform.SetParent(this.transform);
    }

    private void Update()
    {
        Move();
        Look();
        Fall();
    }
    void Move()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(inputHorizontal, 0, inputVertical).normalized;
        movement = transform.TransformDirection(movement);

        controller.Move(movement * MoveSpeed * Time.deltaTime);
        animator.SetFloat("Horizontal", inputHorizontal);
        animator.SetFloat("Vertical", inputVertical);
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.eulerAngles += new Vector3(0, mouseX * lookSpeed * Time.deltaTime, 0);
        //camLookAtPivot.eulerAngles += new Vector3(mouseY *  lookSpeed * Time.deltaTime, 0, 0);
        cameraTransform.LookAt(camLookAt);

        camLookAtPivotX += -mouseY * lookSpeed * Time.deltaTime;
        camLookAtPivotX = Mathf.Clamp(camLookAtPivotX, -30, 30);
        camLookAtPivot.localEulerAngles = new Vector3(camLookAtPivotX, 0, 0);
    }

    void Fall()
    {
        isGraunded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
        if (!isGraunded)
        {
            float height = transform.position.y;
            height -= 5;
            Vector3 fallMovement = new Vector3(0, height, 0);
            controller.Move(fallMovement * Time.deltaTime);
        }
    }
}
