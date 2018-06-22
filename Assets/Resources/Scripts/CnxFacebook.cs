using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Facebook.Unity;
using Assets.Resources.Scripts;

namespace Facebook.Unity.Example
{

    public class CnxFacebook : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            
        }

        public void Login()
        {
            if (!FB.IsInitialized)
                FB.Init(CallFBLogin);
            else
                CallFBLogin();
            //FB.IsLoggedIn;
        }

        private void Publicar()
        {
            FB.FeedShare(
                picture: new System.Uri("https://play.google.com/store/apps/details?id=com.SigmaBrio.Pictorem"),
                link: new System.Uri("https://play.google.com/store/apps/details?id=com.SigmaBrio.Pictorem"),
                callback: Compartido
            );
        }

        public void Compartido(IShareResult fd)
        {
            if (!fd.Cancelled)
            {
                DataManager.Instance.GuardarDatos((byte?)(DataManager.Instance.getVidasRestantes() + 2), null);
                DataManager.Instance.CargarDatos();
            }
        }

        private void CallFBLogin()
        {
            if (FB.IsLoggedIn && AccessToken.CurrentAccessToken.Permissions.Contains("publish_actions"))
                Publicar();
            else
                FB.LogInWithPublishPermissions(new List<string>() { "publish_actions" });
        }
    }
}
