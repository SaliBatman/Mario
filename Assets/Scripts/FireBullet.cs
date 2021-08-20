using Assets.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private float speed = 10f;
    private Animator anim;

     void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine(DisableBullet(5f));
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var tempPosition = transform.position;
        tempPosition.x += speed * Time.deltaTime;

        transform.position = tempPosition;
    }

    IEnumerator DisableBullet(float time)
    {

        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.BeetleTag || collision.tag == Tags.SnailTag)
        {
            anim.Play("Explode");
            StartCoroutine(DisableBullet(0.1f));
        }
    }
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

}


