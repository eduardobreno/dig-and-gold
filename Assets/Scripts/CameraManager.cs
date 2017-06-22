using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	// player
	public Transform playerToFollow;

	// walls
	public Transform Firstwall;
	public Transform Lastwall;

	// camera rules
	public float Height;
	public float Zoom;
	public float max;
	public float min;

	void Start () {
//		min = Firstwall.position + 2f;
	//	max = Lastwall.position - 2f;
	}

	void Update () {

		// if players position > min and player position < max
		if (playerToFollow.position.x > min && playerToFollow.position.x < max)
		{
			// change camera's position = players position.x height.y zoom.z
			transform.position = new Vector3 (playerToFollow.position.x, Height, Zoom);                
		}
	}
}
