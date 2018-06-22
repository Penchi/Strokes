using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public delegate void statusAccion();
    public static event statusAccion OnToqueNodo;
    public static event statusAccion OnPause;
    public static event statusAccion OnDisminuirVida;
    public static event statusAccion OnGameOver;

    public void ToqueNodo()
    {
        if (OnToqueNodo != null)
            OnToqueNodo();
    }

    public void Pause()
    {
        if (OnPause != null)
            OnPause();
    }

    public void DisminuirVida()
    {
        if (OnDisminuirVida != null)
            OnDisminuirVida();
    }

    public void GameOver()
    {
        if (OnGameOver != null)
            OnGameOver();
    }

    private static EventManager instance;

    private EventManager()
    {

    }

    public static EventManager Instance
    {
        get
        {
            if (instance == null)
                instance = new EventManager();
            return instance;
        }
    }
}
