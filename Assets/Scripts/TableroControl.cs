using UnityEngine;
using System.Collections;

public class TableroControl : MonoBehaviour {
    public GameObject[] indicadoresSuperiores;
	public ControlCamion maquina;
	public ControlCamionMotor motor;

	public Transform agujaTemperatura;
	public Transform agujaRevoluciones;
	public Transform agujaPetroleo;
	// Use this for initialization
	void Start () {
		agujaTemperatura = transform.FindChild ("IndicadorTemperatura");
		agujaRevoluciones = transform.FindChild ("IndicadorRevoluciones");
		agujaPetroleo = transform.FindChild ("IndicadorPetroleo");
		if(maquina == null)
			maquina = GameObject.FindGameObjectWithTag ("Maquina").GetComponent<ControlCamion>();
		if(motor == null)
			motor = GameObject.Find ("Delantera_B").GetComponent<ControlCamionMotor>();
	}

	public void encenderStopMotor(bool encender){ indicadoresSuperiores[5].SetActive(!encender); }
	public void encenderCarga(bool encender) { indicadoresSuperiores[6].SetActive(!encender); }
	public void encenderTolva(bool encender) { indicadoresSuperiores[9].SetActive(!encender); }
	public void encenderFrenoParq(bool encender) { indicadoresSuperiores[10].SetActive(!encender); }
    public void encenderReversa(bool encender){ indicadoresSuperiores[13].SetActive(!encender); }
    public void encenderNeutro(bool encender) { indicadoresSuperiores[14].SetActive(!encender); }
    public void encenderAdelante(bool encender) { indicadoresSuperiores[15].SetActive(!encender); }
    public void encenderAuto(bool encender) { indicadoresSuperiores[16].SetActive(!encender); }
    public void encenderManual(bool encender) { indicadoresSuperiores[17].SetActive(!encender); }


	public void setPetroleo(float target){
		//agujaPetroleo.rotation = Quaternion.Euler (0f, 0f, -179f * porcentaje / 100f);
		rotacionAguja(agujaPetroleo,target,-179f);
	}
	public void setRevoluciones(float target){
		//agujaRevoluciones.rotation = Quaternion.Euler (0f, 0f, 15f -210f * porcentaje / 100f);
		rotacionAguja(agujaRevoluciones,target,(15f -210f));
	}
	public void setTemperatura(float target){
		//agujaTemperatura.rotation = Quaternion.Euler (0f, 0f, -179f * porcentaje / 100f);
		rotacionAguja(agujaTemperatura,target,-179f);
	}
    // Update is called once per frame
    void Update () {
		if (maquina != null) {
			/*if (Mathf.Abs(Input.GetAxis ("ControlTolba")) > 0.1f || Mathf.Abs(Input.GetAxis ("ControlTolbaEditor")) > 0.1f)
				encenderTolva (true);
			else
				encenderTolva (false);*/

			if (maquina.estado == ControlCamion.EstadoMaquina.apagada){
				encenderStopMotor (true);
				encenderReversa (false);
				encenderNeutro (false);
				encenderAdelante (false);
				encenderAuto (false);
				encenderManual (false);
			}
			else
				encenderStopMotor (false);
		}
		/*if (motor != null) {
			encenderFrenoParq (motor.frenoParqueoActivado);
		}*/
	}

	void rotacionAguja(Transform a, float p, float pond){
		/*for (int i = 0; i < p; i++) {
			a.rotation = Quaternion.Euler (0f, 0f, -179f*i / 100f);
		}*/
		if (p > 0) {
			Quaternion current = a.rotation;
			Quaternion target = Quaternion.Euler (0f, 0f, pond * p / 100f);
			a.rotation = Quaternion.Lerp (current, target, Time.deltaTime);
		} else {
			Quaternion current = a.rotation;
			Quaternion target = Quaternion.Euler (0f, 0f, 0f);
			a.rotation = Quaternion.Lerp (current, target, Time.deltaTime);
		}
	}
}
