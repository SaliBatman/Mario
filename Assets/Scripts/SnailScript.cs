using Assets.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    // Start is called before the first frame update


    private Rigidbody2D body;
    private Animator animator;
    public float Speed = 1f;

    public  Transform down_collision;

    private bool moveLeft;
     void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        moveLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft)
        {
            body.velocity = new Vector2(-Speed, body.velocity.y);
        }
        else
        {
            body.velocity = new Vector2(Speed, body.velocity.y);

        }
        CheckColision();
    }

    private void CheckColision()
    {
        if (!Physics2D.Raycast(down_collision.position,Vector2.down,0.1f))
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        var temp = transform.localScale;

        moveLeft = !moveLeft;

        if (moveLeft)
        {
            temp.x = Mathf.Abs(temp.x);
        }
        else
        {
            temp.x = -Mathf.Abs(temp.x);
        }

        transform.localScale = temp;

    }
}
