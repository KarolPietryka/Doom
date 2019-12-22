using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    public float playerWalkingSpeed = 5f;
    public float playerRunningSpeed = 15f;
    public float jumpStrenght;
    public float verticalRotationLimit = 80f;


    float forwardMovement;
    float sidewaysMovement;
    float verticalVelocity;
    float verticalRotation = 0;

    CharacterController characterController;

    public flashScreen flesh;
	void Awake ()
    {
        characterController = GetComponent<CharacterController>();

        jumpStrenght = 7.0f;
    }
	
	void Update ()
    {
        //Head movement//
        float horizontalRotation = Input.GetAxis("Mouse X");
        verticalRotation -= Input.GetAxis("Mouse Y");
        //Horizontal movement
        transform.Rotate(0, horizontalRotation, 0);
        //Vertical movement
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);//limitate valiable
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);//Main camera->Transform->localRotation(in relation to parrent)

        //Character movement//
        //Running/Walking
        forwardMovement = Input.GetAxis("Vertical");// * playerWalkingSpeed;
        sidewaysMovement = Input.GetAxis("Horizontal");// * playerWalkingSpeed;

        Vector3 playerMovement = new Vector3(sidewaysMovement, 0, forwardMovement);
        playerMovement = transform.TransformDirection(playerMovement);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerMovement *= playerRunningSpeed;
        }
        else
        {
            playerMovement *= playerWalkingSpeed;
        }
        //Jumping

        verticalVelocity += Physics.gravity.y * Time.deltaTime;
        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            verticalVelocity = jumpStrenght;
        }
        playerMovement.y = verticalVelocity;
        //Move execute 
        characterController.Move(playerMovement * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)//Step on object that  box colider is set on is trigger
    {
        if (other.CompareTag("HpBonus"))
        {
            float hpBonus = other.GetComponent<HpBonus>().bonusHP;
            GetComponent<PlayerHealth>().addHealth(hpBonus);
        }
        else if (other.CompareTag("ArmorBonus"))
        {
            float armorBonus = other.GetComponent<ArmorBonus>().bonusArmor;
            GetComponent<PlayerHealth>().addArmor(armorBonus);
        }
        else if (other.CompareTag("AmmoBonus"))
        {
            int ammoBonus = other.GetComponent<AmmoBonus>().bonusAmmo;
            transform.Find("Weapons").Find("Thompson_Gun").GetComponent<Thompson_Gun>().addAmmo(ammoBonus);
        }
        if (other.CompareTag("HpBonus") || other.CompareTag("ArmorBonus") || other.CompareTag("AmmoBonus"))
        {
            flesh.tookBonus();
            Destroy(other.gameObject);
        }
    }
}
