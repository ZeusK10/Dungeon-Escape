using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private int gemsValue=1;

    public void GetValue(int value)
    {
        gemsValue = value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            Player player = other.GetComponent<Player>();
            if(player!=null)
            {
                player.GetGems(gemsValue);
            }
            Destroy(this.gameObject);
        }
    }
}
