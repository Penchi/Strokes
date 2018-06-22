using Assets.Resources.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineaCollider : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Nodo")
        {
            Facilitador.Instance.AumentarVida();
        }

        if (other.tag == "DeadZone")
        {
            Facilitador.Instance.DisminuirVida();
        }
    }
}
