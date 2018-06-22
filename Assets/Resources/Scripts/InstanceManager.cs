using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceManager : MonoBehaviour{

    public Camara camara;
    public GameObject goLinea;
    public GameObject goNodo;
    public Comandos comandos;
    public Puntuacion puntuacion;

    private Vector3 posicionNodo;
    private GameObject linea;
    private GameObject nodo;
    
    void OnEnable()
    {
        EventManager.OnToqueNodo += CrearNodo;
    }

    void OnDisable()
    {
        EventManager.OnToqueNodo -= CrearNodo;
    }

    void Start()
    {
        posicionNodo = new Vector3(0, 5, 0);

        linea = Instantiate(goLinea, Vector3.zero, new Quaternion()) as GameObject;
        comandos.ReasignarLinea(linea.GetComponent<Linea>());
        nodo = Instantiate(goNodo, posicionNodo, new Quaternion()) as GameObject;
    }

    void CrearNodo()
    {
        puntuacion.AgregarParametros(linea.transform.position, nodo.transform.position);
        camara.Trasladar(posicionNodo);
        linea = Instantiate(goLinea, posicionNodo, new Quaternion()) as GameObject;
        comandos.ReasignarLinea(linea.GetComponent<Linea>());
        posicionNodo = new Vector3(Random.Range(posicionNodo.x - 6.5f, posicionNodo.x + 6.5f), Random.Range(posicionNodo.y + 5, posicionNodo.y + 16f), 0);
        nodo = Instantiate(goNodo, posicionNodo, new Quaternion()) as GameObject;
    }
}