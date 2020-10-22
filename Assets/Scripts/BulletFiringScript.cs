using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFiringScript : MonoBehaviour
{   
    public GameStatusManager gameStatusManager;
    private Vector3 offset;
    private Vector3 offsetRotation;

    private bool canFire = true;
    
    [Header("Front gun variables")]
    public GameObject bulletPrefab;
    public GameObject bulletSpawnPoint;
    public float bulletSpeed;
    private bool frontGunActive = true;
    public float rateofFireFrontGunTime;
    public GameObject frontGun;
    public float frontGunRotationSpeed;
    public GameObject frontGunCam; 
    //****************shell variables**********************
    [Header("Shell/canons variables")]
    public GameObject shellPrefab;
    public float turretRotationSpeed;
    public float cannonRotationSpeed;
    public GameObject leftCannonSpawnPoint;
    public GameObject leftTurret;
    public GameObject leftCannon;
    public GameObject rightCannonSpawnPoint;
    public GameObject rightTurret;
    public GameObject rightCannon;
    float tiltXLeftCannon;
    public float shellSpeed;
    private bool leftCannonActive = false;
    private bool rightCannonActive = false;
    public float rateOfFireCannonTime;
    public GameObject leftCannonCam;
    public GameObject rightCannonCam;

    public bool UIActive = false;
    void Start()
    {
    }
    /*  
        1 is front machine gun
        2 is left canon
        3 is right canon
    */
    // Update is called once per frame
    void Update()
    {
        if (!UIActive && !gameStatusManager.isPaused)
        {
        if (Input.GetKeyDown("1"))
        {
            frontGunActive = true;
            leftCannonActive = false;
            rightCannonActive = false;
            frontGunCam.SetActive(true);
            leftCannonCam.SetActive(false);
            rightCannonCam.SetActive(false);
        }

        if (Input.GetKeyDown("2"))
        {
            //left cannon
            frontGunActive = false;
            leftCannonActive = true;
            rightCannonActive = false;
            frontGunCam.SetActive(false);
            leftCannonCam.SetActive(true);
            rightCannonCam.SetActive(false);
        }

        if (Input.GetKeyDown("3"))
        {
            //right canon
            frontGunActive = false;
            leftCannonActive = false;
            rightCannonActive = true;
            frontGunCam.SetActive(false);
            leftCannonCam.SetActive(true);
            rightCannonCam.SetActive(true);
        }

        if (Input.GetButton("Fire1") && (canFire == true) && (frontGunActive == true))
        {
            canFire = false;
            GameObject newBullet = GameObject.Instantiate(bulletPrefab);
            newBullet.transform.position = bulletSpawnPoint.transform.position;
            newBullet.transform.rotation = bulletSpawnPoint.transform.rotation;
            newBullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.transform.forward * bulletSpeed;
            Invoke("RateOfFire", rateofFireFrontGunTime);
        }

        if (Input.GetButton("Fire1") && (canFire == true) && (leftCannonActive == true))
        {
            canFire = false;
            GameObject newShell = GameObject.Instantiate(shellPrefab);
            newShell.transform.position = leftCannonSpawnPoint.transform.position;
            newShell.transform.rotation = leftCannonSpawnPoint.transform.rotation;
            newShell.GetComponent<Rigidbody>().velocity = leftCannonSpawnPoint.transform.forward * shellSpeed;
            Invoke("RateOfFire", rateOfFireCannonTime);
        }

        if (Input.GetButton("Fire1") && (canFire == true) && (rightCannonActive == true))
        {
            canFire = false;
            GameObject newShell = GameObject.Instantiate(shellPrefab);
            newShell.transform.position = rightCannonSpawnPoint.transform.position;
            newShell.transform.rotation = rightCannonSpawnPoint.transform.rotation;
            newShell.GetComponent<Rigidbody>().velocity = rightCannonSpawnPoint.transform.forward * shellSpeed;
            Invoke("RateOfFire", rateOfFireCannonTime);
        }

        tiltXLeftCannon = (leftCannon.transform.eulerAngles.x > 180f) ? leftCannon.transform.eulerAngles.x - 180 : leftCannon.transform.eulerAngles.x;
        }
        
    }
    void LateUpdate() 
    {
        if (leftCannonActive == true) 
        {
            if ((Input.GetAxis("Mouse X") != 0))
            {
                if (Input.GetAxis("Mouse X") >= 0 && leftTurret.transform.eulerAngles.z < 315)
                    {
                        leftTurret.transform.Rotate(Vector3.forward * turretRotationSpeed * Time.deltaTime, Space.Self);
                    }

                if (Input.GetAxis("Mouse X") <= 0  && leftTurret.transform.eulerAngles.z > 180)
                    {
                        leftTurret.transform.Rotate(Vector3.forward * -turretRotationSpeed * Time.deltaTime, Space.Self);
                    }
            }
            Debug.Log("leftcanon; " + leftCannon.transform.localEulerAngles);
            // if ((Input.GetAxis("Mouse Y") != 0))// && (Input.GetAxis("Mouse X") == 0))
            // {
            //     if (Input.GetAxis("Mouse Y") >= 0)// && tiltXLeftCannon < -82)
            //         {
            //             leftCannon.transform.Rotate(Vector3.left * cannonRotationSpeed * Time.deltaTime, Space.Self);
            //         }

            //     if (Input.GetAxis("Mouse Y") <= 0)// && tiltXLeftCannon > -95)
            //         {
            //             leftCannon.transform.Rotate(Vector3.left * -cannonRotationSpeed * Time.deltaTime, Space.Self); 
            //         }
            // }
        }

        if (rightCannonActive == true) 
        {
            //Debug.Log("right cannon; " + rightCannon.transform.localRotation);
            if ((Input.GetAxis("Mouse X") != 0))
            {
                if (Input.GetAxis("Mouse X") >= 0 && rightTurret.transform.localEulerAngles.y < 195)
                    {
                        rightTurret.transform.Rotate(Vector3.forward * turretRotationSpeed * Time.deltaTime, Space.Self);
                    }

                if (Input.GetAxis("Mouse X") <= 0 && rightTurret.transform.localEulerAngles.y > 65)
                    {
                        rightTurret.transform.Rotate(Vector3.forward * -turretRotationSpeed * Time.deltaTime, Space.Self);
                    }
            }

            // if ((Input.GetAxis("Mouse Y") != 0))// && (Input.GetAxis("Mouse X") == 0))
            // {
            //     if (Input.GetAxis("Mouse Y") >= 0)
            //         {
            //             rightCannon.transform.Rotate(Vector3.left * cannonRotationSpeed * Time.deltaTime, Space.Self);
            //         }

            //     if (Input.GetAxis("Mouse Y") <= 0)
            //         {
            //             rightCannon.transform.Rotate(Vector3.left * -cannonRotationSpeed * Time.deltaTime, Space.Self);
            //         }
            // }
        }

        if (frontGunActive == true) 
        {
            // if (Input.GetKeyDown("1"))
            //     {
            //         frontGunActive = true;
            //         leftCannonActive = false;
            //         rightCannonActive = false;
            //         frontGunCam.SetActive(true);
            //         leftCannonCam.SetActive(false);
            //         rightCannonCam.SetActive(false);
            //     }
            
            // if ((Input.GetAxis("Mouse X") != 0))
            // {
            //     if (Input.GetAxis("Mouse X") >= 0)
            //         {
            //             frontGun.transform.Rotate(Vector3.left * frontGunRotationSpeed * Time.deltaTime, Space.Self);
            //         }

            //     if (Input.GetAxis("Mouse X") <= 0)
            //         {
            //             frontGun.transform.Rotate(Vector3.left * -frontGunRotationSpeed * Time.deltaTime, Space.Self);
            //         }
            // }

            // if ((Input.GetAxis("Mouse Y") != 0))// && (Input.GetAxis("Mouse X") == 0))
            // {
            //     if (Input.GetAxis("Mouse Y") >= 0)
            //         {
            //             frontGun.transform.Rotate(Vector3.up * frontGunRotationSpeed * Time.deltaTime, Space.Self);
            //         }

            //     if (Input.GetAxis("Mouse Y") <= 0)
            //         {
            //             frontGun.transform.Rotate(Vector3.up * -frontGunRotationSpeed * Time.deltaTime, Space.Self);
            //         }
            // }
        }
    }
    void RateOfFire()
    {
        canFire = true;
    }

    

}
