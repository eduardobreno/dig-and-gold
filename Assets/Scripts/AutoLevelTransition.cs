using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLevelTransition : MonoBehaviour {

	public int waitTime = 2;

	// Use this for initialization
	void Start () {
		StartCoroutine("AutoChangeLevel");
	}
	
	IEnumerator AutoChangeLevel(){
		yield return new WaitForSeconds (waitTime);
		float fadeTime = this.GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene(Application.loadedLevel + 1);

	}

}
