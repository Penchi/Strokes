using LitJson;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class Textos{
    
    private JsonData json;

    public void Crear()
    {
        instance = null;
    }

    public void Reescribir()
    {

        BuscarTxt("TxtDescripcionPuntuacion").text = json["puntuacion"]["actual"].ToString();
        BuscarTxt("TxtDescripcionPuntuacionMejor").text = json["puntuacion"]["mejor"].ToString();

        try
        {
            BuscarTxt("TxtPaso1").text = json["tutorial"]["paso1"].ToString();
            BuscarTxt("TxtPaso2").text = json["tutorial"]["paso2"].ToString();
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public string[] TextoTwitter()
    {
        string[] s = new string[2];
        s[0] = json["twitter"]["texto1"].ToString();
        s[1] = json["twitter"]["texto2"].ToString();
        return s;
    }

    /**
     * Carga el último idioma seleccionado por el usuario
     */
    public string Idioma()
    {
        /*
        if (!PlayerPrefs.HasKey("idioma"))
            PlayerPrefs.SetString("idioma", Application.systemLanguage.ToString());
        */

        return Application.systemLanguage.ToString();
    }

    /**
     * Busca el GameObject dado por el parametro y devuelve su componente UnityEngine.UI.Text
     * @param nombre Es el nombre del GameObject que se desea buscar
     * @return El componente UnityEngine.UI.Text del GameObject que se buscó
     */
    public Text BuscarTxt(string nombre)
    {
        try
        {
            return GameObject.Find(nombre).GetComponent<Text>();
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
        }
        return null;
    }

    private static Textos instance;

    private Textos()
    {
        string path;
        string jsonCrudo;
        
        #if UNITY_EDITOR
        path = Application.streamingAssetsPath + "/Texto/" + Idioma() + ".txt";
        //StreamReader para poder leer acentos con el encoding
        StreamReader r = new StreamReader(path, Encoding.GetEncoding("iso-8859-1"));
        jsonCrudo = r.ReadToEnd();

#endif

#if UNITY_ANDROID && !UNITY_EDITOR
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/Texto/" + Idioma() + ".txt");  // this is the path to your StreamingAssets in android
            while (!loadDB.isDone) ;
            jsonCrudo = loadDB.text;
#endif

        json = JsonMapper.ToObject(jsonCrudo);

        Reescribir();
    }

    public static Textos Instance
    {
        get
        {
            if (instance == null)
                instance = new Textos();
            return instance;
        }
    }
}
