using Assets.Resources.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linea : MonoBehaviour {

    public LineRenderer linea;
    public Vector3 longitud;
    public Transform posCollider;
    public float incremento;
    public float velocidadRotacion = 1.5f;
    public float velocidadTrazado = 0.5f;
    bool preparado;
    bool girar;
    bool trazar;

    bool invertir;
    // Use this for initialization

    void OnEnable()
    {
        EventManager.OnToqueNodo += Contacto;
        EventManager.OnDisminuirVida += Renacer;
    }

    void OnDisable()
    {
        EventManager.OnToqueNodo -= Contacto;
        EventManager.OnDisminuirVida -= Renacer;
    }
    
    void Start()
    {
        longitud.x = 1.5f;
        preparado = false;
        girar = false;
        trazar = false;
        
        velocidadRotacion += Facilitador.Instance.getNodos() / 25f;
        velocidadTrazado += Facilitador.Instance.getNodos() / 25f;

        StartCoroutine(Nacer());
    }

    public void Contacto()
    {
        posCollider.GetComponent<Collider2D>().isTrigger = false;
        trazar = false;
        StartCoroutine(Desaparecer());
    }

    public void Renacer()
    {
        preparado = false;
        girar = false;
        trazar = false;
        StartCoroutine(ReNacer());
    }

    public void ResetearLinea()
    {
        longitud.x = 1.5f;
        posCollider.localPosition = longitud;
        transform.localEulerAngles = Vector3.zero;
    }

    IEnumerator ReNacer()
    {
        float t = 0f;

        Vector3 vector = new Vector3(longitud.x, 0, 0);

        while (t < 1)
        {
            t += velocidadTrazado * 2 * Time.deltaTime;

            linea.SetPosition(1, Vector3.Lerp(vector, Vector3.zero, t));
            yield return new WaitForSeconds(0);
        }
        ResetearLinea();
        StartCoroutine(Nacer());
    }

    IEnumerator Nacer()
    {
        preparado = true;
        float t = 0f;

        while (t < 1)
        {
            t += incremento * Time.deltaTime;

            linea.SetPosition(1, Vector3.Lerp(Vector3.zero, longitud, t));
            yield return new WaitForSeconds(0);
        }
    }

    IEnumerator Rotar()
    {
        float t;
        Vector3 vector = new Vector3(0, 0, 180);

        while (girar)
        {
            t = 0;
            while (t < 1)
            {
                t += velocidadRotacion * Time.deltaTime;

                transform.localEulerAngles = (Vector3.Lerp(Vector3.zero, vector, t));
                yield return new WaitForSeconds(0);
            }

            t = 0;

            while (t < 1)
            {
                t += velocidadRotacion * Time.deltaTime;

                transform.localEulerAngles = (Vector3.Lerp(vector, Vector3.zero, t));
                yield return new WaitForSeconds(0);
            }
        }
    }

    IEnumerator Crecer()
    {
        float t = 0f;
        float longitudInicial = longitud.x;
        Vector3 vector = new Vector3(longitud.x, 0, 0);
        //limite 30
        while (trazar)
        {
            t += velocidadTrazado * Time.deltaTime;

            longitud.x = Mathf.Lerp(longitudInicial, 30, t);
            linea.SetPosition(1, longitud);
            posCollider.localPosition = longitud;
            yield return new WaitForSeconds(0);
        }
    }

    IEnumerator Desaparecer()
    {
        float t = 0f;

        Vector3 temp = linea.GetPosition(0);

        while (t < 1f)
        {
            t += incremento * Time.deltaTime;
            
            linea.SetPosition(0, Vector3.Lerp(temp, linea.GetPosition(1), t));
            yield return new WaitForSeconds(0);
        }

        t = 0;

        while (t < 1)
        {
            t += incremento * Time.deltaTime;
            linea.widthMultiplier = Mathf.Lerp(1, 0, t);
            yield return new WaitForSeconds(0);
        }

        linea.widthMultiplier = 0;
        Destroy(gameObject);
    }

    public void Girar()
    {
        if(preparado && !trazar)
        {
            girar = !girar;
            StartCoroutine(Rotar());
        }
    }

    public void Trazar()
    {
        if(preparado && !trazar && girar)
        {
            girar = false;
            StopCoroutine(Rotar());
            StopAllCoroutines();
            trazar = true;
            StartCoroutine(Crecer());
        }
    }

    public void Mover(Vector3 posicion)
    {
        transform.position = posicion;
    }

    public Vector3 ObtenerRotacion()
    {
        return transform.eulerAngles;
    }
}
