using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts
{
    class Facilitador
    {
        public float puntuacion;
        public int nodosAcumulados;
        public int vidas;

        public void setPuntuacion(float puntuacion)
        {
            this.puntuacion = puntuacion;
        }

        public float getPuntuacion()
        {
            return puntuacion;
        }

        public void AgregarVidas(int vidas)
        {
            this.vidas += vidas;
        }

        public int getVidas()
        {
            return vidas;
        }

        public int getNodos()
        {
            return nodosAcumulados;
        }

        //llamado por collider, tocó un nodo
        public void AumentarVida()
        {
            nodosAcumulados++;
            if (vidas < 9)
            {
                AgregarVidas(1);
            }
            EventManager.Instance.ToqueNodo();
        }

        public void DisminuirVida()
        {
            vidas -= 1;
            if (vidas < 0)
            {
                EventManager.Instance.GameOver();
                DataManager.Instance.GuardarDatos(1, null);
                //instance = null;
            }
            else
                EventManager.Instance.DisminuirVida();
        }

        public void Reiniciar()
        {
            Debug.Log("OLA");
            
            vidas = 1;
            nodosAcumulados = 0;
            puntuacion = 0;

            if (DataManager.Instance.getVidasRestantes() > 0)
            {
                vidas = DataManager.Instance.getVidasRestantes();
                DataManager.Instance.GuardarDatos(0, null);
            }
        }


        private static Facilitador instance;

        private Facilitador()
        {

        }

        public static Facilitador Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Facilitador();
                    instance.Reiniciar();
                }
                return instance;
            }
        }
    }
}
