using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
  
public class GunProjectile : MonoBehaviour
{   //bullet object
    public GameObject bullet;

    public float shootForce;
    //Gun stats
    public float timeBetweenShooting, reloadTime;
    public int magazineSize;
    public int bulletsLeft, bulletsShot;
    public bool allowButtonHold;

    private bool readyToShoot, shooting, reloading;

    //reference camera
    public Camera PlayerCam;
    public Transform attackPoint;

    //Graphics
    public TextMeshProUGUI ammunitionDisplay;

    private bool allowInvoke = true;

    private void Awake()
    {   
        // make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;

    }

    private void Update()
    {
        myInput();

        //set ammo display
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft + "/" + magazineSize);
    }

    private void myInput()
    {   
        //check if allowed to hold down button to shoot
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        //Automatically reload when shooting with no ammo
        if (readyToShoot && shooting && !reloading && bulletsLeft == 0) Reload();

        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //Set bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }


    }

    private void Shoot()
    {
        readyToShoot = false;

        //Find the exact hit position of bullet using a raycast
        Ray ray = PlayerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //Check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        //calculates direction from attackPoint to targetPoint
        Vector3 direction = targetPoint - attackPoint.position;

        //instantiate bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        currentBullet.tag = "Bullet";
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = direction.normalized;

        //add forces to the bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);

        //bullets in mag decrease by 1
        bulletsLeft--;
        //bullets shot increase by 1
        bulletsShot++; 

        //Invokes ResetShot function
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
    }

    private void ResetShot()
    {
        //allows shooting and invoke again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}   
