using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;

public class Diapositiva : MonoBehaviour {
    public enum TipoDispositiva {TextoDerecha, TextoIzquierda, Titulo};
    public TipoDispositiva tipo = TipoDispositiva.TextoDerecha;
    public PanelInformacionBullets[] panelInformacionBullets;
    public UITexture imagenPrincipal;
    public UILabel titulo;
    public UILabel subTitulo;
    public GameObject[] sonidoBotones;
    public PanelDiapositivas panelDiapositivas;
    public GameObject botonSiguienteDiapositiva;
    public GameObject botonAnteriorDiapositiva;
    public string sound;
    AudioSource audio;
        

    // Use this for initialization
    void Start ()
    {
        audio = GetComponent<AudioSource>();
    }

    public void verDiapositivaAnterior()
    {
        panelDiapositivas.cambiarDiapositiva(false);
    }

    public void verDiapositivaSiguiente()
    {
        panelDiapositivas.cambiarDiapositiva(true);
    }

    public void escucharSonido()
    {
        audio.clip = ((AudioClip)Resources.Load(sound));
        audio.Play();
    }

    public void pararSonido()
    {
        audio.Stop();
    }

    // Update is called once per frame
    void Update ()
    {
	
	}
}
