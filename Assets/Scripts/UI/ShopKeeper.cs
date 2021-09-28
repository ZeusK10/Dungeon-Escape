using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField]
    GameObject shopPanel;
    private int currentSelectedItem;
    private int currentItemCost;
    [SerializeField]
    GameObject gemMsg;
    [SerializeField]
    GameObject itemBoughtMsg;
    Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            UIManager.Instance.OpenShop(player.diamonds);
            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        switch(item)
        {
            case 0:
                UIManager.Instance.UpdateSelectionImage(80);
                currentItemCost = 200;
                currentSelectedItem = 0;
                break;
            case 1:
                UIManager.Instance.UpdateSelectionImage(-30);
                currentItemCost = 400;
                currentSelectedItem = 1;
                break;
            case 2:
                UIManager.Instance.UpdateSelectionImage(-140);
                currentItemCost = 100;
                currentSelectedItem = 2;
                break;
        }
    }

    public void BuyItem()
    {
        if(player.diamonds>=currentItemCost)
        {
            if(currentSelectedItem==2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }
            player.diamonds -= currentItemCost;
            UIManager.Instance.OpenShop(player.diamonds);
            itemBoughtMsg.SetActive(true);
            StartCoroutine(WaitForMsg(itemBoughtMsg));
        }
        else
        {
            gemMsg.SetActive(true);
            StartCoroutine(WaitForMsg(gemMsg));
        }
                
    }


    IEnumerator WaitForMsg(GameObject msg)
    {
        yield return new WaitForSeconds(3);
        msg.SetActive(false);
    }
}
