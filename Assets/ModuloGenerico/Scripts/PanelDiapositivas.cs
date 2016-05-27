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

    MySqlDataReader infoPageDesign;
    MySqlDataReader infoStructureSlider;
    MySqlDataReader sliders;
    MySqlDataReader module;
    MySqlDataReader moduleType;

    int SliderCount = 0;
    int i = 0;
    //int idModule = PlayerPrefs.GetInt("idModule");
    int idModule = 10;
    int idTipoModule;
    int StructureCount = 0;

    string moduleName;
    string moduleTypeName;
    string fontColor;
    string icoFullName;
    string hexaColor;
    string imageSlider;
    string imagenPrincipal;


    void Start ()
    {
        module = db.Consultar("SELECT * FROM Module WHERE id = "+idModule);
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
        sliders = db.Consultar("SELECT * FROM Slider WHERE fk_module = "+idModule+" ORDER BY orderSlider");

        traerInformacion();
    }

    void traerInformacion()
    {
        diapositivas = new ArrayList();
        diapositivaTitulo.titulo.text = moduleName;
        diapositivaTitulo.subTitulo.text = moduleTypeName;
        diapositivas.Add(diapositivaTitulo.gameObject);

        while (sliders.Read())
        {
            
            //recibimos el tipo de la diapositiva. En este caso, todas del mismo tipo
            Diapositiva.TipoDispositiva tipo = Diapositiva.TipoDispositiva.TextoDerecha;
            switch (tipo)
            {
                case Diapositiva.TipoDispositiva.TextoDerecha:
                    GameObject g = NGUITools.AddChild(gameObject, (GameObject)Resources.Load("Diapositiva_Tipo1"));
                    Diapositiva d = g.GetComponent<Diapositiva>();
                    d.panelDiapositivas = this;

                    //SEGMENTO DE CODIGO A CONFIGURAR SEGUN LA INFORMACION
                    d.titulo.text = moduleName;
                    d.subTitulo.text = moduleTypeName;

                    //CONSULTO POR LA INFORMACION QUE TIENE CADA SLIDER

                      db = new DataBase();
                      infoPageDesign = db.Consultar("SELECT * FROM InformationPageDesign WHERE fk_slider = "+ sliders["id"]);

                      //TODO
                      //PONGO IMAGEN Y SONIDO
                      while (infoPageDesign.Read())
                      {
                          imagenPrincipal = (string)infoPageDesign["image"];
                          imagenPrincipal = imagenPrincipal.Split('.')[0].ToString();
                          if ((string)infoPageDesign["sound"].ToString().Trim() != "")
                          {
                              d.sonidoBotones[0].SetActive(true);
                              d.sonidoBotones[1].SetActive(true);
                              d.sound = (string)infoPageDesign["sound"];
                          }
                          else
                          {
                              d.sonidoBotones[0].SetActive(false);
                              d.sonidoBotones[1].SetActive(false);
                          }

                      }

                    
                    d.imagenPrincipal.mainTexture = (Texture)Resources.Load("Diapositivas/Modulo1/"+imagenPrincipal);

                    db = new DataBase();
                    infoStructureSlider = db.Consultar("SELECT * FROM Structure WHERE fk_informationPageDesign IN (SELECT id FROM InformationPageDesign WHERE fk_slider = "+sliders["id"]+")");
                   
                    StructureCount = 0;

                    while (infoStructureSlider.Read())
                    {
                        StructureCount++;
                    }

                    db = new DataBase();
                    infoStructureSlider = db.Consultar("SELECT * FROM Structure WHERE fk_informationPageDesign IN (SELECT id FROM InformationPageDesign WHERE fk_slider = " + sliders["id"] + ") ORDER BY orderStructure");

                    InformacionBullet[] bullets = new InformacionBullet[StructureCount];

                    for (int countBullets = 0; countBullets < StructureCount; countBullets++)
                    {
                        bullets[countBullets] = new InformacionBullet();
                    }

                    i = 0;
                    while (infoStructureSlider.Read())
                    {

                        //la forma en que se maneja la informacion en cada panel de informacion depende de su diseño. en este caso, solo hay uno

                        icoFullName = (string)infoStructureSlider["icon"];
                        bullets[i].sprite = icoFullName.Split('.')[0].ToString();
                        fontColor = (string)infoStructureSlider["fontColor"];
                        
                        switch (fontColor.Trim())
                        {

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

                        bullets[i].texto = "["+hexaColor+"]"+(string)infoStructureSlider["text"]+"[-]";
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

                        }
                        i++;
                    }

                    d.panelInformacionBullets[0].inicializar((string)sliders["Title"], bullets);
                    //FIN SEGMENTO

                    g.SetActive(false);
                    diapositivas.Add(g);

                    break;
            }
          //  i++;

        }

        diapositivas.Add(diapositivaPreguntas.gameObject);
    }

    public void cambiarDiapositiva(bool adelante)
    {
        diapositivaActual = Mathf.Clamp(diapositivaActual + (adelante ? 1 : -1), 0, diapositivas.Count);
        for(int i = 0; i < diapositivas.Count; i++)
        {
            ((GameObject)diapositivas[i]).SetActive(i == diapositivaActual);
        }
    }

   
	
	// Update is called once per frame
	void Update () {
	
	}
}
