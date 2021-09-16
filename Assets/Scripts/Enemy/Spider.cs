using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    Vector3 _currentTraget;
    SpriteRenderer _spiderRenderer;
    Animator _spiderAnimator;

    private void Start()
    {
        _spiderRenderer = GetComponentInChildren<SpriteRenderer>();
        _spiderAnimator = GetComponentInChildren<Animator>();
    }

    public override void Update()
    {
        if(_spiderAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            Movement();
        }
        
    }

    void Movement()
    {
        if(_currentTraget==pointA.position)
        {
            _spiderRenderer.flipX = true;
        }
        else if(_currentTraget==pointB.position)
        {
            _spiderRenderer.flipX = false;
        }

        if(transform.position==pointA.position)
        {
            _currentTraget = pointB.position;
            _spiderAnimator.SetTrigger("Idle");
        }
        else if(transform.position==pointB.position)
        {
            _currentTraget = pointA.position;
            _spiderAnimator.SetTrigger("Idle");
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTraget, speed * Time.deltaTime);
    }
}
