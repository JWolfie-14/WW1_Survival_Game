using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionRaycast : MonoBehaviour
{
    public TankMovement tankMovement;
    public Color raycastColor, collisionColor;
    public float collisonDistance = 1.5f;
    int buildingsLayer, boundaryLayer;

    public bool isFrontRaycast;
    void Awake() {
        buildingsLayer = LayerMask.NameToLayer("Buildings");
        boundaryLayer = LayerMask.NameToLayer("Boundary"); 
    }
    void Update() 
        {
            StopMovement();
        }
    
    void StopMovement()
    {
        RaycastHit hit;
        Ray collsionRay = new Ray (transform.position, transform.TransformDirection(Vector3.forward));
        
        if (Physics.Raycast(collsionRay, out hit, collisonDistance))
        {
            if ((hit.transform.gameObject.layer == buildingsLayer) || (hit.transform.gameObject.layer == boundaryLayer))
                    { 
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * collisonDistance, collisionColor);
                        if(isFrontRaycast)
                        {
                            //isforwardmovementfalse
                            tankMovement.forwardMovementWanted = false;
                        }
                        if(!isFrontRaycast)
                        {
                            Debug.Log("Back raycast detected wall");
                            tankMovement.backwardMovementWanted = false;
                        }
                    }
            
        }
        if (hit.collider == false)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * collisonDistance, raycastColor);
                if(isFrontRaycast)
                        {
                            //isforwardmovementfalse
                            tankMovement.forwardMovementWanted = true;
                        }
                        if(!isFrontRaycast)
                        {
                            tankMovement.backwardMovementWanted = true;
                        }
            }
         
    }
}
