using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy,IDamageable
{
    public int Health { get; set; }
    [SerializeField]
    private GameObject acid;
    public override void Init()
    {
        base.Init();
        Health = health;
    }

    public override void Update()
    {

    }

    public void Damage()
    {
        Health--;
        if(Health<1)
        {
            anim.SetTrigger("Death");
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //Destroy(this.gameObject.transform.parent.gameObject);
        }
    }

    public void Attack()
    {
        Instantiate(acid, transform.position, Quaternion.identity);
    }
}
