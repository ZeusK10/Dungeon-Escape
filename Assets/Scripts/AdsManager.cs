using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    string placement = "rewardedVideo";

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("4382575", true);
    }

    public void ShowAd()
    {
        Advertisement.Show(placement);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(showResult==ShowResult.Finished)
        {
            Debug.Log("Finished");
            GameObject.Find("Player").GetComponent<Player>().GetGems(100);
        }
        else if(showResult==ShowResult.Failed)
        {
            Debug.Log("Failed");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {
    }
}