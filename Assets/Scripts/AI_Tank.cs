using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Tank : MonoBehaviour
{
    private GameStatusManager gameStatusManager;
    public float speedOfEnemy;
    private NavMeshAgent nav;
    public GameObject playerTank;
    public Transform frontTankRaycastPosition;
    public Color noCollsion, playerCollison, enemyCollision, turretRaycasts, environmentCollision;

    public Transform leftTurretSpawnPoint, rightTurretSpawnPoint;

    public float rotationSpeedAI;
    public float delayToActiveNavEneOnEne;
    public float collsionDistance;
    int randomRotation;
    bool rotationDirectionSelected;
    bool navActive = true;
    public bool playerInRange;
    int playerLayer, enemyLayer, environmentLayer, obstacleLayer;
    public GameObject shellPrefab;
    public float shellSpeed;
    public float reloadTime = 1.5f;
    void Awake() 
    {
        gameStatusManager = GameObject.Find("MenusCanvas").GetComponent<GameStatusManager>();
        playerTank = GameObject.FindWithTag("Player");
        nav  = gameObject.GetComponent<NavMeshAgent>();
        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        environmentLayer = LayerMask.NameToLayer("Environment");
    }
    
    
    void Update()
    {
       //Debug.Log("Is nav active- " + navActive);
       if (!gameStatusManager.isPaused)
       {
        nav.isStopped = false;
        Raycast();
        InrangeOfPlayer();
       }
       if (gameStatusManager.isPaused)
       {
           nav.isStopped = true;
       }
    }
    //do the stupid circle raycast thingy. skylar says so and the delay on the shooting thing which i agree with 
    
    //raycast doesnt cast when nav is unactive
    void Raycast()
    {
        if (navActive)
        {
            RaycastHit hit;
            Ray collsionRay = new Ray (frontTankRaycastPosition.transform.position, frontTankRaycastPosition.transform.TransformDirection(Vector3.forward));
            // Does the ray intersect any objects excluding the player layer
            if ((Physics.Raycast(collsionRay, out hit, collsionDistance)) && !playerInRange)
            {
                if ((hit.transform.gameObject.layer == playerLayer))
                {
                    Debug.Log("Hit player");
                    navActive = false;
                    Debug.DrawRay(frontTankRaycastPosition.transform.position, frontTankRaycastPosition.transform.TransformDirection(Vector3.forward) * hit.distance, playerCollison);  
                }

                if ((hit.transform.gameObject.layer == environmentLayer))
                {
                    Debug.Log("Hit environment");
                    Debug.DrawRay(frontTankRaycastPosition.transform.position, frontTankRaycastPosition.transform.TransformDirection(Vector3.forward) * hit.distance, environmentCollision);
                    UseNav();
                    //InrangeOfPlayer();
                    
                }
                if (hit.transform.gameObject.layer == enemyLayer)
                {
                    Debug.Log("Hit enemy");
                    Debug.DrawRay(frontTankRaycastPosition.transform.position, frontTankRaycastPosition.transform.TransformDirection(Vector3.forward) * hit.distance, enemyCollision);
                    InrangeOfEnemy();
                }           
            }
            
            if ((Physics.Raycast(collsionRay, out hit, collsionDistance)) && playerInRange)
            {
                if ((hit.transform.gameObject.layer == playerLayer))
                {
                    nav.speed = 0;
                }

            }
            if (hit.collider == false)
                {
                    Debug.Log("nothing hit");
                    Debug.DrawRay(frontTankRaycastPosition.transform.position, frontTankRaycastPosition.transform.TransformDirection(Vector3.forward) * collsionDistance, noCollsion);
                    UseNav();
                } 
        }
    }

    void UseNav()
    {
        if (navActive)
        {
            nav.enabled = true;
            
            nav.speed = speedOfEnemy;

            nav.destination = playerTank.transform.position; 
        }
    }

    void InrangeOfPlayer()
    {
        if (!navActive)
        {
            nav.enabled = false;
            if (!rotationDirectionSelected)
            {
                randomRotation = Random.Range(0,2);
                //Debug.Log(randomRotation);
                rotationDirectionSelected = true;
            }
            if (playerInRange == false)
            {
            if (randomRotation == 0)
                {
                    transform.Rotate(Vector3.up * -rotationSpeedAI * Time.deltaTime);
                }
                else{
                    transform.Rotate(Vector3.up * rotationSpeedAI * Time.deltaTime);
                }
            }
            TurretsRaycasts();
            
        }
    }
    

    void InrangeOfEnemy()
    {
        nav.speed = 0;
        nav.destination = playerTank.transform.position;
        Invoke("UseNav", delayToActiveNavEneOnEne);
    }

    void TurretsRaycasts()
    {
        RaycastHit hit;
        
        Ray rightTurretRaycast = new Ray (rightTurretSpawnPoint.transform.position, rightTurretSpawnPoint.transform.TransformDirection(Vector3.forward));
        Ray leftTurretRaycast = new Ray (leftTurretSpawnPoint.transform.position, leftTurretSpawnPoint.transform.TransformDirection(Vector3.forward));

        if ((Physics.Raycast(rightTurretRaycast, out hit, Mathf.Infinity)))
            {
                if ((hit.transform.gameObject.layer == playerLayer))
                {
                    playerInRange = true;
                    navActive = true;
                    //Debug.Log("right turret hit");
                    ShootRightTurret();
                }
                Debug.DrawRay(rightTurretSpawnPoint.transform.position, rightTurretSpawnPoint.transform.TransformDirection(Vector3.forward) * hit.distance, turretRaycasts);
            }
        if ((Physics.Raycast(leftTurretRaycast, out hit, Mathf.Infinity)))
        {
            if ((hit.transform.gameObject.layer == playerLayer))
                {
                    playerInRange = true;
                    navActive = true;
                    //Debug.Log("Left turret hit");
                    ShootLeftTurret();
                }
            Debug.DrawRay(leftTurretSpawnPoint.transform.position, leftTurretSpawnPoint.transform.TransformDirection(Vector3.forward) * hit.distance, turretRaycasts);
        }
    }
    void ShootLeftTurret()
    {
    
        GameObject newShell = GameObject.Instantiate(shellPrefab);
        newShell.transform.position = leftTurretSpawnPoint.transform.position;
        newShell.transform.rotation = leftTurretSpawnPoint.transform.rotation;
        newShell.GetComponent<Rigidbody>().velocity = leftTurretSpawnPoint.transform.forward * shellSpeed;
        Invoke("Reload", reloadTime);
        ResetRotation();
        UseNav();

    }

    void ShootRightTurret()
    {
        GameObject newShell = GameObject.Instantiate(shellPrefab);
        newShell.transform.position = rightTurretSpawnPoint.transform.position;
        newShell.transform.rotation = rightTurretSpawnPoint.transform.rotation;
        newShell.GetComponent<Rigidbody>().velocity = rightTurretSpawnPoint.transform.forward * shellSpeed;
        Invoke("Reload", reloadTime);
        ResetRotation();
        UseNav();
    }

    void Reload()
    {
        playerInRange = false;
    }

    void ResetRotation()
    {
        rotationDirectionSelected = false;
    }
          
  
}
