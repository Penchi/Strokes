using Assets.Resources.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vidas : MonoBehaviour {
    
    public Text txtVidas;
    public int vidas;

    void OnEnable()
    {
        EventManager.OnToqueNodo += AsignarVida;
        EventManager.OnDisminuirVida += AsignarVida;
    }

    void OnDisable()
    {
        EventManager.OnToqueNodo -= AsignarVida;
        EventManager.OnDisminuirVida -= AsignarVida;
    }

    // Use this for initialization
    void Start () {
        vidas = Facilitador.Instance.getVidas();
        txtVidas.text = "X" + vidas;
    }

    public void AsignarVida()
    {
        vidas = Facilitador.Instance.getVidas();
        txtVidas.text = "X" + vidas;
    }
}
