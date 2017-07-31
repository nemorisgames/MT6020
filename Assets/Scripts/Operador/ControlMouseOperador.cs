using UnityEngine;
using System.Collections;

public class ControlMouseOperador : MonoBehaviour {
	public Vector2 mousePosition2;
	public Camera camara;
	public Camera camaraSecundaria;
	public Texture2D cursor;
	int[] valoresPotenciometro = new int[6];

	ConfiguracionControles configuracionControles;
	ControlTarjetaControladora controlTarjetaControladora;

	public UISprite mouseSprite;
	// Use this for initialization
	void Start () {
		mousePosition2 = new Vector2 (Screen.width / 2f, Screen.height / 2f);
		controlTarjetaControladora = GameObject.FindWithTag("TarjetaControladora").GetComponent<ControlTarjetaControladora>();

		configuracionControles = GameObject.FindWithTag ("Configuracion").GetComponent<ConfiguracionControles> ();
	}

	//0: freno
	//1: acelerador
	//2: joy izq X
	//3: joy izq Y
	//4: joy der X
	//5: joy der Y
	void potenciometros(int[] valores){
		valoresPotenciometro[0] = valores[configuracionControles.idFreno];
		valoresPotenciometro[1] = valores[configuracionControles.idAcelerador];
		valoresPotenciometro[2] = valores[configuracionControles.idJoystickIzquierdoX];
		valoresPotenciometro[3] = valores[configuracionControles.idJoystickIzquierdoY];
		valoresPotenciometro[4] = valores[configuracionControles.idJoystickDerechoX];
		valoresPotenciometro[5] = valores[configuracionControles.idJoystickDerechoY];
	}
	/*
	void BotonUp(int indice){
		if (!this.enabled)
			return;
		indice = indice + 1;
		//print ("recibido " + indice);
		//if(configuracionControles == null) configuracionControles = GameObject.FindWithTag ("Configuracion").GetComponent<ConfiguracionControles> ();
		if(controlTarjetaControladora.BotonAccion() == 1){//indice == configuracionControles.idJDerechoGatillo){
			print ("recibo");
			Ray ray = camara.ScreenPointToRay(new Vector2(mousePosition2.x, Screen.height - mousePosition2.y));
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 1000000)){
				//print ("click " + hit.transform.gameObject.name);
				if(hit.transform.gameObject.activeSelf)
					hit.transform.gameObject.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
				//Debug.DrawLine(ray.origin, hit.point);
			}
			if(camaraSecundaria != null){
				ray = camaraSecundaria.ScreenPointToRay(new Vector2(mousePosition2.x, Screen.height - mousePosition2.y));
				if (Physics.Raycast(ray, out hit, 1000000)){
					//print ("click " + hit.transform.gameObject.name);
					if(hit.transform.gameObject.activeSelf)
						hit.transform.gameObject.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
					//Debug.DrawLine(ray.origin, hit.point);
				}
			}
			return;
		}
	}*/

	/*void OnGUI(){
		float hor = 0f;
		float ver = 0f;
		#if UNITY_EDITOR
		hor = Input.GetAxis("ControlTolba") * 0.1f;
		ver = Input.GetAxis("Acelerador") * 0.1f;
		#else
		hor = ((valoresPotenciometro[4] * 1f) - 512f) / 1024f;
		ver = ((valoresPotenciometro[5] * 1f) - 512f) / 1024f;
		hor = VariablesGlobales.calcularPresicion(hor);
		ver = VariablesGlobales.calcularPresicion(ver);
		#endif
		mousePosition2 += new Vector2 (16f * hor, - 16f * ver);
		mousePosition2 = new Vector2 (Mathf.Clamp (mousePosition2.x, 0f, Screen.width), Mathf.Clamp (mousePosition2.y, 0, Screen.height));
		GUI.Box (new Rect(mousePosition2.x, mousePosition2.y, 12, 24), cursor, GUIStyle.none);

		//GUI.Box(new Rect(Screen.width * 7f / 10f, 0f, 300f, 200f), "freno: " + valoresPotenciometro[0] + "\n" + "acelerador: " + valoresPotenciometro[1]);
		//GUI.Box (new Rect (Screen.width * 7f / 10f, Screen.height / 2f, 400f, 300f), "mousex: " + hor + "\n" + "mousey: " + ver + "\n" + valoresPotenciometro[0] + "\n" + valoresPotenciometro[1] + "\n" + valoresPotenciometro[2] + "\n" + valoresPotenciometro[3] + "\n" + valoresPotenciometro[4] + "\n" + valoresPotenciometro[5]);
	}*/

	void OnEnable(){
		mouseSprite.gameObject.SetActive (true);
	}
	void OnDisable(){
		mouseSprite.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if(camara.transform.root.gameObject.GetComponent<Rigidbody>() != null) Destroy (camara.transform.root.gameObject.GetComponent<Rigidbody>());
		if(camara.transform.parent.gameObject.GetComponent<Rigidbody>() != null) Destroy (camara.transform.parent.gameObject.GetComponent<Rigidbody>());

		if(camaraSecundaria != null && camaraSecundaria.transform.root.gameObject.GetComponent<Rigidbody>() != null) Destroy (camaraSecundaria.transform.root.gameObject.GetComponent<Rigidbody>());
		//if (Input.GetKeyUp(KeyCode.JoystickButton1)) {

		//}
		float hor = 0f;
		float ver = 0f;
		#if UNITY_EDITOR
		hor = Input.GetAxis("ManubrioEditor");
		ver = controlTarjetaControladora.Retardador() + controlTarjetaControladora.Freno();
		camara.GetComponent<UICamera> ().scrollAxisName = "ControlTolbaEditor";
		#else
		//print(Input.GetAxis("Manubrio"));
		hor = Input.GetAxis("Manubrio");
		ver = -controlTarjetaControladora.Retardador() + controlTarjetaControladora.Freno();
		camara.GetComponent<UICamera> ().scrollAxisName = "ControlTolba";
		#endif
		//Debug.Log (hor + ", " + ver);
		mousePosition2 += new Vector2 (16f * hor,  16f * ver);
		mousePosition2 = new Vector2 (Mathf.Clamp (mousePosition2.x, 0f, Screen.width), Mathf.Clamp (mousePosition2.y, 0, Screen.height));
		mouseSprite.transform.localPosition = new Vector2(mousePosition2.x - Screen.width / 2f, Screen.height / 2f - mousePosition2.y);
		

		if(Input.GetKeyDown(KeyCode.E) || Input.GetButton("Fire3")) {//controlTarjetaControladora.BotonAccion() == 0){//indice == configuracionControles.idJDerechoGatillo){
			print ("recibo");
			Ray ray = camara.ScreenPointToRay(new Vector2(mousePosition2.x, Screen.height - mousePosition2.y));
			RaycastHit hit;
		    print(ray.origin + " " + (ray.origin + ray.direction));
			if (Physics.Raycast(ray, out hit, 1000000)){
				print ("click " + hit.transform.gameObject.name);
				if(hit.transform.gameObject.activeSelf)
					hit.transform.gameObject.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);

			}
			if(camaraSecundaria != null){
				ray = camaraSecundaria.ScreenPointToRay(new Vector2(mousePosition2.x, Screen.height - mousePosition2.y));
				if (Physics.Raycast(ray, out hit, 1000000)){
					print ("click " + hit.transform.gameObject.name);
					if(hit.transform.gameObject.activeSelf)
						hit.transform.gameObject.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
					//Debug.DrawLine(ray.origin, hit.point);
				}
			}
			return;
		}

	//mouseSprite.transform.localPosition = new Vector3 (mouseSprite.transform.localPosition.x, mouseSprite.transform.localPosition.y, -10f);
	}
}
