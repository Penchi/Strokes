using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comandos : MonoBehaviour {

    private Linea linea;
    private bool presionado;

    private bool activo;

    void OnEnable()
    {
        EventManager.OnPause += ActivarDesactivar;
        EventManager.OnGameOver += ActivarDesactivar;
    }

    void OnDisable()
    {
        EventManager.OnPause -= ActivarDesactivar;
        EventManager.OnGameOver -= ActivarDesactivar;
    }

    // Use this for initialization
    void Start () {
        presionado = false;
        activo = true;
	}

    public void MouseDown()
    {
        if(activo)
            if (!presionado)
            {
                linea.Girar();
                presionado = true;
            }
    }

    public void MouseUp()
    {
        if(activo)
            if(presionado)
            {
                linea.Trazar();
                presionado = false;
            }
    }

    public void ReasignarLinea(Linea linea)
    {
        this.linea = linea;
    }

    public void ActivarDesactivar()
    {
        activo = !activo;
    }
}
