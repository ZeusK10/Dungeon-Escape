using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy
{
    private Vector3 _currentTarget;
    private Animator enemyAnim;
    private SpriteRenderer _enemyRenderer;

    private void Start()
    {
        enemyAnim = GetComponentInChildren<Animator>();
        _enemyRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public override void Update()
    {
        if(this.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Walk_anim"))
        {
            Movement();
        }
        
    }

    void Movement()
    {
        if (_currentTarget == pointA.position)
        {
            _enemyRenderer.flipX = true;
        }
        else if (_currentTarget == pointB.position)
        {
            _enemyRenderer.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            _currentTarget = pointB.position;
            enemyAnim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            _currentTarget = pointA.position;
            enemyAnim.SetTrigger("Idle");
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
    }

}
