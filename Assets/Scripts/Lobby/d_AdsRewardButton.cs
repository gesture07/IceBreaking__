using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEditor;

public class d_AdsRewardButton : MonoBehaviour
{
    public void ShowAd()
    {
        if(Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions{resultCallback = HandleShowResult};
            Advertisement.Show("rewardedVideo", options);
        }

    }

    private void HandleShowResult(ShowResult result)
    {
        switch(result)
        {
        case ShowResult.Finished:
            Debug.Log("The ad was successfully shown");
            //your code to reward the gamer
            //give coins etc
            break;
        case ShowResult.Skipped:
            Debug.Log("the ad was skipped before reaching the end");
            break;
        case ShowResult.Failed:
            Debug.LogError("the ad failed to be shown");
            break;
        }

    }
    // Start is called before the first frame update
}
