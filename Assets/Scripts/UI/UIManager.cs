using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance==null)
            {
                Debug.LogError("Instance is null");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private Text playerGemCountText;
    [SerializeField]
    private Image selectionImg;
    [SerializeField]
    private Text gemsCountTextHUD;
    [SerializeField]
    private GameObject[] lives;

    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = "" + gemCount + "G";
    }

    public void UpdateSelectionImage(int yPos)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemsCount(int count)
    {
        gemsCountTextHUD.text = "" + count;
    }

    public void UpdateLives(int life)
    {
         for(int i=0; i<=life; i++)
        {
            if(i==life)
            {
                lives[i].SetActive(false);
            }
        }
    }
}
