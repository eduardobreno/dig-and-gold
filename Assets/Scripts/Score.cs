using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	Text score;
	public int totalPoints = 0;


	void Start(){
		score = GetComponent<Text>();

	}

	void Update () {
		score.text = totalPoints.ToString();
	}
}
