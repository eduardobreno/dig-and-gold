using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void NextLevel(){
		StartCoroutine("ChangeLevel");
	}

	IEnumerator ChangeLevel(){
		float fadeTime = this.GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene(Application.loadedLevel + 1);

	}
}
