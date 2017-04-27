using UnityEngine;
using System.Collections;
//using MySql.Data.MySqlClient;

public class DiapositivaPreguntas : MonoBehaviour {
    ArrayList preguntas; //arreglo de tipo Pregunta
    public GameObject preguntaPrefab;
    public UILabel titulo;
    public UILabel subTitulo;
    DataBase db = new DataBase();
    //MySqlDataReader question;
	string [,] question;
    //MySqlDataReader answerIncorrect;
	string [,] answerIncorrect;
	//MySqlDataReader answerCorrect;
	string [] answerCorrect;
    //public int respuestaCorrectaID;
    public int preguntaID;
    public int[] respuestaIncorrectaID;
    public string[] respuestaIncorrecta;
    public string preguntaText;

//    MySqlDataReader module;
//    MySqlDataReader moduleType;
    public int idModule = 10;
    int idTipoModule;

    int questions = 0;

    public string[] respuestas = new string[4];

    public int idModulo;
    public string alumno;
    public int percentageAproval;
    public float percentageGet;

    private Configuracion conf;

    public int tiempoInicial = -1;
    public int tiempoFinal = -1;
    public int tiempoGet = -1;

	public GameObject panelResultados;
	public UILabel tiempo;
	public UILabel repeticiones;
	public UILabel resultado;
	public GameObject[] resultados;


    // Use this for initialization
    void Start () {
		StartCoroutine(inicializar());
    }

	public IEnumerator inicializar()
    {

		WWWForm form = new WWWForm ();
		form.AddField ("idModule",idModule);
		WWW download = new WWW (db.direccion+"obtenerModulo.php",form);
		yield return download;
		if (download.error != null) {
			print ("Error downloading: " + download.error);
			//mostrarError("Error de conexion");
			yield return false;
		} else {
			string retorno = download.text;
			string[] retArr = retorno.Split (new char[]{ '|' });
			titulo.text = retArr [0];
			idTipoModule = int.Parse(retArr [1].ToString().Trim());
		}
			
        /*module = db.Consultar("SELECT * FROM Module WHERE id = " + idModule);
        while (module.Read())
        {
            titulo.text = (string)module["name"];
            idTipoModule = int.Parse(module["fk_moduleType"].ToString());

        }*/


		form = new WWWForm ();
		form.AddField ("idTipoModule",idTipoModule);
		download = new WWW (db.direccion + "obtenerNombreTipoModulo.php", form);
		yield return download;
		if (download.error != null) {
			print ("Error downloading: " + download.error);
			//mostrarError("Error de conexion");
			yield return false;
		} else {
			subTitulo.text = download.text;
		}

        /*db = new DataBase();
        moduleType = db.Consultar("SELECT * FROM ModuleType WHERE id = " + idTipoModule);

        while (moduleType.Read())
        {
            subTitulo.text = (string)moduleType["name"];
        }*/

		form = new WWWForm ();
		form.AddField ("idModule",idModule);
		download = new WWW (db.direccion + "obtenerInfoModuleQuestions.php", form);
		yield return download;
		if (download.error != null) {
			print ("Error downloading: " + download.error);
			//mostrarError("Error de conexion");
			yield return false;
		} else {
			string[] resultado = download.text.Split (new char[]{ '*' });
			question = new string[2, resultado.Length];
			for (int i = 0; i < resultado.Length; i++) {
				string[] row = resultado [i].Split (new char[]{ '|' });
				for (int j = 0; j < row.Length; j++) {
					question [j, i] = row [j];
				}
			}
		}

        /*db = new DataBase();
        question = db.Consultar("SELECT * FROM InformationModuleQuestion WHERE fk_module = 10");*/

        preguntas = new ArrayList();

        //while (question.Read())
		for(int i=0;i<question.GetLength(1);i++)
        {
            //preguntaID = int.Parse(question["id"].ToString());
            //preguntaText = (string)question["question"];
			preguntaID = int.Parse(question[0,i].ToString().Trim());
			preguntaText = question [1, i];

            //db = new DataBase();
            //answerIncorrect = db.Consultar("SELECT * FROM InformationModuleAnswers WHERE fk_informationModuleQuestions = " + preguntaID + " AND Correct = 'False' ORDER BY RAND() LIMIT 3");

			form = new WWWForm ();
			form.AddField ("preguntaID",preguntaID);
			download = new WWW (db.direccion + "obtenerInfoModuleIncorrectAnswers.php", form);
			yield return download;
			if (download.error != null) {
				print ("Error downloading: " + download.error);
				//mostrarError("Error de conexion");
				yield return false;
			} else {
				string[] resultado = download.text.Split (new char[]{ '*' });
				answerIncorrect = new string[2, resultado.Length];
				for (int j = 0; j < resultado.Length; j++) {
					string[] row = resultado [j].Split (new char[]{ '|' });
					for (int k = 0; k < row.Length; k++) {
						answerIncorrect [k, j] = row [k];
					}
				}
			}


            /*db = new DataBase();
            answerCorrect = db.Consultar("SELECT * FROM InformationModuleAnswers WHERE fk_informationModuleQuestions = " + preguntaID + " AND Correct = 'True' ORDER BY RAND() LIMIT 1");
			*/

			form = new WWWForm ();
			form.AddField ("preguntaID",preguntaID);
			download = new WWW (db.direccion + "obtenerInfoModuleCorrectAnswers.php", form);
			yield return download;
			if (download.error != null) {
				print ("Error downloading: " + download.error);
				//mostrarError("Error de conexion");
				yield return false;
			} else {
				answerCorrect = download.text.Split (new char[]{ '|' });
			}



            int[] respuestasID = new int[4];
            string[] respuestas = new string[4];

            int randomCorrecta = Random.Range(0, 4);
            //while (answerCorrect.Read())
            //{
                //respuestasID[randomCorrecta] = int.Parse(answerCorrect["id"].ToString());
                //respuestas[randomCorrecta] = answerCorrect["text"].ToString();
				respuestasID[randomCorrecta] = int.Parse(answerCorrect[0].ToString().Trim());
				respuestas[randomCorrecta] = answerCorrect[1];
            //}

            int resp = 0;
            respuestaIncorrectaID = new int[3];
            respuestaIncorrecta = new string[3];
            //while (answerIncorrect.Read())

			for(int j=0;j<answerIncorrect.GetLength(1);j++)
            {
                //this.respuestaID = respuestaID;
                //respuestaIncorrectaID[resp] = int.Parse(answerIncorrect["id"].ToString());
                //respuestaIncorrecta[resp] = answerIncorrect["text"].ToString();

				respuestaIncorrectaID[resp] = int.Parse(answerIncorrect[0,j].ToString().Trim());
				respuestaIncorrecta[resp] = answerIncorrect[1,j];
                resp++;
            }

            int aux = 0;
            for (int j = 0; j < 4; j++)
            {
                if (j != randomCorrecta)
                {
                   respuestasID[j] = respuestaIncorrectaID[aux];
                   respuestas[j] = respuestaIncorrecta[aux];
                   aux++;
                }
            }

            //int preguntaID, string pregunta, int[] respuestaID, string[] respuesta, int respuestaCorrecta

            
            GameObject g = NGUITools.AddChild(transform.FindChild("PanelCentral").gameObject, preguntaPrefab);
            g.transform.localPosition = new Vector3(g.transform.localPosition.x, g.transform.localPosition.y - questions * 350f, g.transform.localPosition.z);
            Pregunta d = g.GetComponent<Pregunta>();
            d.inicializar(preguntaID, preguntaText, respuestasID, respuestas, randomCorrecta);
            
            preguntas.Add(d);
            questions++;


            /*for (int j = 0; j < 4; j++)
            {
                db = new DataBase();
                db.EjecutarConsultar("INSERT INTO QuestionMakedInformationModule (fk_realizationModule, fk_questionID, fk_answerID ) VALUES (1," + preguntaID + "," +respuestasID[i]+")");
			}*/

			form = new WWWForm ();
			form.AddField ("preguntaID",preguntaID);
			form.AddField ("respuestasID0", respuestasID [0]);
			form.AddField ("respuestasID1", respuestasID [1]);
			form.AddField ("respuestasID2", respuestasID [2]);
			form.AddField ("respuestasID3", respuestasID [3]);
			download = new WWW (db.direccion + "crearPreguntaHechaInfoModule.php", form);
			yield return download;
			if (download.error != null) {
				print ("Error downloading: " + download.error);
				//mostrarError("Error de conexion");
				yield return false;
			} else {
				print (download.text);
			}

        }
    }

	public void entregarEvaluacion(){
		StartCoroutine (evaluacion ());
        /*
        if (tiempoFinal < 0)
        {
            tiempoFinal = Mathf.RoundToInt(Time.time);
            tiempoGet = tiempoFinal - tiempoInicial;

            print("tiempo: " + tiempoGet);
        }
        */
    }

	public IEnumerator evaluacion()
	{
		int correcta = 0;

		if (preguntas != null) {
			foreach (Pregunta p in preguntas) {
				/*db = new DataBase();
            db.EjecutarConsultar("INSERT INTO InformationModuleDetail (fk_realizationModule, fk_questionID, fk_informationModuleAnswers) VALUES (1," + preguntaID + "," + p.respuestaUsuarioID + ")");
            */

				WWWForm form = new WWWForm ();
				form.AddField ("idModulo", idModulo);
				form.AddField ("preguntaID", preguntaID);
				form.AddField ("respuestaUsuarioID", p.respuestaUsuarioID);
				WWW download = new WWW (db.direccion + "crearInfoModuleDetail.php", form);
				yield return download;
				Debug.Log (download);
				if (download.error != null) {
					print ("Error downloading: " + download.error);
					//mostrarError("Error de conexion");
					yield return false;
				} else {
					print (download.text);
				}

				if (p.respuestaToggle [p.respuestaCorrectaID].isChecked == true) {
					//print("Pregunta: " + correcta + " es correcta");
					correcta++;
				}

				Debug.Log ("CORRECTA!" + correcta.ToString ());
			}
			int j = preguntas.Count;
			int k = correcta;
			percentageGet = (k * 100) / j;

			if (GameObject.Find ("Configuracion") != null) {
				print ("Configuracion Existe");
				conf = GameObject.Find ("Configuracion").GetComponent<Configuracion> ();
			}
			conf.ResultadoPreguntas = Mathf.RoundToInt (percentageGet);
			if (idModulo == 1) {
				if (tiempoFinal < 0) {
					tiempoFinal = Mathf.RoundToInt (Time.time);
					conf.ResultadoTiempo = tiempoFinal - tiempoInicial;
				}
			}
			//conf.ResultadoTiempo = tiempoGet;
			//print(conf.ResultadoPreguntas);
			if(panelResultados != null)
				panelResultados.SetActive(true);
			if(tiempo != null)
				tiempo.text = "" + conf.ResultadoTiempo;
			if(resultado != null)
				resultado.text = "Resultado: " + percentageGet + "%";
			if (resultados.Length != 0) {
				resultados [0].SetActive (conf.ResultadoPreguntas < conf.ExitoPreguntas);
				resultados [1].SetActive (conf.ResultadoPreguntas >= conf.ExitoPreguntas);
			}
			conf.guardarHistorial ();
			StartCoroutine (verificarResultados ());
		}
    }

    IEnumerator verificarResultados()
    {
        WWWForm form = new WWWForm();
        form.AddField("idModule", idModule);
		WWW download = new WWW(VariablesGlobales.direccion+"/SimuladorMT6020/infoModule.php", form);
        yield return download;
        if (download.error != null)
        {
            print("Error downloading: " + download.error);
			yield return false;
        }
        else
        {
			string retorno = download.text.ToString().Trim();
			percentageAproval = int.Parse(retorno);
			print ("OK");
        }
		//nuevo
		//UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
    }
	
    public void IniciarContador()
    {
        if (tiempoInicial < 0)
            tiempoInicial = Mathf.RoundToInt(Time.time);
        print(tiempoInicial);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
