using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {
	GameObject pause;

	void Start(){
		pause = GameObject.Find ("pauseMenu");
		pause.SetActive(false);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			SoundManager.PlaySFX("pause");
			Pause();
		}

	}

	public void Pause(){
		if (Time.timeScale != 0) {
			Time.timeScale = 0;
			pause.SetActive(true);
		} else {
			pause.SetActive(false);
			Time.timeScale =1;
		}
	}
		
}
