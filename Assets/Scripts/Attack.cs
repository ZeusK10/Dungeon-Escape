using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool canDamage=true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        if(hit!=null)
        {
            if(canDamage)
            {
                hit.Damage();
            }
            canDamage = false;
            StartCoroutine(AllowDamage());
        }
    }

    IEnumerator AllowDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }
}
