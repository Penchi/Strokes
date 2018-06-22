using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour {

    public LineRenderer linea;
    public float incremento;

    void OnEnable()
    {
        EventManager.OnToqueNodo += Destruir;
    }

    void OnDisable()
    {
        EventManager.OnToqueNodo -= Destruir;
    }

    // Use this for initialization
    void Start () {
        name = "Nodo";
        StartCoroutine(Nacer());
    }

    public void Destruir()
    {
        StartCoroutine(Desaparecer());
    }

    IEnumerator Nacer()
    {
        float t = 0f;

        while (t < 1)
        {
            t += incremento * Time.deltaTime;
            linea.widthMultiplier = Mathf.Lerp(0, 1.25f, t);
            yield return new WaitForSeconds(0);
        }
    }

    IEnumerator Desaparecer()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
