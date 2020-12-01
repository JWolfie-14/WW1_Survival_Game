using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public GameStatusManager gameStatusManager;
    public PlayerTrackerManager playerTrackerManager;
    public float movementSpeed;
    private float internalMovementSpeed;
    private float rotationInput, moveInput, cameraRotationInput;
    public float rotationSpeed;
    public GameObject cameraTarget;
    public float cameraOrbitSpeed;
    Rigidbody rb;
    public GameObject leftTread, rightTread;
    Animator leftTreadAnim, rightTreadAnim;
    public float mudPuddleMultiplier = 2;
    public float collisonDistance = 3f;
    public bool forwardMovementWanted = true;
    public bool backwardMovementWanted = false;
    
    // Update is called once per frame
    void Start() 
    {
        internalMovementSpeed = movementSpeed;
        rb = GetComponent<Rigidbody>();
        leftTreadAnim = leftTread.GetComponent<Animator>();
        rightTreadAnim = rightTread.GetComponent<Animator>();
    }
    
    void Update()
    {
        if (!gameStatusManager.isPaused)
        {
            moveInput = Input.GetAxis("Vertical");
            rotationInput = Input.GetAxis("Horizontal");
            cameraRotationInput = Input.GetAxis("Mouse X");
            Debug.Log("forwardMovement: " + forwardMovementWanted);
            Debug.Log("backwardmovement: " + backwardMovementWanted);
            //Debug.Log(rotationInput);
            TankMovementAndAnimation();
            CameraOrbit();
        }
    }

    void TankMovementAndAnimation()
    {
        
        //rotation of tank
        if((rotationInput != 0) && (moveInput == 0))
        {
            transform.Rotate(new Vector3(0,0,rotationInput*rotationSpeed)*Time.deltaTime);
            //rb.MoveRotation(transform.up * rotationInput * rotationSpeed);
            if (rotationInput > 0)
            {
                leftTreadAnim.SetBool("isTankMovingForward", true);
                leftTreadAnim.SetBool("isTankMovingBackwards", false);
                rightTreadAnim.SetBool("isTankMovingForward", false);
                rightTreadAnim.SetBool("isTankMovingBackwards", false);
            }
            else{
                leftTreadAnim.SetBool("isTankMovingForward", false);
                leftTreadAnim.SetBool("isTankMovingBackwards", false);
                rightTreadAnim.SetBool("isTankMovingForward", true);
                rightTreadAnim.SetBool("isTankMovingBackwards", false);
        
            }
        }
        if (rotationInput == 0 && moveInput == 0)
        {
            DeactiveTreadAnimation();
        }
        if (!forwardMovementWanted || !backwardMovementWanted)
        {
            DeactiveTreadAnimation();
        }

        if(moveInput != 0)
        {
            if (moveInput > 0)
            {
                if (forwardMovementWanted)
                {
                    TankForwardAndBackward();
                    leftTreadAnim.SetBool("isTankMovingForward", true);
                    leftTreadAnim.SetBool("isTankMovingBackwards", false);
                    rightTreadAnim.SetBool("isTankMovingForward", true);
                    rightTreadAnim.SetBool("isTankMovingBackwards", false);
                }
            }
            
            if (moveInput < 0)
            {
                if (backwardMovementWanted)
                {
                    TankForwardAndBackward();
                    leftTreadAnim.SetBool("isTankMovingForward", false);
                    leftTreadAnim.SetBool("isTankMovingBackwards", true);
                    rightTreadAnim.SetBool("isTankMovingForward", false);
                    rightTreadAnim.SetBool("isTankMovingBackwards", true);
                }
            }
        }
        
    }

    void DeactiveTreadAnimation()
    {
        leftTreadAnim.SetBool("isTankMovingForward", false);
        leftTreadAnim.SetBool("isTankMovingBackwards", false);
        rightTreadAnim.SetBool("isTankMovingForward", false);
        rightTreadAnim.SetBool("isTankMovingBackwards", false);
    }
    void TankForwardAndBackward()
    {
        transform.Translate(new Vector3(moveInput * internalMovementSpeed, 0, 0) * Time.deltaTime);
    }


    // void TreadAnimation(bool leftForward, bool leftBackward, bool rightForawrd, bool rightBackward)
    // {
    //     leftTreadAnim.SetBool("isTankMovingForward", leftForward);
    //     leftTreadAnim.SetBool("isTankMovingBackwards", leftBackward);
    //     rightTreadAnim.SetBool("isTankMovingForward", rightForawrd);
    //     rightTreadAnim.SetBool("isTankMovingBackwards", rightBackward);
    // }
    void CameraOrbit()
    {
        if (Input.GetAxis("Mouse X") != 0)
        {
            cameraTarget.transform.Rotate(new Vector3(0,cameraRotationInput * cameraOrbitSpeed,0) * Time.deltaTime);  
        }
    }
    void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.CompareTag("Mud"))
                {
                    internalMovementSpeed = internalMovementSpeed / mudPuddleMultiplier;
                }
            if (other.gameObject.CompareTag("River"))
                {
                    playerTrackerManager.DecreasePlayerHealth(100);
                }
        }
    
    void OnTriggerExit(Collider other) 
        {
            if (other.gameObject.CompareTag("Mud"))
                {
                    internalMovementSpeed = movementSpeed;
                }
        }
    
    void OnCollisionEnter(Collision other) 
    {
         if (other.gameObject.CompareTag("Obstacles"))
         {
             other.gameObject.GetComponent<ObstaclesManager>().ObstacleTakenDamage(100);
         }
    }
}
