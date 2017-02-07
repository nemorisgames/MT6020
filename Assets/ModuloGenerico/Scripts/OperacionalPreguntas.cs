using UnityEngine;
using System.Collections;

public class OperacionalPreguntas : MonoBehaviour {
    ArrayList preguntas;
    public int preguntaID;

    public Configuracion conf;
    public float porcentajePreguntas;

    public int tiempoInicial;
    public int tiempoFinal;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator InformationDetail()
    {
        int correctas = 0;

        foreach (Pregunta p in preguntas)
        {
            WWWForm form = new WWWForm();
            form.AddField("preguntaID", preguntaID);
            form.AddField("respuestaUsuarioID", p.respuestaUsuarioID);
            WWW download = new WWW(VariablesGlobales.direccion + "SimuladorMT6020/operationalInfoModuleDetail.php", form);
            yield return download;
            if (download.error != null)
            {
                print("Error downloading: " + download.error);
                //mostrarError("Error de conexion");
				yield return false;
            }
            else
            {
                print(download.text);
            }

            if (p.respuestaToggle[p.respuestaCorrectaID].isChecked == true)
            {
                //print("Pregunta: " + correcta + " es correcta");
                correctas++;
            }

            porcentajePreguntas = (correctas * 100) / preguntas.Count;

            if (GameObject.Find("Configuracion") != null)
            {
                print("Configuracion Existe");
                conf = GameObject.Find("Configuracion").GetComponent<Configuracion>();
            }
            conf.ResultadoPreguntas = Mathf.RoundToInt(porcentajePreguntas);

            /* VERIFICAR */
            if (tiempoFinal < 0)
            {
                tiempoFinal = Mathf.RoundToInt(Time.time);
                conf.ResultadoTiempo = tiempoFinal - tiempoInicial;
            }
        }
    }

    public void IniciarContador()
    {
        if (tiempoInicial < 0)
            tiempoInicial = Mathf.RoundToInt(Time.time);
        print(tiempoInicial);
    }
}
