using Assets.Resources.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public LineRenderer fondo;

    //Botones que se desactivarán temporalmente en las animaciones
    byte numBotones;
    public Button[] botones;

    //Elementos del menú de pausa
    byte numBotonesPausa;
    public Image[] botonesPausa;

    //Elementos del menú de gameover
    byte numBotonesFin;
    public Image[] botonesFin;

    //Elementos que cambiarán de color que son parte de la puntuacion en pausa
    byte numPuntuacionPausa;
    public Text[] puntuacionPausa;
    //Elementos que cambiarán de color que son parte de la puntuacion en pausa
    byte numPuntuacionFin;
    public Text[] puntuacionFin;
    
    float incremento = 1f;

    public bool pause;
    bool gameover;

    void OnEnable()
    {
        EventManager.OnGameOver += Terminar;
    }

    void OnDisable()
    {
        EventManager.OnGameOver -= Terminar;
    }

    // Use this for initialization
    void Start ()
    {
        Textos.Instance.Crear();
        numBotones = (byte)botones.Length;
        numBotonesPausa = (byte)botonesPausa.Length;
        numBotonesFin = (byte)botonesFin.Length;
        numPuntuacionPausa = (byte)puntuacionPausa.Length;
        numPuntuacionFin = (byte)puntuacionFin.Length;

        pause = false;
        gameover = false;

        Color color = new Color();
        color.a = 0;

        for (int i = 0; i < numBotonesPausa; i++)
        {
            botonesPausa[i].gameObject.SetActive(false);
            botonesPausa[i].color = color;
        }
        for (int i = 0; i < numBotonesFin; i++)
        {
            botonesFin[i].color = color;
            botonesFin[i].gameObject.SetActive(false);
        }
        
        for (int i = 0; i < numPuntuacionFin; i++)
            puntuacionFin[i].enabled = false;
    }

    public void DesplegarMenu()
    {
        if (!gameover)
        {
            EventManager.Instance.Pause();
            StartCoroutine(AbrirCerrar());
            StartCoroutine(AparecerDesaparecer());

            Debug.Log("Desplegando menu");
        }
    }

    public void Resumir()
    {
        if (!gameover)
        {
            EventManager.Instance.Pause();
            StartCoroutine(AbrirCerrar());
            StartCoroutine(AparecerDesaparecer());

            Debug.Log("Resumiendo");
        }
    }

    public void Reiniciar()
    {
        Facilitador.Instance.Reiniciar();
        SceneManager.LoadScene(0);
    }

    public void Terminar()
    {
        if (!gameover)
        {
            gameover = true;
            StartCoroutine(AbrirCerrar());
            StartCoroutine(AparecerFin());

            Debug.Log("GameOver");
        }
    }

    IEnumerator AbrirCerrar()
    {
        for (int i = 0; i < numBotones; i++)
            botones[i].interactable = !botones[i].interactable;

        float t = 0f;
        float minimo;
        float maximo;

        Color colorTemp = puntuacionPausa[0].color;
        Color colorTexto;

        if (fondo.widthMultiplier == 0)
        {
            minimo = 0;
            maximo = 50;
            colorTexto = new Color(1, 1, 1, 1);
        }
        else
        {
            minimo = 50;
            maximo = 0;
            colorTexto = new Color(0, 0, 0, 1);
        }

        //Guardamos el valor de lerp para no ejecutarlo varias veces en las iteraciones
        Color lerp;

        while (t < 1)
        {
            t += incremento * Time.deltaTime;

            fondo.widthMultiplier = Mathf.Lerp(minimo, maximo, t);

            lerp = Color.Lerp(colorTemp, colorTexto, t);

            for (int i = 0; i < numPuntuacionPausa; i++)
                puntuacionPausa[i].color = lerp;

            yield return new WaitForSeconds(0);
        }

        for (int i = 0; i < numBotones; i++)
            botones[i].interactable = !botones[i].interactable;
    }

    IEnumerator AparecerDesaparecer()
    {
        float t = 0f;
        Color colorTemp = new Color(1, 1, 1, 0);

        float minimo;
        float maximo;

        if (botonesPausa[0].color.a == 0)
        {
            minimo = 0;
            maximo = 1;
        }
        else
        {
            minimo = 1;
            maximo = 0;
        }

        if(!pause)
        {
            for (int i = 0; i < numBotonesPausa; i++)
                botonesPausa[i].gameObject.SetActive(true);
        }

        while (t < 1)
        {
            t += incremento * Time.deltaTime;

            colorTemp.a = Mathf.Lerp(minimo, maximo, t);

            for (int i = 0; i < numBotonesPausa; i++)
                botonesPausa[i].color = colorTemp;

            yield return new WaitForSeconds(0);
        }

        if (pause)
        {
            for (int i = 0; i < numBotonesPausa; i++)
                botonesPausa[i].gameObject.SetActive(false);
        }

        pause = !pause;
    }

    IEnumerator AparecerFin()
    {
        float t = 0f;
        Color colorTemp = new Color(1, 1, 1, 0);

        float minimo = 0;
        float maximo = 1;

        for (int i = 0; i < numBotonesFin; i++)
            botonesFin[i].gameObject.SetActive(true);

        for (int i = 0; i < numPuntuacionFin; i++)
            puntuacionFin[i].enabled = true;

        while (t < 1)
        {
            t += incremento * Time.deltaTime;

            colorTemp.a = Mathf.Lerp(minimo, maximo, t);

            for (int i = 0; i < numBotonesFin; i++)
                botonesFin[i].color = colorTemp;

            yield return new WaitForSeconds(0);
        }
    }
}
