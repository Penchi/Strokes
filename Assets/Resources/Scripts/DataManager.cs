using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager
{
    private Datos datos;
    private string path;
    private FileStream file;

    public float getMejorPuntuacion()
    {
        return datos.mejorPuntuacion;
    }

    public byte getVidasRestantes()
    {
        return datos.vidasAdicionales;
    }

    public void CargarDatos()
    {
        if (File.Exists(Application.persistentDataPath + "/Datos.dsb"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + "/Datos.dsb");
            datos = (Datos)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            GuardarDatos(null, null);
            CargarDatos();
        }
    }

    public void GuardarDatos(byte? vidasAdicionales, float? mejorPuntuacion = -1)
    {
        if (mejorPuntuacion.HasValue)
            datos.mejorPuntuacion = (float) mejorPuntuacion;
        if (vidasAdicionales.HasValue)
            datos.vidasAdicionales = (byte)vidasAdicionales;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenWrite(Application.persistentDataPath + "/Datos.dsb");
        bf.Serialize(file, datos);
        file.Close();
    }


    private static DataManager instance;

    private DataManager()
    {
        path = Application.persistentDataPath + "/Datos.dsb";
        datos = new Datos();
        CargarDatos();
    }

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
                instance = new DataManager();
            return instance;
        }
    }
}
