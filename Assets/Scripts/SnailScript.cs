using Assets.Enums;
using Assets.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    // Start is called before the first frame update


    private Rigidbody2D body;
    private Animator animator;
    public float Speed = 1f;
    private bool stunned;
    private bool canMove;
    public Transform down_collision, left_collision, right_collision, top_collision;
    public LayerMask playerLayer;
    private Vector3 left_position, right_position;

    private bool moveLeft;
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        left_position = left_collision.position;
        right_position = right_collision.position;
    }
    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                body.velocity = new Vector2(-Speed, body.velocity.y);
            }
            else
            {
                body.velocity = new Vector2(Speed, body.velocity.y);

            }
        }

        CheckColision();
    }

    private void CheckColision()
    {

        var leftHit = Physics2D.Raycast(left_collision.position, Vector2.left, 0.1f, playerLayer);
        var rightHit = Physics2D.Raycast(right_collision.position, Vector2.right, 0.1f, playerLayer);
        var topHit = Physics2D.OverlapCircle(top_collision.position, 0.2f, playerLayer);

        if (topHit != null)
        {
            if (topHit.gameObject.CompareTag(Tags.PlayerTag))
            {
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                    stunned = true;
                    canMove = false;
                    body.velocity = new Vector2(0, 0);

                    if (tag == Tags.BeetleTag)
                    {
                        StartCoroutine(Dead(0.5f));
                    }
                    animator.Play("Stuned");

                }
            }
        }

        if (leftHit)
        {
            if (leftHit.collider.gameObject.CompareTag(Tags.PlayerTag))
            {
                if (!stunned)
                {
                    print("kirtoosh");
                }
                else
                {

                    if (tag != Tags.BeetleTag)
                    {
                        body.velocity = new Vector2(-15f, body.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }
        if (rightHit)
        {
            if (rightHit.collider.gameObject.CompareTag(Tags.PlayerTag))
            {
                if (!stunned)
                {
                    print("kirtoosh");
                }
                else
                {
                    if (tag != Tags.BeetleTag)
                    {
                        body.velocity = new Vector2(15f, body.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }
        if (!Physics2D.Raycast(down_collision.position, Vector2.down, 0.01f))
        {
            ChangeDirection();
        }
    }

    private IEnumerator Dead(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);

    }

    private void ChangeDirection()
    {
        var temp = transform.localScale;

        moveLeft = !moveLeft;

        if (moveLeft)
        {

            left_position = left_collision.position;
            right_position = right_collision.position;

            temp.x = Mathf.Abs(temp.x);
        }
        else
        {
            left_position = right_collision.position;
            right_position = left_collision.position;
            temp.x = -Mathf.Abs(temp.x);
        }

        transform.localScale = temp;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.Bullet)
        {
            if (tag == Tags.BeetleTag)
            {
                canMove = false;
                StartCoroutine(Dead(0.1f));
                body.velocity = new Vector2(0, 0);
                animator.Play("Stuned");

            }
            if (tag == Tags.SnailTag)
            {
                if (!stunned)
                {
                    canMove = false;
                    body.velocity = new Vector2(0, 0);
                    animator.Play("Stuned");
                    stunned = true;


                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
