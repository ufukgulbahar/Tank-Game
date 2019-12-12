using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBeHaviour : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    float TimeFromLastShoot;

    public void Shoot(float shootfreq)
    {
        if ((TimeFromLastShoot += Time.deltaTime) >= 1f / shootfreq)
        {
            InstantiateBullet();
            TimeFromLastShoot = 0;
        }
    }

    public void Shoot()
    {
        InstantiateBullet();
    }

    private void InstantiateBullet()
    {

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(1200f * transform.forward);

    }
}
