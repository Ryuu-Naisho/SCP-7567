using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class wCharacterController : MonoBehaviour
{
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private float jumpSpeed = 8.0f;
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] float lookXLimit = 45.0f;

    [SerializeField] private float speed = 7.5f;
    private Camera playerCamera;
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private Vector2 rotation = Vector2.zero;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        rotation.y = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;


        characterController.Move(moveDirection * Time.deltaTime);


        if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
        }
    }
}
