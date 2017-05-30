using UnityEngine;
using System.Collections;

public class ControlCamionMotor : MonoBehaviour {

    ControlTarjetaControladora controlTarjetaControladora;
	public ControlCamion.EstadoMaquina estado = ControlCamion.EstadoMaquina.apagadaTotal;
    
    public int cambioActual = 0;
    public int[] velocidades;
    public float velocidadActual;

    public bool retroceso = false;
    public float factorRetroceso = 1f;
    public enum TipoCambio {automatico, manual};
    public TipoCambio tipoCambio = TipoCambio.automatico;
    public enum TipoPalanca { reversa, neutral, directa };
    public TipoPalanca tipoPalanca = TipoPalanca.neutral;
    /*
    public GameObject cambioAutomaticoBoton;
    public GameObject[] cambiosBotones;

    public AudioClip apagadoMotor;

    public Central central;
    */
    public WheelCollider[] ruedasConMotor;
    public bool frenoParqueoActivado = true;
    public GameObject camaraMedicionAtras;
    public GameObject camaraMedicionAdelante;

    public InGame ingame;
    public AudioSource audioSource;
    public AudioClip sonidoMotor;
    public AudioClip sonidoBomba;
    public AudioClip sonidoEncendido;
    public AudioClip sonidoRetroceso;
    public AudioClip sonidoPitido;

	GameObject ruedasT;
	GameObject ejeT;

    Transform balde;
    /*

    public AudioClip sonidoClaxon;
    public AudioClip sonidoRetroceso;

    AudioSource audioMotor;
    public AudioSource audioRetroceso;

    int[] valoresPotenciometro = new int[6];

    public bool test = false;

    float tiempoClaxon =  0f;
    // Use this for initialization
    */
    void Start () {
		ruedasT = transform.Find ("../Trasero_B/Ruendas_T").gameObject;
		ejeT = transform.Find ("../Trasero_B/EjeTrasero").gameObject;
        balde = transform.Find("../Trasero_B/Balde");
        /*
        audioMotor = gameObject.GetComponent<AudioSource> ();
        cambioVelocidad (0);
        */
        ingame = GameObject.FindWithTag("InGame").GetComponent<InGame>();
        controlTarjetaControladora = GameObject.FindWithTag("TarjetaControladora").GetComponent<ControlTarjetaControladora>();

#if UNITY_EDITOR
        tipoCambio = TipoCambio.automatico;
#else
        tipoCambio = TipoCambio.manual;
#endif
    }
    /*
    //0: freno
    //1: acelerador
    //2: joy izq X
    //3: joy izq Y
    //4: joy der X
    //5: joy der Y
    void potenciometros(int[] valores){
        valoresPotenciometro = valores;
    }

    public void retrocesoActivado(bool activado){
        retroceso = activado;
    }

    public void sonarClaxon(bool activar){
        if (activar && tiempoClaxon < Time.time) {
            AudioSource a = gameObject.GetComponent<AudioSource> ();
            a.PlayOneShot (sonidoClaxon);
            tiempoClaxon = Time.time + 1.5f;
        }
    }
    
    */
    public void encender(bool activar){
        /*if (estado == ControlCamion.EstadoMaquina.apagadaTotal)
            return;*/
        estado = activar? ControlCamion.EstadoMaquina.encendida: ControlCamion.EstadoMaquina.apagada;
        if (activar)
        {
            audioSource.clip = sonidoMotor;
            audioSource.loop = true;
            audioSource.Play();
            audioSource.PlayOneShot(sonidoEncendido);
			StartCoroutine(ingame.ShakeForSecs(1f));
        }
        else
		{
			//ingame.tableroControl.setRevoluciones (0f);
            audioSource.Stop();
        }
    }
    // Update is called once per frame
    void FixedUpdate () {
		if (estado == ControlCamion.EstadoMaquina.encendida && !audioSource.isPlaying){
			GetComponent<AudioSource>().Play ();
		}

		if (estado != ControlCamion.EstadoMaquina.encendida) {// || central.estado == Central.EstadoSimulacion.Finalizando) {
			//if(central != null && central.estado == Central.EstadoSimulacion.Finalizando){
			//GetComponent<Rigidbody>().velocity = Vector3.zero;
			//GetComponent<Rigidbody>().isKinematic = true;
			//audioMotor.pitch = 0.7f;
			//audioRetroceso.Stop();
			//}
			ingame.tableroControl.setRevoluciones (0f);

			foreach (WheelCollider w in ruedasConMotor) {
				w.brakeTorque = 500000f;
			}
			if (estado == ControlCamion.EstadoMaquina.apagada) {
				audioSource.loop = false;
				if (!audioSource.isPlaying) {
					audioSource.clip = sonidoPitido;
					audioSource.PlayDelayed (4f);
				}
			}
			return;
		}

        float throttle = controlTarjetaControladora.Acelerador();
		float brake = controlTarjetaControladora.Freno();
        float retardador = controlTarjetaControladora.Retardador();
        
        //print(brake + " " + retardador);
#if UNITY_EDITOR
        /*throttle = ((valoresPotenciometro[0] * 1f) - 310f) / 520f;
		brake = ((valoresPotenciometro[1] * 1f) - 310f) / 520f;*/
#endif
        float factorAceleracion = 1f;
		if (throttle > 0) {
			factorAceleracion = 2f / ((cambioActual + 1f));
		} else
			factorAceleracion = 5f;



        //if (tipoCambio != TipoCambio.automatico)
        //	throttle = throttle * cambioActual / 6f;

        if(!frenoParqueoActivado)
            velocidadActual = Mathf.Lerp(velocidadActual, Mathf.Clamp(Mathf.Clamp(throttle, 0f, 1f) * velocidades[cambioActual], 0f, velocidades[cambioActual]) * 130000f, factorAceleracion * Time.deltaTime);

        if (retardador > 0.5f)
        {
            print("retardador" + retardador);
            velocidadActual = velocidadActual * Mathf.Clamp01(0.9f + 1f / (20f * retardador));
        }
        //print(throttle + " " + brake + " " + velocidadActual);
        ingame.tableroControl.encenderManual(tipoCambio != TipoCambio.automatico);
        ingame.tableroControl.encenderAuto(tipoCambio == TipoCambio.automatico);
        ingame.tableroControl.encenderNeutro(cambioActual == 0);
        if (tipoCambio == TipoCambio.automatico) {

            if (retroceso){
			    if(factorRetroceso * velocidadActual > 1f){ 
				    cambioVelocidad(cambioActual - 1);
				    factorRetroceso = 1;
			    }
			    else{
				    calcularCambio(throttle);
				    factorRetroceso = -1;
			    }
		    }
		    else{
			    if(factorRetroceso * velocidadActual < -1f){
                    //print("bajando velocidad " + (cambioActual - 1));
				    cambioVelocidad(cambioActual - 1);
				    factorRetroceso = -1;
			    }
			    else{
				    calcularCambio(throttle);
				    factorRetroceso = 1;
			    }
		    }
		}
        else
        {
            float c = Input.GetAxis("Cambio");
            if (c > 0.9f)
            {
                cambioActual = 1;
                factorRetroceso = 1;
            }
            else
            {
                if(c > 0.6f)
                {
                    cambioActual = 2;
                    factorRetroceso = 1;
                }
                else
                {
                    if (c > 0.1f)
                    {
                        cambioActual = 3;
                        factorRetroceso = 1;
                    }
                    else
                    {
                        if (c > -0.3f)
                        {
                            cambioActual = 4;
                            factorRetroceso = 1;
                        }
                        else
                        {
                            if (c > -0.7f)
                            {
                                cambioActual = 0;
                                factorRetroceso = 1;
                            }
                            else
                            {
                                cambioActual = 1;
                                factorRetroceso = -1;
                            }
                        }
                    }
                }
            }
            print("cambio " + cambioActual);
        }


		ingame.tableroControl.encenderNeutro (false);
		ingame.tableroControl.encenderAdelante (false);
		ingame.tableroControl.encenderReversa (false);
		if (cambioActual != 0 && throttle != 0 && !retroceso)
			ingame.tableroControl.encenderAdelante (true);
		else if (factorRetroceso == -1 && throttle != 0)
			ingame.tableroControl.encenderReversa (true);
		else if(cambioActual == 0)
			ingame.tableroControl.encenderNeutro(true);
#if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.Q))// || factorRetroceso == -1)
		{
			retroceso = !retroceso;
			camaraMedicionAdelante.SetActive(!camaraMedicionAdelante.activeSelf);
			camaraMedicionAtras.SetActive(!camaraMedicionAtras.activeSelf);
		}
#else
            retroceso = factorRetroceso == -1;
            camaraMedicionAdelante.SetActive(factorRetroceso == 1);
            camaraMedicionAtras.SetActive(factorRetroceso == -1);
        
#endif
        //print(velocidadActual + " " + cambioActual);
        audioSource.pitch = Mathf.Clamp (1f + velocidadActual / (velocidades[cambioActual] * 100000f + 1f), 1f, 1.5f);

		foreach (WheelCollider w in ruedasConMotor) {
			w.motorTorque = factorRetroceso * velocidadActual * Time.deltaTime;
            //print(w.motorTorque);
			w.transform.Rotate(new Vector3(w.rpm / 60 * 360 * Time.deltaTime, 0f, 0f));
		}
//		print (brake);
		if (brake > 0.5f || frenoParqueoActivado) {
            //	print ("frenando" + brake);
            foreach (WheelCollider w in ruedasConMotor)
            {
                w.brakeTorque = 500000f * (frenoParqueoActivado ? 1f : brake);
            }
		}
		else
			foreach (WheelCollider w in ruedasConMotor) {
				if(velocidadActual > 1f)
					w.brakeTorque = 0f;
				else{
				//print ("freno");
					w.brakeTorque = 500000f * 0.3f;
				}
			}

		float auxSpeed = Mathf.Clamp((velocidadActual / 100000f)/50f,0.1f,float.MaxValue);
		Debug.Log (auxSpeed);

		if (velocidadActual > 0.1) {
			if (!retroceso)
				//ruedasT.transform.RotateAround (ejeT.transform.position, Vector3.right, auxSpeed / 50f);
				ruedasT.transform.Rotate(new Vector3(auxSpeed,0f,0f));
			else
				//ruedasT.transform.RotateAround (ejeT.transform.position, Vector3.left, auxSpeed / 50f);
				ruedasT.transform.Rotate(new Vector3(-auxSpeed,0f,0f));
		}
        ingame.tableroControl.encenderFrenoParq(frenoParqueoActivado);
		//ingame.tableroControl.encenderReversa(retroceso || factorRetroceso == -1);
		//ingame.tableroControl.encenderAdelante(!(retroceso || factorRetroceso == -1));
		if (estado == ControlCamion.EstadoMaquina.encendida) {
			ingame.tableroControl.setPetroleo (90f);
			ingame.tableroControl.setRevoluciones (Mathf.Clamp(auxSpeed*20f,0f,100f));
			ingame.tableroControl.setTemperatura (20f);
            ingame.tableroControl.encenderTolva(balde.transform.localEulerAngles.x != 0f);
        }

        //print(GetComponent<Rigidbody>().velocity.magnitude);
    }
    
	void calcularCambio(float throttle){
		if(throttle > 0f){
			//print ("cambio up!" + Mathf.RoundToInt(throttle * 6));
			cambioVelocidad(Mathf.RoundToInt(throttle * 6));
		}
    }

    public void cambioVelocidad(int cambio)
    {
        cambioActual = Mathf.Clamp(cambio, 0, 6);
        //print("cambio " + cambioActual);
        //transform.parent.gameObject.SendMessage("cambio" + (cambioActual), true);
    }

	void OnGUI(){
		GUI.Label (new Rect(300f, 200f, 200f, 20f), "Cambio " + cambioActual + " " + Input.GetAxis("Cambio"));
		GUI.Label (new Rect(300f, 220f, 200f, 20f), "Freno " + controlTarjetaControladora.Freno());
		GUI.Label (new Rect(300f, 240f, 200f, 20f), "Retardador " + controlTarjetaControladora.Retardador());
	}

}
