using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public Text[] pasos;
    float incremento = 1f;
    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("tutorial") == 0)
        {
            StartCoroutine(AparecerPasos(0));
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(AparecerPasos(1));
            PlayerPrefs.SetInt("tutorial", 1);
        }
        if(Input.GetMouseButtonUp(0))
        {
            StopAllCoroutines();
            StartCoroutine(DesaparecerPasos());
        }
    }

    IEnumerator AparecerPasos(byte paso)
    {
        float t = 0f;
        Color colorTemp = new Color(0, 0, 0, 0);

        float minimo = 0;
        float maximo = 1;

        pasos[paso].gameObject.SetActive(true);

        while (t < 1)
        {
            t += incremento * Time.deltaTime;

            colorTemp.a = Mathf.Lerp(minimo, maximo, t);

            pasos[paso].color = colorTemp;

            yield return new WaitForSeconds(0);
        }
    }

    IEnumerator DesaparecerPasos()
    {
        float t = 0f;
        Color colorTemp = new Color(0, 0, 0, 0);

        float minimo = 1;
        float maximo = 0;
        
        while (t < 1)
        {
            t += incremento * Time.deltaTime;

            colorTemp.a = Mathf.Lerp(minimo, maximo, t);

            pasos[0].color = colorTemp;
            pasos[1].color = colorTemp;

            yield return new WaitForSeconds(0);
        }
        Destroy(gameObject);
    }
}
