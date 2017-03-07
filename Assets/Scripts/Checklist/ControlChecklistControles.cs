using UnityEngine;
using System.Collections;

public class ControlChecklistControles : MonoBehaviour {
	public GameObject[] imagenesJoysticks;
	public int joystickFalla = -1;
	public GameObject[] imagenesPedales;
	public int pedalFalla = -1;
	public GameObject[] vistoBueno;
	public GameObject[] vistoMalo;
	// Use this for initialization
	void Start () {

	}

	public void generarFallaJoysticks(){
		joystickFalla = Random.Range (0, imagenesJoysticks.Length - 1);
	}
	
	public void generarFallaPedales(){
		pedalFalla = Random.Range (0, imagenesPedales.Length - 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.JoystickButton9)){ //Boton5i (Jizq BotonGatillo)
			if(joystickFalla == 0){
				vistoMalo[0].SetActive(true);
			}
			else{
				imagenesJoysticks[0].SetActive(true);
				vistoBueno[0].SetActive(true);
			}
		}
		if (Input.GetKeyUp(KeyCode.JoystickButton9)){
			if(joystickFalla == 0){
				vistoMalo[0].SetActive(false);
			}
			else{
				imagenesJoysticks[0].SetActive(false);
				vistoBueno[0].SetActive(false);
			}
		}
		if (Input.GetKeyDown(KeyCode.JoystickButton10)){ //Boton1i (Jizq BotonSupIzq)
			if(joystickFalla == 1){
				vistoMalo[1].SetActive(true);
			}
			else{
				imagenesJoysticks[1].SetActive(true);
				vistoBueno[1].SetActive(true);
			}
		}
		if (Input.GetKeyUp(KeyCode.JoystickButton10)){
			if(joystickFalla == 1){
				vistoMalo[1].SetActive(false);
			}
			else{
				imagenesJoysticks[1].SetActive(false);
				vistoBueno[1].SetActive(false);
			}
		}
		if (Input.GetKeyDown(KeyCode.JoystickButton7)){ //Boton2i (Jizq BotonSupDer)
			if(joystickFalla == 2){
				vistoMalo[1].SetActive(true);
			}
			else{
				imagenesJoysticks[2].SetActive(true);
				vistoBueno[1].SetActive(true);
			}
		}
		if (Input.GetKeyUp(KeyCode.JoystickButton7)){
			if(joystickFalla == 2){
				vistoMalo[1].SetActive(false);
			}
			else{
				imagenesJoysticks[2].SetActive(false);
				vistoBueno[1].SetActive(false);
			}
		}
		if (Input.GetKeyDown(KeyCode.JoystickButton11)){ //Boton3i (Jizq BotonInfIzq)
			if(joystickFalla == 3){
				vistoMalo[1].SetActive(true);
			}
			else{
				imagenesJoysticks[3].SetActive(true);
				vistoBueno[1].SetActive(true);
			}
		}
		if (Input.GetKeyUp(KeyCode.JoystickButton11)){
			if(joystickFalla == 3){
				vistoMalo[1].SetActive(false);
			}
			else{
				imagenesJoysticks[3].SetActive(false);
				vistoBueno[1].SetActive(false);
			}
		}
		if (Input.GetKeyDown(KeyCode.JoystickButton6)){ //Boton4i (Jizq BotonInfDer)
			if(joystickFalla == 4){
				vistoMalo[1].SetActive(true);
			}
			else{
				imagenesJoysticks[4].SetActive(true);
				vistoBueno[1].SetActive(true);
			}
		}
		if (Input.GetKeyUp(KeyCode.JoystickButton6)){
			if(joystickFalla == 4){
				vistoMalo[1].SetActive(false);
			}
			else{
				imagenesJoysticks[4].SetActive(false);
				vistoBueno[1].SetActive(false);
			}
		}
		if (Input.GetKeyDown(KeyCode.JoystickButton3)){ //Boton2d (Jder BotonSupDer)
			if(joystickFalla == 5){
				vistoMalo[2].SetActive(true);
			}
			else{
				imagenesJoysticks[5].SetActive(true);
				vistoBueno[2].SetActive(true);
			}
		}
		if (Input.GetKeyUp(KeyCode.JoystickButton3)){ 
			if(joystickFalla == 5){
				vistoMalo[2].SetActive(false);
			}
			else{
				imagenesJoysticks[5].SetActive(false);
				vistoBueno[2].SetActive(false);
			}
		}
		if (Input.GetKeyDown(KeyCode.JoystickButton2)){ //Boton3d (Jder BotonInfIzq)
			if(joystickFalla == 6){
				vistoMalo[2].SetActive(true);
			}
			else{
				imagenesJoysticks[6].SetActive(true);
				vistoBueno[2].SetActive(true);
			}
		}
		if (Input.GetKeyUp(KeyCode.JoystickButton2)){ 
			if(joystickFalla == 6){
				vistoMalo[2].SetActive(false);
			}
			else{
				imagenesJoysticks[6].SetActive(false);
				vistoBueno[2].SetActive(false);
			}
		}
		if (Input.GetKeyDown(KeyCode.JoystickButton4)){ //Boton4d (Jder BotonInfDer)
			if(joystickFalla == 7){
				vistoMalo[2].SetActive(true);
			}
			else{
				imagenesJoysticks[7].SetActive(true);
				vistoBueno[2].SetActive(true);
			}
		}
		if (Input.GetKeyUp(KeyCode.JoystickButton4)){ 
			if(joystickFalla == 7){
				vistoMalo[2].SetActive(false);
			}
			else{
				imagenesJoysticks[7].SetActive(false);
				vistoBueno[2].SetActive(false);
			}
		}
		if (Input.GetKeyDown(KeyCode.JoystickButton1)){ //Boton5d (Jder BotonGatillo)
			if(joystickFalla == 8){
				vistoMalo[2].SetActive(true);
			}
			else{
				imagenesJoysticks[8].SetActive(true);
				vistoBueno[2].SetActive(true);
			}
		}
		if (Input.GetKeyUp(KeyCode.JoystickButton1)){ 
			if(joystickFalla == 8){
				vistoMalo[2].SetActive(false);
			}
			else{
				imagenesJoysticks[8].SetActive(false);
				vistoBueno[2].SetActive(false);
			}
		}
		if (Input.GetButtonDown ("Fire1")){ //Boton1d (Jder BotonSupIzq)
			if(joystickFalla == 9){
				vistoMalo[3].SetActive(true);
			}
			else{
				imagenesJoysticks[9].SetActive(true);
				vistoBueno[3].SetActive(true);
			}
		}
		if (Input.GetButtonUp ("Fire1")){ 
			if(joystickFalla == 9){
				vistoMalo[3].SetActive(false);
			}
			else{
				imagenesJoysticks[9].SetActive(false);
				vistoBueno[3].SetActive(false);
			}
		}
		//PEDALES
		float throttle = Input.GetAxis ("Joy1 Axis 5");
		float brake = Input.GetAxis ("Joy1 Axis 6");
		#if !UNITY_EDITOR
		throttle = (throttle + 1f) * 0.5f;
		brake = (brake + 1f) * 0.5f;
		#endif
		if (brake != 0f){
			if(pedalFalla == 0){
				vistoMalo[4].SetActive(true);
			}
			else{
				imagenesPedales[0].SetActive(true);
				vistoBueno[4].SetActive(true);
			}
		}
		if (brake == 0f){ 
			if(pedalFalla == 0){
				vistoMalo[4].SetActive(false);
			}
			else{
				imagenesPedales[0].SetActive(false);
				vistoBueno[4].SetActive(false);
			}
		}
		if (throttle != 0f){
			if(pedalFalla == 1){
				vistoMalo[5].SetActive(true);
			}
			else{
				imagenesPedales[1].SetActive(true);
				vistoBueno[5].SetActive(true);
			}
		}
		if (throttle == 0f){ 
			if(pedalFalla == 1){
				vistoMalo[5].SetActive(false);
			}
			else{
				imagenesPedales[1].SetActive(false);
				vistoBueno[5].SetActive(false);
			}
		}
	}
}
