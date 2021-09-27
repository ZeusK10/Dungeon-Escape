using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy,IDamageable
{
    public int Health { get; set; }
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }
    public void Damage()
    {
        Health--;
        isHit = true;
        anim.SetTrigger("Hit");
        anim.SetBool("InCombat", true);
        if(Health<1)
        {
            isDead = true;
            anim.SetTrigger("Death");

            GameObject dmd = Instantiate(diamond, transform.position, Quaternion.identity);
            dmd.GetComponent<Diamond>().GetValue(gems);
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //Destroy(this.gameObject);
        }
    }
}
