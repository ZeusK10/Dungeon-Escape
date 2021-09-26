using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;
    protected Transform player;
    protected bool isDead;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected bool isHit = false;

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        Init();
    }


    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat")==false)
        {
            return;
        }

        if(isDead==false)
        {
            Movement();
        }
        
    }

    public virtual void Movement()
    {
        if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else if (currentTarget == pointB.position)
        {
            sprite.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            anim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            anim.SetTrigger("Idle");
        }

        if(isHit==false)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }


        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if (distance > 2)
        {
            isHit = false;
            anim.SetBool("InCombat", false);

        }
        if(anim.GetBool("InCombat")==true)
        {
            if (player.transform.localPosition.x - transform.localPosition.x > 0)
            {
                sprite.flipX = false;
            }
            else if (player.transform.localPosition.x - transform.localPosition.x < 0)
            {
                sprite.flipX = true;
            }
        }
    }
}
