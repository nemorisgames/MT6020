using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Señaleticas : MonoBehaviour {
	public TweenAlpha forward;
	public TweenAlpha left;
	public TweenAlpha right;
	public TweenAlpha stop;
	public int currentIndex = -1;
	SenalTunel [] signs;
	bool active = true;

	// Use this for initialization
	void Start () {
		forward.value = 0;
		left.value = 0;
		right.value = 0;
		stop.value = 0;
		signs = transform.GetComponentsInChildren<SenalTunel> ();
		for (int i = 0; i < signs.Length; i++)
			signs [i].index = i;
		ToggleMidRestart (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (!active) {
			forward.value = 0;
			left.value = 0;
			right.value = 0;
			stop.value = 0;
		}
	}

	public void ShowSign(SenalTunel.direccionSeñal dir, bool b){
		switch (dir) {
		case SenalTunel.direccionSeñal.forward:
			forward.Play (b);
			break;
		case SenalTunel.direccionSeñal.left:
			left.Play (b);
			break;
		case SenalTunel.direccionSeñal.right:
			right.Play (b);
			break;
		case SenalTunel.direccionSeñal.stop:
			stop.Play (b);
			break;
		}
	}

	public void ToggleMidRestart(bool b){
		signs [10].enable (b);
		signs [2].enable (!b);
		signs [1].enable (!b);
		signs [0].enable (!b);
		signs [3].enable (!b);
		signs [13].enable (b);
	}
}
