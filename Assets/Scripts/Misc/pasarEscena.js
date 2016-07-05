var tiempo:float=3.0;
var escena:String;

//private var boton:GUI.Button;
function Awake(){

}
function OnGUI(){

}
function Update () {
	if(tiempo!=-1){
		//tiempo-=Time.deltaTime;
		if(tiempo<=Time.timeSinceLevelLoad){ 
			Application.LoadLevel(escena); 
		}
	}
}