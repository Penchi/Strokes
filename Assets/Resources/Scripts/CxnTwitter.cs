using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitterKit.Unity;
using UnityEngine.UI;
using System.IO;
using Assets.Resources.Scripts;

public class CxnTwitter : MonoBehaviour {

    public void Login()
    {
        //Compartido();
        Twitter.Init();
        
        TwitterSession session = Twitter.Session;
        if(session == null)
        {
            Twitter.LogIn(LoginCompleteWithCompose, Fallo);
        }
        else
        {
            LoginCompleteWithCompose(session);
        }
    }

    public void LoginCompleteWithCompose(TwitterSession session)
    {
        string[] s = Textos.Instance.TextoTwitter();
        Twitter.Compose(session, null, s[0] + Facilitador.Instance.getPuntuacion() + s[1] + " https://play.google.com/store/apps/details?id=com.SigmaBrio.Pictorem", null, (string tweetId) => { Compartido(); });
        DataManager.Instance.CargarDatos();
    }

    public void Compartido()
    {
        DataManager.Instance.GuardarDatos((byte?)(DataManager.Instance.getVidasRestantes() + 2), null);
    }

    public void Fallo(ApiError error)
    {
        Debug.Log(error.code + " mensaje:" + error.message);
    }
}
