using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{


    // Update is called once per frame
    public GameObject fireBullet;
    void Update()
    {
        BulletShooting();
    }

    private void BulletShooting()
    {
   
            if (Input.GetKeyDown(KeyCode.W))
            {
                GameObject bullet = Instantiate(fireBullet, transform.position, Quaternion.identity);
                bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
            }
    }
}
