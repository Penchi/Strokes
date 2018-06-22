using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Datos
{
    public float mejorPuntuacion {get;set;}
    public byte vidasAdicionales {get;set;}

    public Datos()
    {
        mejorPuntuacion = 0;
        vidasAdicionales = 1;
    }
}
