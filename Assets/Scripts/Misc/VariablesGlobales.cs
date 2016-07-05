using UnityEngine;
using System.Collections;

public static class VariablesGlobales {
	public static string direccion = "http://localhost/";
	public static float calcularPresicion(float potenciometro){
		potenciometro = ((Mathf.RoundToInt(potenciometro * 100f) * 1f) / 100f);
		if(Mathf.Abs(potenciometro) < 0.1f) potenciometro = 0f;
		return potenciometro;
	}
}
