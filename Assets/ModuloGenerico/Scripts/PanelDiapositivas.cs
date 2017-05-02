using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;

public class PanelDiapositivas : MonoBehaviour {
ArrayList diapositivas;
    int diapositivaActual = 0;
    public Diapositiva diapositivaTitulo;
    public DiapositivaPreguntas diapositivaPreguntas;
    // Use this for initialization

    DataBase db = new DataBase();

    //MySqlDataReader infoPageDesign;
    //MySqlDataReader infoStructureSlider;
    //MySqlDataReader sliders;
    //MySqlDataReader module;
    //MySqlDataReader moduleType;

	string[,] sliders;
	string[,] infoStructureSlider;
	string[] infoPageDesign;

    int SliderCount = 0;
    int i = 0;
    //int idModule = PlayerPrefs.GetInt("idModule");
    public int idModule = 10;
    int idTipoModule;
    int StructureCount = 0;

    string moduleName;
    string moduleTypeName;
    string fontColor;
    string icoFullName;
    string hexaColor;
    string imageSlider;
    string imagenPrincipal;

	public GameObject botonSiguienteTitulo;

    void Start ()
    {
		botonSiguienteTitulo.SetActive (false);
        /*module = db.Consultar("SELECT * FROM Module WHERE id = "+idModule);
        while (module.Read())
        {
            moduleName = (string)module["name"];
            idTipoModule = int.Parse(module["fk_moduleType"].ToString());

        }

        db = new DataBase();
        moduleType = db.Consultar("SELECT * FROM ModuleType WHERE id = "+ idTipoModule);

        while (moduleType.Read())
        {
            moduleTypeName = (string)moduleType["name"];
        }

        db = new DataBase();
        sliders = db.Consultar("SELECT * FROM Slider WHERE fk_module = "+idModule+" ORDER BY orderSlider");*/

		StartCoroutine (Inicializar ());
    }

    IEnumerator traerInformacion()
    {
        diapositivas = new ArrayList();
        diapositivaTitulo.titulo.text = moduleName;
        diapositivaTitulo.subTitulo.text = moduleTypeName;
        diapositivas.Add(diapositivaTitulo.gameObject);

        //while (sliders.Read())
		for(int i=0;i<sliders.GetLength(1);i++)
        {
            
            //recibimos el tipo de la diapositiva. En este caso, todas del mismo tipo
            Diapositiva.TipoDispositiva tipo = Diapositiva.TipoDispositiva.TextoDerecha;
            switch (tipo)
            {
			case Diapositiva.TipoDispositiva.TextoDerecha:
				GameObject g = NGUITools.AddChild (gameObject, (GameObject)Resources.Load ("Diapositiva_Tipo1"));
				g.SetActive (false);
				Diapositiva d = g.GetComponent<Diapositiva> ();
				d.panelDiapositivas = this;

                //SEGMENTO DE CODIGO A CONFIGURAR SEGUN LA INFORMACION
				d.titulo.text = moduleName;
				d.subTitulo.text = moduleTypeName;

                //CONSULTO POR LA INFORMACION QUE TIENE CADA SLIDER

                //db = new DataBase();
                //infoPageDesign = db.Consultar("SELECT * FROM InformationPageDesign WHERE fk_slider = "+ sliders["id"]);

				WWWForm form = new WWWForm ();
				//Debug.Log (sliders [0, i]);
				form.AddField ("id", int.Parse (sliders [0, i].ToString ().Trim ()));
				WWW download = new WWW (db.direccion + "obtenerInfoPageDesign.php", form);
				yield return download;
				if (download.error != null) {
					print ("Error downloading: " + download.error);
					//mostrarError("Error de conexion");
					yield return false;
				} else {
					infoPageDesign = download.text.Split (new char[]{ '|' });
				}

                //TODO
                //PONGO IMAGEN Y SONIDO
                //while (infoPageDesign.Read())
                //{
					//imagenPrincipal = (string)infoPageDesign["image"];
					//imagenPrincipal = imagenPrincipal.Split('.')[0].ToString();
					//if ((string)infoPageDesign["sound"].ToString().Trim() != "")
				imagenPrincipal = infoPageDesign [1].Split ('.') [0];
				if (infoPageDesign [2].Trim () != "") {
					d.sonidoBotones [0].SetActive (true);
					d.sonidoBotones [1].SetActive (true);
					//d.sound = (string)infoPageDesign["sound"];
					d.sound = infoPageDesign [2];
				} else {
					d.sonidoBotones [0].SetActive (false);
					d.sonidoBotones [1].SetActive (false);
				}

                //}

                
				d.imagenPrincipal.mainTexture = (Texture)Resources.Load ("Diapositivas/Modulo1/" + imagenPrincipal);

                //db = new DataBase();
                //infoStructureSlider = db.Consultar("SELECT * FROM Structure WHERE fk_informationPageDesign IN (SELECT id FROM InformationPageDesign WHERE fk_slider = "+sliders["id"]+")");

				form = new WWWForm ();
				form.AddField ("id", int.Parse (sliders [0, i].ToString ().Trim ()));
				download = new WWW (db.direccion + "obtenerInfoStructureSlider.php", form);
				yield return download;
				if (download.error != null) {
					print ("Error downloading: " + download.error);
					//mostrarError("Error de conexion");
					yield return false;
				} else {
					string[] retorno = download.text.Split (new char[]{ '*' });
					StructureCount = retorno.Length;
					string[] rowAux = retorno [0].Split (new char[]{ '|' });
					infoStructureSlider = new string[rowAux.Length, StructureCount];
					for (int j = 0; j < StructureCount; j++) {
						string[] row = retorno [j].Split (new char[]{ '|' });
						for (int k = 0; k < row.Length; k++) {
							infoStructureSlider [k, j] = row [k];
						}
					}
				}
					
					
                //StructureCount = 0;

                /*while (infoStructureSlider.Read())
                {
                    StructureCount++;
                }*/

                //db = new DataBase();
                //infoStructureSlider = db.Consultar("SELECT * FROM Structure WHERE fk_informationPageDesign IN (SELECT id FROM InformationPageDesign WHERE fk_slider = " + sliders["id"] + ") ORDER BY orderStructure");

				InformacionBullet[] bullets = new InformacionBullet[StructureCount];

				for (int countBullets = 0; countBullets < StructureCount; countBullets++) {
					bullets [countBullets] = new InformacionBullet ();
				}

                //i = 0;
                //while (infoStructureSlider.Read())
				for (int j = 0; j < StructureCount; j++) {
					//la forma en que se maneja la informacion en cada panel de informacion depende de su diseño. en este caso, solo hay uno

					//icoFullName = (string)infoStructureSlider["icon"];
					//bullets[i].sprite = icoFullName.Split('.')[0].ToString();
					//fontColor = (string)infoStructureSlider["fontColor"];
					icoFullName = infoStructureSlider [1, j].ToString ();
					bullets [j].sprite = icoFullName.Split ('.') [0];
					fontColor = infoStructureSlider [2, j].ToString ();

					switch (fontColor.Trim ()) {

					case "White":
						hexaColor = "ffffff";
						break;
					case "Black":
						hexaColor = "000000";
						break;
					case "Green":
						hexaColor = "00ff00";
						break;
					case "Red":
						hexaColor = "ff0000";
						break;
					case "Blue":
						hexaColor = "0000ff";
						break;
					case "Yellow":
						hexaColor = "ffff00";
						break;
					default:
						hexaColor = "000000";
						break;
					}
						

					/*bullets[i].texto = "["+hexaColor+"]"+(string)infoStructureSlider["text"]+"[-]";
                    if (bullets[i].texto.Length < 32)
                    {
                        bullets[i].texto = "\n" + bullets[i].texto+"\n";
                    }
                    else
                    {
                        if (bullets[i].texto.Length < 64)
                        {
                            bullets[i].texto += "\n";
                        }

                    }*/
					bullets [j].texto = "[" + hexaColor + "]" + infoStructureSlider [3, j] + "[-]";
					if (bullets [j].texto.Length < 32) {
						bullets [j].texto = "\n" + bullets [j].texto + "\n";
					} else {
						if (bullets [j].texto.Length < 64) {
							bullets [j].texto += "\n";
						}

					}
					//i++;
				}

				d.panelInformacionBullets [0].inicializar ((string)sliders [1, i], bullets);
                //FIN SEGMENTO

				diapositivas.Add (g);
				print ("añade 1");
                break;
            }
          //  i++;

        }

        diapositivas.Add(diapositivaPreguntas.gameObject);

		botonSiguienteTitulo.SetActive (true);
    }

    public void cambiarDiapositiva(bool adelante)
    {
        diapositivaActual = Mathf.Clamp(diapositivaActual + (adelante ? 1 : -1), 0, diapositivas.Count);
        for(int i = 0; i < diapositivas.Count; i++)
        {
            ((GameObject)diapositivas[i]).SetActive(i == diapositivaActual);
        }
            
        if (diapositivaActual == diapositivas.Count-1)
        {
            print("ultima diapo");
            diapositivaPreguntas.IniciarContador();
        }
    }

	
	IEnumerator Inicializar(){
		//Obtiene id de modulo
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
			moduleName = retArr [0];
			idTipoModule = int.Parse(retArr [1]);
		}

		//Obtiene id del tipo de modulo
		form = new WWWForm ();
		form.AddField ("idTipoModule",idTipoModule);
		download = new WWW (db.direccion + "obtenerNombreTipoModulo.php", form);
		yield return download;
		if (download.error != null) {
			print ("Error downloading: " + download.error);
			//mostrarError("Error de conexion");
			yield return false;
		} else {
			moduleTypeName = download.text;
		}

		//Obtiene sliders del modulo
		form = new WWWForm ();
		form.AddField ("idModule", idModule);
		download = new WWW (db.direccion+"obtenerSliders.php",form);
		yield return download;
		if (download.error != null) {
			print ("Error downloading: " + download.error);
			//mostrarError("Error de conexion");
			yield return false;
		} else {
			string retorno = download.text;
			string[] retArr = retorno.Split (new char[]{'*'});
			sliders = new string[3, retArr.Length];
			for (int i = 0; i < retArr.Length; i++) {
				string [] row = retArr [i].Split (new char[]{ '|' });
				for (int j = 0; j < row.Length; j++) {
					sliders [j, i] = row [j];
				}
			}
		}
		StartCoroutine (traerInformacion ());
	}

	public void terminarSimulacion(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Login");
	}
	public void resetSimulacion(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
	}
}
