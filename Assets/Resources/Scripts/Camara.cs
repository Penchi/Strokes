using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour {

    public void Trasladar(Vector3 vector)
    {
        StopCoroutine(Mover(vector));
        StartCoroutine(Mover(vector));
    }

    IEnumerator Mover(Vector3 nodo)
    {
        float t = 0f;
        float delay = 0.9f;

        Vector3 temp = transform.position;

        nodo.y += 10;
        nodo.z = -10;

        while (t < 1f)
        {
            t += delay * Time.deltaTime;

            transform.position = Vector3.Lerp(temp, nodo, t);
            yield return new WaitForSeconds(0);
        }
    }
}
