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
        /*
        audioMotor = gameObject.GetComponent<AudioSource> ();
        cambioVelocidad (0);
        */
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
        if (estado == ControlCamion.EstadoMaquina.apagadaTotal)
            return;
        estado = activar? ControlCamion.EstadoMaquina.encendida: ControlCamion.EstadoMaquina.apagada;
        /*if (activar)
            GetComponent<AudioSource>().Play ();
        else { 
            GetComponent<AudioSource>().Stop ();
            GetComponent<AudioSource>().PlayOneShot(apagadoMotor);
        }*/
    }
    // Update is called once per frame
    void FixedUpdate () {
		if (estado == ControlCamion.EstadoMaquina.encendida && !GetComponent<AudioSource>().isPlaying){
			GetComponent<AudioSource>().Play ();
		}

		if (estado != ControlCamion.EstadoMaquina.encendida) {// || central.estado == Central.EstadoSimulacion.Finalizando) {
			//if(central != null && central.estado == Central.EstadoSimulacion.Finalizando){
				//GetComponent<Rigidbody>().velocity = Vector3.zero;
				//GetComponent<Rigidbody>().isKinematic = true;
				//audioMotor.pitch = 0.7f;
				//audioRetroceso.Stop();
			//}
			foreach (WheelCollider w in ruedasConMotor) {
				w.brakeTorque = 500000f;
			}
			return;
		}

        float throttle = controlTarjetaControladora.Acelerador();
		float brake = controlTarjetaControladora.Freno();
        float retardador = controlTarjetaControladora.Retardador();
        if (Input.GetKeyUp(KeyCode.Q))
        {
            retroceso = !retroceso;
            camaraMedicionAdelante.SetActive(!camaraMedicionAdelante.activeSelf);
            camaraMedicionAtras.SetActive(!camaraMedicionAtras.activeSelf);
        }
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
            velocidadActual = Mathf.Lerp(velocidadActual, Mathf.Clamp(Mathf.Clamp(throttle, 0f, 1f) * velocidades[cambioActual], 0f, velocidades[cambioActual]) * 100000f, factorAceleracion * Time.deltaTime);

        if (retardador > 0.5f)
        {
            print("retardador" + retardador);
            velocidadActual = velocidadActual * Mathf.Clamp01(0.9f + 1f / (20f * retardador));
        }
        //print(throttle + " " + brake + " " + velocidadActual);
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
                    print("bajando velocidad " + (cambioActual - 1));
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

		//audioMotor.pitch = Mathf.Clamp (0.7f + velocidadActual / velocidades [Mathf.Clamp(cambioActual, 1, 6)], 0.7f, 1.1f);

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


        //print(GetComponent<Rigidbody>().velocity.magnitude);
    }
    
	void calcularCambio(float throttle){
		if(throttle > 0f){
			print ("cambio up!" + Mathf.RoundToInt(throttle * 6));
			cambioVelocidad(Mathf.RoundToInt(throttle * 6));
		}
    }

    public void cambioVelocidad(int cambio)
    {
        cambioActual = Mathf.Clamp(cambio, 0, 6);
        print("cambio " + cambioActual);
        //transform.parent.gameObject.SendMessage("cambio" + (cambioActual), true);
    }

}
