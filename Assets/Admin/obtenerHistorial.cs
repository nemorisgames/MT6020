using UnityEngine;
using System.Collections;

public class obtenerHistorial : MonoBehaviour {
	public GameObject historiales;
	public GameObject opcionesadmin;
	string numeroNivel;
	public string idniv;
	string idhist;
    public GameObject infoGeneral;
    public GameObject intMaquina;
	public GameObject check1;
	public GameObject check2;
	public GameObject zipper;
	public GameObject metaFaena;
	public GameObject tunel;
	public GameObject camion;
	public GameObject preguntas;
	public GameObject tiempo;
	public GameObject ordenEjecucion;
	public GameObject motorPunta; 
	public GameObject baldePunta;
	public GameObject entrega;
	public GameObject entregaSup;
	public GameObject vueltas;
	public GameObject traslado;
	public GameObject termino;

    public GameObject vueltasDetalles;
    public GameObject cicloDetalles;
    public GameObject fallaOperacional;
    public GameObject puntoPartidaFaena;
    public GameObject porcentajeColision;
    public GameObject integridadCamioneta;
    public GameObject integridadCamion;
    public GameObject preguntasFaena;

    public GameObject popup;

    ArrayList vueltasAuxArray = new ArrayList();
    ArrayList cicloAuxArray = new ArrayList();

    GameObject c;
    GameObject v;

    // Use this for initialization
    void Start () {
		apagarTodo ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void apagarTodo(){
		intMaquina.SetActive (false);
		check1.SetActive (false);
		check2.SetActive (false);
		zipper.SetActive (false);
		metaFaena.SetActive (false);
		tunel.SetActive (false);
		camion.SetActive (false);
		tiempo.SetActive (false);
		preguntas.SetActive (false);
		ordenEjecucion.SetActive (false);
		motorPunta.SetActive (false);
		baldePunta.SetActive (false);
		entrega.SetActive (false);
		entregaSup.SetActive (false);
		vueltas.SetActive (false);
		traslado.SetActive (false);
		termino.SetActive (false);

        vueltasDetalles.SetActive(false);
        cicloDetalles.SetActive(false);
        fallaOperacional.SetActive(false);
        puntoPartidaFaena.SetActive(false);
        porcentajeColision.SetActive(false);
        integridadCamioneta.SetActive(false);
        integridadCamion.SetActive(false);
        preguntasFaena.SetActive(false);
    }
	void escogeNivel(){
		apagarTodo ();
		//switch (numeroNivel) {
		

				
		//}
		//gameObject.GetComponent<UIGrid>().Reposition();
		//gameObject.GetComponentInParent<UIScrollView> ().ResetPosition ();
	}
	public void ObtenerHistorial(){

		string hist = historiales.GetComponent<UIPopupList> ().value;
		//print (hist);
		historiales.GetComponent<verHistorial> ().miLista.TryGetValue (hist, out idhist);
		if(idhist != null)
			StartCoroutine (obtenerHistorialEjecutar ());

	}

	public IEnumerator obtenerHistorialEjecutar(){
		WWWForm form = new WWWForm();


		//print (list.value);
		print (idhist);
		//print ("solicitud");
		form.AddField ("id", idhist);
		popup.SetActive (true);
		popup.GetComponent<UILabel>().text="Cargando Información ";
		popup.transform.FindChild ("Boton").gameObject.SetActive (false);
		WWW download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/cargarHistorial.php", form);
		yield return download;
		if (download.error != null) {
			print ("Error downloading: " + download.error);
			//mostrarError("Error de conexion");
			yield break;
		} else {
			//print ("hola");
			string retorno = download.text;

            for(int i = 0; i < vueltasAuxArray.Count; i++)
            {
                if(i > 0) Destroy(((Transform)(vueltasAuxArray[i])).gameObject);
            }
            vueltasAuxArray = new ArrayList();

            for (int i = 0; i < cicloAuxArray.Count; i++)
            {
                if (i > 0) Destroy(((Transform)(cicloAuxArray[i])).gameObject);
            }
            cicloAuxArray = new ArrayList();

            print (retorno);
            string[] retTotal = retorno.Split(new char[] { '|' });
            string[] retDatos = retTotal[0].Split(new char[] { '*' });
            string[] ret = retTotal[1].Split (new char[]{'*'});
            string[] check = retTotal.Length>2?retTotal[2].Split(new char[] { '*' }):null;
            string[] vuelta = retTotal.Length > 3 ? retTotal[3].Split(new char[] { '*' }) : null;
            string[] ciclo = retTotal.Length > 4 ? retTotal[4].Split(new char[] { '*' }) : null;
            //HistorialOpcionAdmin gameObject.GetComponent<HistorialOpcionAdmin>()=gameObject.GetComponent<HistorialOpcionAdmin>();
            infoGeneral.transform.FindChild("Numero ID").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text = "Número ID: " + retDatos[0];
            infoGeneral.transform.FindChild("Repeticion Modulo").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text = "Repetición Módulo: " + retDatos[1];

            gameObject.GetComponent<HistorialOpcionAdmin>().numeroID = retDatos[0];
            gameObject.GetComponent<HistorialOpcionAdmin>().repeticion = retDatos[1];

            string[] fecha = ret[39].Split(new char[] { ' ' });
            //infoGeneral.transform.FindChild("Fecha").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text = "Fecha: " + fecha[2] + " " + fecha[3] + " " + fecha[4];
            infoGeneral.transform.FindChild("Fecha").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text = "Fecha: " + fecha[0] + " " + fecha[1];

            preguntas.transform.FindChild("Resultado Preguntas").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[1]), 0)+" %";
            preguntas.transform.FindChild("CantPreguntas Contestadas").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = (ret[40]==""?"0":ret[40]);
            gameObject.GetComponent<HistorialOpcionAdmin>().cantpreguntascontestadas = (ret[40] == "" ? "0" : ret[40]);
            gameObject.GetComponent<HistorialOpcionAdmin>().aprobacionO=ret[1];
            tiempo.transform.FindChild("Resultado Tiempo").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = calcularReloj(float.Parse(ret[2])); // float.Parse(ret[2]) + " min";//
            gameObject.GetComponent<HistorialOpcionAdmin>().tiempoO=ret[2];

            gameObject.GetComponent<HistorialOpcionAdmin>().vueltasDatos = new ArrayList();
            if (v == null)
                v = vueltasDetalles.transform.FindChild("Vuelta").gameObject;
            if(vuelta != null && vuelta.Length > 1)
            {
                print("hay info");
                float contador = 0f;
                for(int i = 0; i < vuelta.Length; i = i + 2)
                {
                    Transform vueltaAux;
                    if(i == 0)
                    {
                        print("creando " + i);
                        vueltaAux = v.transform;
                    }
                    else
                    {
                        print("creando nuevo " + i);
                        GameObject g = (GameObject)Instantiate(v, v.transform.localPosition + new Vector3(0f, -35f * i / 2, 0f), v.transform.rotation);
                        g.transform.parent = v.transform.parent;
                        g.transform.localPosition = v.transform.localPosition + new Vector3(0f, -35f * i / 2, 0f);
                        g.transform.localScale = v.transform.localScale;
                        vueltaAux = g.transform;
                    }
                    vueltasAuxArray.Add(vueltaAux);
                    vueltaAux.FindChild("Label").gameObject.GetComponent<UILabel>().text = "Vuelta " + vuelta[i];
                    vueltaAux.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = calcularReloj(float.Parse(vuelta[i + 1]));
                    gameObject.GetComponent<HistorialOpcionAdmin>().vueltasDatos.Add(calcularReloj(float.Parse(vuelta[i + 1])));
                    contador += float.Parse(vuelta[i + 1]);
                }

                GameObject g2 = (GameObject)Instantiate(v, v.transform.localPosition, v.transform.rotation);
                g2.transform.parent = v.transform.parent;
                g2.transform.localPosition = v.transform.localPosition + new Vector3(0f, -35f * (vuelta.Length / 2 + 1), 0f);
                g2.transform.localScale = v.transform.localScale;

                vueltasAuxArray.Add(g2.transform);
                g2.transform.FindChild("Label").gameObject.GetComponent<UILabel>().text = "TOTAL";
                g2.transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = calcularReloj(contador);

                gameObject.GetComponent<HistorialOpcionAdmin>().vueltasDatos.Add(calcularReloj(contador));
            }

            if (check != null)
                for (int i = 0; i < check.Length / 58; i++)
                {
                    string[] s = new string[29];
                    string[] r = new string[29];
                    GameObject checkAux = i == 0 ? check1 : check2;
                    checkAux.transform.FindChild("RevFuncional/NivPet/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +0]) == 1 ? "OK" : "X");
                    s[0] = checkAux.transform.FindChild("RevFuncional/NivPet/Estado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("RevFuncional/NivPet/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +1]) == 1 ? "Bien" : "Mal");
                    r[0] = checkAux.transform.FindChild("RevFuncional/NivPet/Resultado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("RevFuncional/NivAceMot/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +2]) == 1 ? "OK" : "X");
                    s[1] = checkAux.transform.FindChild("RevFuncional/NivAceMot/Estado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("RevFuncional/NivAceMot/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +3]) == 1 ? "Bien" : "Mal");
                    r[1] = checkAux.transform.FindChild("RevFuncional/NivAceMot/Resultado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("RevFuncional/NivAceHid/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +4]) == 1 ? "OK" : "X");
                    s[2] = checkAux.transform.FindChild("RevFuncional/NivAceHid/Estado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("RevFuncional/NivAceHid/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +5]) == 1 ? "Bien" : "Mal");
                    r[2] = checkAux.transform.FindChild("RevFuncional/NivAceHid/Resultado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("RevFuncional/EstLuc/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +6]) == 1 ? "OK" : "X");
                    s[3] = checkAux.transform.FindChild("RevFuncional/EstLuc/Estado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("RevFuncional/EstLuc/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +7]) == 1 ? "Bien" : "Mal");
                    r[3] = checkAux.transform.FindChild("RevFuncional/EstLuc/Resultado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("RevFuncional/EstNeu/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +8]) == 1 ? "OK" : "X");
                    s[4] = checkAux.transform.FindChild("RevFuncional/EstNeu/Estado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("RevFuncional/EstNeu/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +9]) == 1 ? "Bien" : "Mal");
                    r[4] = checkAux.transform.FindChild("RevFuncional/EstNeu/Resultado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("RevFuncional/EstRef/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +10]) == 1 ? "OK" : "X");
                    s[5] = checkAux.transform.FindChild("RevFuncional/EstRef/Estado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("RevFuncional/EstRef/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +11]) == 1 ? "Bien" : "Mal");
                    r[5] = checkAux.transform.FindChild("RevFuncional/EstRef/Resultado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("RevFuncional/NivAceTRa/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +12]) == 1 ? "OK" : "X");
                    s[6] = checkAux.transform.FindChild("RevFuncional/NivAceTRa/Estado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("RevFuncional/NivAceTRa/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +13]) == 1 ? "Bien" : "Mal");
                    r[6] = checkAux.transform.FindChild("RevFuncional/NivAceTRa/Resultado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("RevFuncional/Resumen/Resultado").gameObject.GetComponent<UILabel>().text = Configuracion.aproximar(float.Parse(ret[5 * i + 4]), 0) + " %";
                    if(i == 0) gameObject.GetComponent<HistorialOpcionAdmin>().check1por[0] = checkAux.transform.FindChild("RevFuncional/Resumen/Resultado").gameObject.GetComponent<UILabel>().text;
                    else gameObject.GetComponent<HistorialOpcionAdmin>().check2por[0] = checkAux.transform.FindChild("RevFuncional/Resumen/Resultado").gameObject.GetComponent<UILabel>().text;
                     
                    checkAux.transform.FindChild("RevEstructural/Balde/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +14]) == 1 ? "OK" : "X");
                    s[7] = checkAux.transform.FindChild("RevEstructural/Balde/Estado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("RevEstructural/Balde/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +15]) == 1 ? "Bien" : "Mal");
                    r[7] = checkAux.transform.FindChild("RevEstructural/Balde/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 16] != null && check[58 * i + 16] != "") checkAux.transform.FindChild("RevEstructural/Antenas/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +16]) == 1 ? "OK" : "X");
                    s[8] = checkAux.transform.FindChild("RevEstructural/Antenas/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 17] != null && check[58 * i + 17] != "") checkAux.transform.FindChild("RevEstructural/Antenas/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +17]) == 1 ? "Bien" : "Mal");
                    r[8] = checkAux.transform.FindChild("RevEstructural/Antenas/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 18] != null && check[58 * i + 18] != "") checkAux.transform.FindChild("RevEstructural/ArtCen/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +18]) == 1 ? "OK" : "X");
                    s[9] = checkAux.transform.FindChild("RevEstructural/ArtCen/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 19] != null && check[58 * i + 19] != "") checkAux.transform.FindChild("RevEstructural/ArtCen/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +19]) == 1 ? "Bien" : "Mal");
                    r[9] = checkAux.transform.FindChild("RevEstructural/ArtCen/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 20] != null && check[58 * i + 20] != "") checkAux.transform.FindChild("RevEstructural/PasGen/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +20]) == 1 ? "OK" : "X");
                    s[10] = checkAux.transform.FindChild("RevEstructural/PasGen/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 21] != null && check[58 * i + 21] != "") checkAux.transform.FindChild("RevEstructural/PasGen/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +21]) == 1 ? "Bien" : "Mal");
                    r[10] = checkAux.transform.FindChild("RevEstructural/PasGen/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 22] != null && check[58 * i + 22] != "") checkAux.transform.FindChild("RevEstructural/Fugas/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +22]) == 1 ? "OK" : "X");
                    s[11] = checkAux.transform.FindChild("RevEstructural/Fugas/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 23] != null && check[58 * i + 23] != "") checkAux.transform.FindChild("RevEstructural/Fugas/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +23]) == 1 ? "Bien" : "Mal");
                    r[11] = checkAux.transform.FindChild("RevEstructural/Fugas/Resultado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("RevEstructural/Resumen/Resultado").gameObject.GetComponent<UILabel>().text = Configuracion.aproximar(float.Parse(ret[5 * i + 6]), 0) + " %";
                    if (i == 0) gameObject.GetComponent<HistorialOpcionAdmin>().check1por[1] = checkAux.transform.FindChild("RevEstructural/Resumen/Resultado").gameObject.GetComponent<UILabel>().text;
                    else gameObject.GetComponent<HistorialOpcionAdmin>().check2por[1] = checkAux.transform.FindChild("RevEstructural/Resumen/Resultado").gameObject.GetComponent<UILabel>().text;

                    if (check[58 * i + 24] != null && check[58 * i + 24] != "") checkAux.transform.FindChild("RevCabina/LucGen/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +24]) == 1 ? "OK" : "X");
                    s[12] = checkAux.transform.FindChild("RevCabina/LucGen/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 25] != null && check[58 * i + 25] != "") checkAux.transform.FindChild("RevCabina/LucGen/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +25]) == 1 ? "Bien" : "Mal");
                    r[12] = checkAux.transform.FindChild("RevCabina/LucGen/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 26] != null && check[58 * i + 26] != "") checkAux.transform.FindChild("RevCabina/LimPar/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +26]) == 1 ? "OK" : "X");
                    s[13] = checkAux.transform.FindChild("RevCabina/LimPar/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 27] != null && check[58 * i + 27] != "") checkAux.transform.FindChild("RevCabina/LimPar/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +27]) == 1 ? "Bien" : "Mal");
                    r[13] = checkAux.transform.FindChild("RevCabina/LimPar/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 28] != null && check[58 * i + 28] != "") checkAux.transform.FindChild("RevCabina/AirAco/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +28]) == 1 ? "OK" : "X");
                    s[14] = checkAux.transform.FindChild("RevCabina/AirAco/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 29] != null && check[58 * i + 29] != "") checkAux.transform.FindChild("RevCabina/AirAco/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +29]) == 1 ? "Bien" : "Mal");
                    r[14] = checkAux.transform.FindChild("RevCabina/AirAco/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 30] != null && check[58 * i + 30] != "") checkAux.transform.FindChild("RevCabina/Man/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +30]) == 1 ? "OK" : "X");
                    s[15] = checkAux.transform.FindChild("RevCabina/Man/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 31] != null && check[58 * i + 31] != "") checkAux.transform.FindChild("RevCabina/Man/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +31]) == 1 ? "Bien" : "Mal");
                    r[15] = checkAux.transform.FindChild("RevCabina/Man/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 32] != null && check[58 * i + 32] != "") checkAux.transform.FindChild("RevCabina/MonDis/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +32]) == 1 ? "OK" : "X");
                    s[16] = checkAux.transform.FindChild("RevCabina/MonDis/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 33] != null && check[58 * i + 33] != "") checkAux.transform.FindChild("RevCabina/MonDis/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +33]) == 1 ? "Bien" : "Mal");
                    r[16] = checkAux.transform.FindChild("RevCabina/MonDis/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 34] != null && check[58 * i + 34] != "") checkAux.transform.FindChild("RevCabina/AseCab/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +34]) == 1 ? "OK" : "X");
                    s[17] = checkAux.transform.FindChild("RevCabina/AseCab/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 35] != null && check[58 * i + 35] != "") checkAux.transform.FindChild("RevCabina/AseCab/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +35]) == 1 ? "Bien" : "Mal");
                    r[17] = checkAux.transform.FindChild("RevCabina/AseCab/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 36] != null && check[58 * i + 36] != "") checkAux.transform.FindChild("RevCabina/Boc/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +36]) == 1 ? "OK" : "X");
                    s[18] = checkAux.transform.FindChild("RevCabina/Boc/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 37] != null && check[58 * i + 37] != "") checkAux.transform.FindChild("RevCabina/Boc/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +37]) == 1 ? "Bien" : "Mal");
                    r[18] = checkAux.transform.FindChild("RevCabina/Boc/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 48] != null && check[58 * i + 48] != "") checkAux.transform.FindChild("RevCabina/TemAceMot/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i + 48]) == 1 ? "OK" : "X");
                    s[19] = checkAux.transform.FindChild("RevCabina/TemAceMot/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 49] != null && check[58 * i + 49] != "") checkAux.transform.FindChild("RevCabina/TemAceMot/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i + 49]) == 1 ? "Bien" : "Mal");
                    r[19] = checkAux.transform.FindChild("RevCabina/TemAceMot/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 50] != null && check[58 * i + 50] != "") checkAux.transform.FindChild("RevCabina/TemAceTra/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i + 50]) == 1 ? "OK" : "X");
                    s[20] = checkAux.transform.FindChild("RevCabina/TemAceTra/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 51] != null && check[58 * i + 51] != "") checkAux.transform.FindChild("RevCabina/TemAceTra/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i + 51]) == 1 ? "Bien" : "Mal");
                    r[20] = checkAux.transform.FindChild("RevCabina/TemAceTra/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 52] != null && check[58 * i + 52] != "") checkAux.transform.FindChild("RevCabina/Ventanas/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i + 52]) == 1 ? "OK" : "X");
                    s[21] = checkAux.transform.FindChild("RevCabina/Ventanas/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 53] != null && check[58 * i + 53] != "") checkAux.transform.FindChild("RevCabina/Ventanas/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i + 53]) == 1 ? "Bien" : "Mal");
                    r[21] = checkAux.transform.FindChild("RevCabina/Ventanas/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 54] != null && check[58 * i + 54] != "") checkAux.transform.FindChild("RevCabina/Joystick/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i + 54]) == 1 ? "OK" : "X");
                    s[22] = checkAux.transform.FindChild("RevCabina/Joystick/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 55] != null && check[58 * i + 55] != "") checkAux.transform.FindChild("RevCabina/Joystick/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i + 55]) == 1 ? "Bien" : "Mal");
                    r[22] = checkAux.transform.FindChild("RevCabina/Joystick/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 56] != null && check[58 * i + 56] != "") checkAux.transform.FindChild("RevCabina/Pedales/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i + 56]) == 1 ? "OK" : "X");
                    s[23] = checkAux.transform.FindChild("RevCabina/Pedales/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 57] != null && check[58 * i + 57] != "") checkAux.transform.FindChild("RevCabina/Pedales/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i + 57]) == 1 ? "Bien" : "Mal");
                    r[23] = checkAux.transform.FindChild("RevCabina/Pedales/Resultado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("RevCabina/Resumen/Resultado").gameObject.GetComponent<UILabel>().text = Configuracion.aproximar(float.Parse(ret[5 * i + 5]), 0) + " %";
                    if (i == 0) gameObject.GetComponent<HistorialOpcionAdmin>().check1por[2] = checkAux.transform.FindChild("RevCabina/Resumen/Resultado").gameObject.GetComponent<UILabel>().text;
                    else gameObject.GetComponent<HistorialOpcionAdmin>().check2por[2] = checkAux.transform.FindChild("RevCabina/Resumen/Resultado").gameObject.GetComponent<UILabel>().text;

                    if (check[58 * i + 38] != null && check[58 * i + 38] != "") checkAux.transform.FindChild("PrevRiesgo/EstExtMan/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +38]) == 1 ? "OK" : "X");
                    s[24] = checkAux.transform.FindChild("PrevRiesgo/EstExtMan/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 39] != null && check[58 * i + 39] != "") checkAux.transform.FindChild("PrevRiesgo/EstExtMan/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +39]) == 1 ? "Bien" : "Mal");
                    r[24] = checkAux.transform.FindChild("PrevRiesgo/EstExtMan/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 40] != null && check[58 * i + 40] != "") checkAux.transform.FindChild("PrevRiesgo/EstExtInc/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +40]) == 1 ? "OK" : "X");
                    s[25] = checkAux.transform.FindChild("PrevRiesgo/EstExtInc/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 41] != null && check[58 * i + 41] != "") checkAux.transform.FindChild("PrevRiesgo/EstExtInc/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +41]) == 1 ? "Bien" : "Mal");
                    r[25] = checkAux.transform.FindChild("PrevRiesgo/EstExtInc/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 42] != null && check[58 * i + 42] != "") checkAux.transform.FindChild("PrevRiesgo/EstEsc/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +42]) == 1 ? "OK" : "X");
                    s[26] = checkAux.transform.FindChild("PrevRiesgo/EstEsc/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 43] != null && check[58 * i + 43] != "") checkAux.transform.FindChild("PrevRiesgo/EstEsc/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +43]) == 1 ? "Bien" : "Mal");
                    r[26] = checkAux.transform.FindChild("PrevRiesgo/EstEsc/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 44] != null && check[58 * i + 44] != "") checkAux.transform.FindChild("PrevRiesgo/SalEme/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +44]) == 1 ? "OK" : "X");
                    s[27] = checkAux.transform.FindChild("PrevRiesgo/SalEme/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 45] != null && check[58 * i + 45] != "") checkAux.transform.FindChild("PrevRiesgo/SalEme/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +45]) == 1 ? "Bien" : "Mal");
                    r[27] = checkAux.transform.FindChild("PrevRiesgo/SalEme/Resultado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 46] != null && check[58 * i + 46] != "") checkAux.transform.FindChild("PrevRiesgo/SenMov/Estado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +46]) == 1 ? "OK" : "X");
                    s[28] = checkAux.transform.FindChild("PrevRiesgo/SenMov/Estado").gameObject.GetComponent<UILabel>().text;
                    if (check[58 * i + 47] != null && check[58 * i + 47] != "") checkAux.transform.FindChild("PrevRiesgo/SenMov/Resultado").gameObject.GetComponent<UILabel>().text = (int.Parse(check[58 * i +47]) == 1 ? "Bien" : "Mal");
                    r[28] = checkAux.transform.FindChild("PrevRiesgo/SenMov/Resultado").gameObject.GetComponent<UILabel>().text;
                    checkAux.transform.FindChild("PrevRiesgo/Resumen/Resultado").gameObject.GetComponent<UILabel>().text = Configuracion.aproximar(float.Parse(ret[5 * i + 7]), 0) + " %";
                    if (i == 0) gameObject.GetComponent<HistorialOpcionAdmin>().check1por[3] = checkAux.transform.FindChild("PrevRiesgo/Resumen/Resultado").gameObject.GetComponent<UILabel>().text;
                    else gameObject.GetComponent<HistorialOpcionAdmin>().check2por[3] = checkAux.transform.FindChild("PrevRiesgo/Resumen/Resultado").gameObject.GetComponent<UILabel>().text;

                    if (i == 0)
                    {
                        gameObject.GetComponent<HistorialOpcionAdmin>().check1lista = s;
                        gameObject.GetComponent<HistorialOpcionAdmin>().check1resp = r;
                    }
                    else
                    {
                        gameObject.GetComponent<HistorialOpcionAdmin>().check2lista = s;
                        gameObject.GetComponent<HistorialOpcionAdmin>().check2resp = r;
                    }
                }
            check1.transform.FindChild("Resultado Check1").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[3]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().checkO=ret[3];
            gameObject.GetComponent<HistorialOpcionAdmin>().check1por[4] = ret[3];
            check1.transform.FindChild("Resultado revFunc").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[4]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().revFunc1=ret[4];
			check1.transform.FindChild("Resultado revCab").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[5]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().revCab1=ret[5];
			check1.transform.FindChild("Resultado revEst").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[6]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().revEst1=ret[6];
			check1.transform.FindChild("Resultado prevRiesg").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[7]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().prevRies1=ret[7];

            check2.transform.FindChild("Resultado Check2").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[8]), 0)+" %";
            gameObject.GetComponent<HistorialOpcionAdmin>().checkO2=ret[8];
            gameObject.GetComponent<HistorialOpcionAdmin>().check2por[4] = ret[8];
            check2.transform.FindChild("Resultado revFunc").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[9]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().revFunc2=ret[9];
			check2.transform.FindChild("Resultado revCab").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[10]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().revCab2=ret[10];
			check2.transform.FindChild("Resultado revEst").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[11]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().revEst2=ret[11];
			check2.transform.FindChild("Resultado prevRiesg").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[12]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().prevRies2=ret[12];
            if (ret[41] != "")
            {
                ordenEjecucion.transform.FindChild("Resultado").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = calcularReloj(int.Parse(ret[41]));
                gameObject.GetComponent<HistorialOpcionAdmin>().ordenEj = calcularReloj(int.Parse(ret[41]));
            }
			motorPunta.transform.FindChild("Resultado").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=ret[14];
			gameObject.GetComponent<HistorialOpcionAdmin>().avanceMotor=ret[14];
			baldePunta.transform.FindChild("Resultado").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=ret[15];
			gameObject.GetComponent<HistorialOpcionAdmin>().avanceBalde=ret[15];
			vueltas.transform.FindChild("Resultado Vueltas").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=ret[36];
			gameObject.GetComponent<HistorialOpcionAdmin>().vueltasR=ret[36];
			vueltas.transform.FindChild("Resultado VueltasCorrectas").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=ret[16];
			gameObject.GetComponent<HistorialOpcionAdmin>().vueltasC=ret[16];
			entrega.transform.FindChild("Resultado").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=ret[17];
			gameObject.GetComponent<HistorialOpcionAdmin>().ENO=ret[17];
			entregaSup.transform.FindChild("Resultado").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=ret[37];
			gameObject.GetComponent<HistorialOpcionAdmin>().ENS=ret[37];
			metaFaena.transform.FindChild("Resultado TonelajeTotal").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[18]), 2)+ "tn";
			gameObject.GetComponent<HistorialOpcionAdmin>().tonelajeO=ret[18];
			metaFaena.transform.FindChild("Resultado CaidaMaterial").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[19]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().caidaO=ret[19];
			if(ret[45] != null && ret[45] != "") metaFaena.transform.FindChild("MaximoCarga").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[45]), 0)+" tn";
            else metaFaena.transform.FindChild("MaximoCarga").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = "-";
            if (ret[44] != null && ret[44] != "") metaFaena.transform.FindChild("MinimoCarga").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = Configuracion.aproximar(float.Parse(ret[44]), 0) + " tn";
            else metaFaena.transform.FindChild("MinimoCarga").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = "-";
            //gameObject.GetComponent<HistorialOpcionAdmin>().correctoCarguio=ret[20];
            // metaFaena.transform.FindChild("Resultado Patinaje").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[21]), 0)+" %";
            //gameObject.GetComponent<HistorialOpcionAdmin>().patinaje=ret[21];

            porcentajeColision.transform.FindChild("PorcentajeIntegridad/Resultado").gameObject.GetComponent<UILabel>().text = Configuracion.aproximar(float.Parse(ret[22]), 0) + " %";

            intMaquina.transform.FindChild("Resultado Maquina").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[22]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().intO=ret[22];
			intMaquina.transform.FindChild("Resultado Posterior").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[23]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().postO=ret[23];
			intMaquina.transform.FindChild("Resultado PostIzq").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[24]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().postIO=ret[24];
			intMaquina.transform.FindChild("Resultado PostDer").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[25]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().postDO=ret[25];
			intMaquina.transform.FindChild("Resultado MedioDer").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[26]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().medioDO=ret[26];
			intMaquina.transform.FindChild("Resultado Cabina").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[27]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().cabO=ret[27];
			intMaquina.transform.FindChild("Resultado Brazo").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[28]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().baldeO=ret[28];
			zipper.transform.FindChild("Resultado Zipper").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[30]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().zipperO=ret[29];
			zipper.transform.FindChild("Resultado CantZipper").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=ret[29];
			gameObject.GetComponent<HistorialOpcionAdmin>().cantzipper=ret[30];
			tunel.transform.FindChild("Resultado Tunel").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[31]), 0)+" %";
			gameObject.GetComponent<HistorialOpcionAdmin>().tunelO=ret[31];
			tunel.transform.FindChild("Resultado CantTunel").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=ret[32];
			gameObject.GetComponent<HistorialOpcionAdmin>().canttunel=ret[32];
			//camion.transform.FindChild("Resultado Camion").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=Configuracion.aproximar(float.Parse(ret[33]), 0)+" %";
			//gameObject.GetComponent<HistorialOpcionAdmin>().camionO=ret[33];
			//camion.transform.FindChild("Resultado CantCamion").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=ret[34];
			//gameObject.GetComponent<HistorialOpcionAdmin>().cantcamion=ret[34];
			traslado.transform.FindChild("Resultado").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=ret[35];
			gameObject.GetComponent<HistorialOpcionAdmin>().correctoTraslado=ret[35];
			termino.transform.FindChild("Resultado").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text=ret[36];
			gameObject.GetComponent<HistorialOpcionAdmin>().terminoFaena=ret[36];
			gameObject.GetComponent<HistorialOpcionAdmin>().fecha=gameObject.GetComponent<UIPopupList>().value;
            gameObject.GetComponent<HistorialOpcionAdmin>().cicloDatos = new ArrayList();
            if (c == null)
                c = cicloDetalles.transform.FindChild("CicloCarguio").gameObject;
            if (ciclo != null && ciclo.Length > 1)
            {
                print("hay info ciclo");
                float contadorCarguio = 0f;
                float contadorCaida = 0f;
                float contadorVaciado = 0f;
                float contadorTiempo = 0f;
                for (int i = 0; i < ciclo.Length; i = i + 7)
                {
                    Transform cicloAux;
                    if (i == 0)
                    {
                        print("creando " + i);
                        cicloAux = c.transform;
                    }
                    else
                    {
                        print("creando nuevo " + i);
                        GameObject g = (GameObject)Instantiate(c, c.transform.localPosition + new Vector3(0f, -35f * i / 7, 0f), c.transform.rotation);
                        g.transform.parent = c.transform.parent;
                        g.transform.localPosition = c.transform.localPosition + new Vector3(0f, -35f * i / 7, 0f);
                        g.transform.localScale = c.transform.localScale;
                        cicloAux = g.transform;
                    }
                    cicloAuxArray.Add(cicloAux);
                    cicloAux.FindChild("LabelCiclo").gameObject.GetComponent<UILabel>().text = ciclo[i];
                    cicloAux.FindChild("LabelCarguio").gameObject.GetComponent<UILabel>().text = "" + Configuracion.aproximar(float.Parse(ciclo[i + 1]) / 1000f, 2);
                    cicloAux.FindChild("LabelPatinaje").gameObject.GetComponent<UILabel>().text = ciclo[i + 2]=="1"?"Sí":"No";
                    cicloAux.FindChild("LabelLevante").gameObject.GetComponent<UILabel>().text = ciclo[i + 3] == "1" ? "Sí" : "No";
                    cicloAux.FindChild("LabelCaida").gameObject.GetComponent<UILabel>().text = "" + Configuracion.aproximar(float.Parse(ciclo[i + 4]) / 1000f, 2);
                    cicloAux.FindChild("LabelVaciado").gameObject.GetComponent<UILabel>().text = "" + Configuracion.aproximar(float.Parse(ciclo[i + 5]) / 1000f, 2);
                    cicloAux.FindChild("LabelTiempo").gameObject.GetComponent<UILabel>().text = calcularReloj(float.Parse(ciclo[i + 6]));
                    gameObject.GetComponent<HistorialOpcionAdmin>().cicloDatos.Add(new string[6] { cicloAux.FindChild("LabelCarguio").gameObject.GetComponent<UILabel>().text, cicloAux.FindChild("LabelPatinaje").gameObject.GetComponent<UILabel>().text, cicloAux.FindChild("LabelLevante").gameObject.GetComponent<UILabel>().text, cicloAux.FindChild("LabelCaida").gameObject.GetComponent<UILabel>().text, cicloAux.FindChild("LabelVaciado").gameObject.GetComponent<UILabel>().text, cicloAux.FindChild("LabelTiempo").gameObject.GetComponent<UILabel>().text });
                    contadorCarguio += float.Parse(ciclo[i + 1]);
                    contadorCaida += float.Parse(ciclo[i + 4]);
                    contadorVaciado += float.Parse(ciclo[i + 5]);
                    contadorTiempo += float.Parse(ciclo[i + 6]);
                }

                GameObject g2 = (GameObject)Instantiate(c, c.transform.localPosition, c.transform.rotation);
                g2.transform.parent = c.transform.parent;
                g2.transform.localPosition = c.transform.localPosition + new Vector3(0f, -35f * (ciclo.Length / 7 + 1), 0f);
                g2.transform.localScale = c.transform.localScale;

                cicloAuxArray.Add(g2.transform);
                g2.transform.FindChild("LabelCiclo").gameObject.GetComponent<UILabel>().text = "TOTAL";
                g2.transform.FindChild("LabelCarguio").gameObject.GetComponent<UILabel>().text = "" + Configuracion.aproximar(contadorCarguio / 1000f, 2);
                g2.transform.FindChild("LabelCaida").gameObject.GetComponent<UILabel>().text = "" + Configuracion.aproximar(contadorCaida / 1000f, 2);
                g2.transform.FindChild("LabelVaciado").gameObject.GetComponent<UILabel>().text = "" + Configuracion.aproximar(contadorVaciado / 1000f, 2);
                g2.transform.FindChild("LabelTiempo").gameObject.GetComponent<UILabel>().text = calcularReloj(contadorTiempo);
                gameObject.GetComponent<HistorialOpcionAdmin>().cicloDatos.Add(new string[6] { g2.transform.FindChild("LabelCarguio").gameObject.GetComponent<UILabel>().text, "", "", g2.transform.FindChild("LabelCaida").gameObject.GetComponent<UILabel>().text, g2.transform.FindChild("LabelVaciado").gameObject.GetComponent<UILabel>().text, g2.transform.FindChild("LabelTiempo").gameObject.GetComponent<UILabel>().text });

            }

            fallaOperacional.transform.FindChild("Resultado").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = ret[43]=="1"?"1":"0";
            opcionesadmin.GetComponent<HistorialOpcionAdmin>().fallaOperacionalMaquina = ret[43] != "0" ? "1" : "0";
            puntoPartidaFaena.transform.FindChild("Resultado").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = ret[42];


            if (ret[46] != null && ret[46] != "") integridadCamioneta.transform.FindChild("Resultado Minimo").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = Configuracion.aproximar(float.Parse(ret[46]), 0) + " %";
            if (ret[47] != null && ret[47] != "") integridadCamioneta.transform.FindChild("Resultado Descuento").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = Configuracion.aproximar(float.Parse(ret[47]), 0) + " %";
            if (ret[48] != null && ret[48] != "") integridadCamioneta.transform.FindChild("Resultado Total").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = Configuracion.aproximar(float.Parse(ret[48]), 0) + " %";
            if (ret[43] != null && ret[43] != "") integridadCamioneta.transform.FindChild("Resultado Falla").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = ret[43] == "2" ? "1" : "0";

            if (ret[49] != null && ret[49] != "") integridadCamion.transform.FindChild("Resultado Minimo").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = Configuracion.aproximar(float.Parse(ret[49]), 0) + " %";
            if (ret[50] != null && ret[50] != "") integridadCamion.transform.FindChild("Resultado Descuento").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = Configuracion.aproximar(float.Parse(ret[50]), 0) + " %";
            if (ret[33] != null && ret[33] != "") integridadCamion.transform.FindChild("Resultado Total").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = Configuracion.aproximar(float.Parse(ret[33]), 0) + " %";
            if (ret[43] != null && ret[43] != "") integridadCamion.transform.FindChild("Resultado Falla").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = ret[43] == "3" ? "1" : "0";
            if (ret[49] != null && ret[49] != "") gameObject.GetComponent<HistorialOpcionAdmin>().camionE = Configuracion.aproximar(float.Parse(ret[49]), 0) + " %";
            if (ret[33] != null && ret[33] != "") gameObject.GetComponent<HistorialOpcionAdmin>().camionO = Configuracion.aproximar(float.Parse(ret[33]), 0) + " %";
            if (ret[50] != null && ret[50] != "") gameObject.GetComponent<HistorialOpcionAdmin>().desccamion = Configuracion.aproximar(float.Parse(ret[50]), 0) + " %";

            if (ret[52] != null && ret[52] != "") preguntasFaena.transform.FindChild("Resultado Pregunta1").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = ret[52]=="1"?"OK":"X";
            if (ret[53] != null && ret[53] != "") preguntasFaena.transform.FindChild("Resultado Pregunta2").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = ret[53] == "1" ? "OK" : "X";
            if (ret[54] != null && ret[54] != "") preguntasFaena.transform.FindChild("Resultado Pregunta3").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = ret[54] == "1" ? "OK" : "X";
            if (ret[55] != null && ret[55] != "") preguntasFaena.transform.FindChild("Resultado Pregunta4").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = ret[55] == "1" ? "OK" : "X";

            if (ret[52] != null && ret[52] != "") gameObject.GetComponent<HistorialOpcionAdmin>().pregunta1 = ret[52] == "1" ? "OK" : "X";
            if (ret[53] != null && ret[53] != "") gameObject.GetComponent<HistorialOpcionAdmin>().pregunta2 = ret[53] == "1" ? "OK" : "X";
            if (ret[54] != null && ret[54] != "") gameObject.GetComponent<HistorialOpcionAdmin>().pregunta3 = ret[54] == "1" ? "OK" : "X";
            if (ret[55] != null && ret[55] != "") gameObject.GetComponent<HistorialOpcionAdmin>().pregunta4 = ret[55] == "1" ? "OK" : "X";

            form = new WWWForm();
			form.AddField ("id", ret[0]);
			idniv=ret[0];
			download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/obtenerNumeroNivel.php", form);
			yield return download;
			if (download.error != null) {
				print ("Error downloading: " + download.error);
				//mostrarError("Error de conexion");
				yield return false;
			} else {
			//	print ("numero niv "+ download.text);
				numeroNivel=download.text;
				escogeNivel ();
				opcionesadmin.GetComponent<HistorialOpcionAdmin>().mirarNumeroNivel();

			}
			


			//print ("avisando");
		}
		
	}

	
	string calcularReloj(float tiempo){
		int minutos = (int)tiempo / 60;
		int segundos = (int)tiempo % 60;
		return ((minutos < 10) ? ("0" + minutos) : "" + minutos) + ":" + ((segundos < 10) ? ("0" + segundos) : "" + segundos);
	}
}
