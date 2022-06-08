using System;
using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK.Setup;
using UnityEngine;
using Random = System.Random;


public class GunScript : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletHole;
    [SerializeField] private float bulletTime;
    public float spreadAngle = 2; 
    public float numShots = 5;
    public bool IsShotgun = false;
    public bool IsLaser = false;

    private void Start()
    {
        if (IsShotgun == false && IsLaser == false)
        {
            InvokeRepeating("LaunchProjectile", 1f, bulletTime);
        }
        else if (IsShotgun == true)
        {
            Debug.Log("PAPAPAP SHOTGUNNNNNNNNNNN");
            InvokeRepeating("ShotgunShoot", 1f, bulletTime);
        }
        else if (IsLaser == true)
        {
            InvokeRepeating("LaserGun", 1f, bulletTime);
        }
    }

    void LaserGun()
    {
        if (Enemie.GameStarted == true)
        {
            GameObject instance = Instantiate(bullet, bulletHole.transform.position, Quaternion.identity);
        }
    }
    void ShotgunShoot()
    {
        if (Enemie.GameStarted == true)
        {
            var qAngle = Quaternion.AngleAxis((float) (-numShots / 2.0 * spreadAngle), transform.up) * transform.rotation;
            var qDelta = Quaternion.AngleAxis(spreadAngle, transform.up);
         
            for (var i = 0; i < 4; i++) {
                Instantiate(bullet, transform.position,qAngle);
                qAngle = qDelta * qAngle;
            } 
        }
    }
    void LaunchProjectile()
    {
        if (Enemie.GameStarted == true)
        {
            GameObject instance = Instantiate(bullet, bulletHole.transform.position, Quaternion.identity);
        }
        
    }
}
