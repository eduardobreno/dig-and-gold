using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour {
	public GameObject mainCamera;
	PauseManager pauseManager;
	ScreenTransition screenTransition;

	// Use this for initialization
	void Start () {
		pauseManager = GetComponent<PauseManager>();
		screenTransition = mainCamera.GetComponent<ScreenTransition>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.T)) {
			StartCoroutine("FadeIn");
		}
		if (Input.GetKeyDown (KeyCode.Y)) {
			StartCoroutine("FadeOut");
		}
	}

	IEnumerator FadeIn() {
		//pauseManager.Pause();
		for (float f = 0f; f <= 1.0f; f += 0.01f) {
			screenTransition.maskValue = f;
			print(screenTransition.maskValue);
			yield return new WaitForSeconds(.01f);
		}
	}

	IEnumerator FadeOut() {
		//pauseManager.Pause();
		for (float f = 1.0f; f >= 0f; f -= 0.01f) {
			screenTransition.maskValue = f;
			print(screenTransition.maskValue);
			yield return new WaitForSeconds(.01f);
		}
	}

}
