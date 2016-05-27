using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;

public class DiapositivaPreguntas : MonoBehaviour {
    ArrayList preguntas; //arreglo de tipo Pregunta
    public GameObject preguntaPrefab;
    public UILabel titulo;
    public UILabel subTitulo;
    DataBase db = new DataBase();
    MySqlDataReader question;
    MySqlDataReader answerIncorrect;
    MySqlDataReader answerCorrect;
    //public int respuestaCorrectaID;
    public int preguntaID;
    public int[] respuestaIncorrectaID;
    public string[] respuestaIncorrecta;
    public string preguntaText;

    MySqlDataReader module;
    MySqlDataReader moduleType;
    int idModule = 10;
    int idTipoModule;


    int questions = 0;

    public string[] respuestas = new string[4];

    // Use this for initialization
    void Start () {
        inicializar();
    }

    public void inicializar()
    {

        module = db.Consultar("SELECT * FROM Module WHERE id = " + idModule);
        while (module.Read())
        {
            titulo.text = (string)module["name"];
            idTipoModule = int.Parse(module["fk_moduleType"].ToString());

        }

        db = new DataBase();
        moduleType = db.Consultar("SELECT * FROM ModuleType WHERE id = " + idTipoModule);

        while (moduleType.Read())
        {
            subTitulo.text = (string)moduleType["name"];
        }

        db = new DataBase();
        question = db.Consultar("SELECT * FROM InformationModuleQuestion WHERE fk_module = 10");

        preguntas = new ArrayList();

        while (question.Read())
        {
            preguntaID = int.Parse(question["id"].ToString());
            preguntaText = (string)question["question"];

            db = new DataBase();
            answerIncorrect = db.Consultar("SELECT * FROM InformationModuleAnswers WHERE fk_informationModuleQuestions = " + preguntaID + " AND Correct = 'False' ORDER BY RAND() LIMIT 3");

            db = new DataBase();
            answerCorrect = db.Consultar("SELECT * FROM InformationModuleAnswers WHERE fk_informationModuleQuestions = " + preguntaID + " AND Correct = 'True' ORDER BY RAND() LIMIT 1");

            int[] respuestasID = new int[4];
            string[] respuestas = new string[4];

            int randomCorrecta = Random.Range(0, 4);
            while (answerCorrect.Read())
            {
                respuestasID[randomCorrecta] = int.Parse(answerCorrect["id"].ToString());
                respuestas[randomCorrecta] = answerCorrect["text"].ToString();
            }

            int resp = 0;
            respuestaIncorrectaID = new int[3];
            respuestaIncorrecta = new string[3];
            while (answerIncorrect.Read())
            {
                //this.respuestaID = respuestaID;
               
                respuestaIncorrectaID[resp] = int.Parse(answerIncorrect["id"].ToString());
                respuestaIncorrecta[resp] = answerIncorrect["text"].ToString();
                resp++;
            }

             int j = 0;
             for (int i = 0; i < 4; i++)
             {
                 if (i != randomCorrecta)
                 {
                    respuestasID[i] = respuestaIncorrectaID[j];
                    respuestas[i] = respuestaIncorrecta[j];
                    j++;
                 }

             }

            //int preguntaID, string pregunta, int[] respuestaID, string[] respuesta, int respuestaCorrecta



            
            GameObject g = NGUITools.AddChild(transform.FindChild("PanelCentral").gameObject, preguntaPrefab);
            g.transform.localPosition = new Vector3(g.transform.localPosition.x, g.transform.localPosition.y - questions * 350f, g.transform.localPosition.z);
            Pregunta d = g.GetComponent<Pregunta>();
            d.inicializar(preguntaID, preguntaText, respuestasID, respuestas, randomCorrecta);
            
            preguntas.Add(d);
            questions++;

            for (int i = 0; i < 4; i++)
            {
                db = new DataBase();
                db.EjecutarConsultar("INSERT INTO QuestionMakedInformationModule (fk_realizationModule, fk_questionID, fk_answerID ) VALUES (1," + preguntaID + "," +respuestasID[i]+")");
            }
        }
    }

    public void entregarEvaluacion()
    {
        int correcta = 0;
        
        foreach (Pregunta p in preguntas)
        {
            db = new DataBase();
            db.EjecutarConsultar("INSERT INTO InformationModuleDetail (fk_realizationModule, fk_questionID, fk_informationModuleAnswers) VALUES (1," + preguntaID + "," + p.respuestaUsuarioID + ")");
            if (p.respuestaToggle[p.respuestaCorrectaID].isChecked == true)
            {
                correcta++;
            }
            Debug.Log(correcta.ToString());
        }
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
