using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseManager : MonoBehaviour {
	GameObject pause;
	void Start(){
		pause = GameObject.Find ("txtPause");
		pause.SetActive(false);
	}
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			if (Time.timeScale != 0) {
				Time.timeScale = 0;
				pause.SetActive(true);
			} else {
				pause.SetActive(false);
				Time.timeScale =1;
			}
		}	
	}
}
