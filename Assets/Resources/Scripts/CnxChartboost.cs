using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartboostSDK;
using UnityEngine.SceneManagement;
using Assets.Resources.Scripts;

public class CnxChartboost : MonoBehaviour {
    
    void OnEnable()
    {
        Chartboost.didCompleteRewardedVideo += didDisplayRewardedVideo;
    }

    void OnDisable()
    {
        Chartboost.didCompleteRewardedVideo -= didDisplayRewardedVideo;
    }
    
    // Use this for initialization
    void Start () {
        Chartboost.setAutoCacheAds(true);
    }

    public void Reproducir()
    {
        Debug.Log("FEDSGRSDFYHDRFSTDUUTDJTUDJGUDTFG");
        if (Chartboost.isInitialized())
            if(!Chartboost.isAnyViewVisible())
                Chartboost.showRewardedVideo(CBLocation.Default);
    }

    void didDisplayRewardedVideo(CBLocation location, int reward)
    {
        DataManager.Instance.GuardarDatos((byte?)(DataManager.Instance.getVidasRestantes() + 3), null);
        DataManager.Instance.CargarDatos();
    }
}
