using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    float horizontalRotation;
    float verticalRotation;
    float frontBackMovement;
    float sidewaysMovement;
    int playerWalkingSpeed = 10;
    int playerRunningSpeed = 3;
    int maxUpHeadMovementInDegrees = 70;
    int maxDownHeadMovementInDegrees = 80;
    float jumpVelocity = 0;
    float jumpStrength = 100;
    CharacterController characterController;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();

    }
    void Update()
    {

        horizontalRotation = Input.GetAxis("Mouse X");
        verticalRotation += Input.GetAxis("Mouse Y");
        verticalRotation = Mathf.Clamp(verticalRotation, -maxDownHeadMovementInDegrees, maxUpHeadMovementInDegrees);

        transform.Rotate(0, horizontalRotation, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(-verticalRotation, 0, 0);

        frontBackMovement = Input.GetAxis("Vertical") * playerWalkingSpeed;
        sidewaysMovement = Input.GetAxis("Horizontal") * playerWalkingSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            frontBackMovement *= playerRunningSpeed;
            sidewaysMovement *= playerRunningSpeed;
        }
        

        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            jumpVelocity = jumpStrength;
        }
        else if(!characterController.isGrounded)
        {
            jumpVelocity += Physics.gravity.y * 0.6f;
        }

        Vector3 playerMovement = new Vector3(sidewaysMovement, jumpVelocity, frontBackMovement);
        characterController.Move(transform.rotation * playerMovement * Time.deltaTime);
    }
}
