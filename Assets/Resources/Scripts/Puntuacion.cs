using Assets.Resources.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Puntuacion : MonoBehaviour {

    public Text txtPuntuacionActual;
    public Text txtPuntuacionMejor;

    public float puntuacion;
    public float mejorPuntuacionPasada;

    void OnEnable()
    {
        EventManager.OnGameOver += ActualizarMejorPuntuacion;
    }

    void OnDisable()
    {
        EventManager.OnGameOver -= ActualizarMejorPuntuacion;
    }

    void Awake()
    {
        puntuacion = 0;
    }
    // Use this for initialization
    void Start ()
    {
        CargarMejorPuntuacion();
    }

    public void AgregarParametros(Vector3 linea, Vector3 nodo)
    {
        puntuacion += Vector3.Distance(linea, nodo);
        StopCoroutine(Sumar(0));
        StartCoroutine(Sumar(float.Parse(txtPuntuacionActual.text)));
    }

    public void CargarMejorPuntuacion()
    {
        DataManager.Instance.CargarDatos();
        txtPuntuacionMejor.text = DataManager.Instance.getMejorPuntuacion().ToString("0.00");
    }

    public void ActualizarMejorPuntuacion()
    {
        Facilitador.Instance.setPuntuacion(puntuacion);
        if (puntuacion > float.Parse(txtPuntuacionMejor.text))
        {
            txtPuntuacionMejor.text = txtPuntuacionActual.text;
            DataManager.Instance.GuardarDatos(null, puntuacion);
        }
    }

    IEnumerator Sumar(float actual)
    {
        float t = 0f;
        float delay = 0.80f;

        while (t < 1)
        {
            t += delay * Time.deltaTime;
            txtPuntuacionActual.text = Mathf.Lerp(actual, puntuacion, t).ToString("0.00");
            yield return new WaitForSeconds(0);
        }
    }
}
