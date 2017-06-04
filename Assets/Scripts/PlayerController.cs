using UnityEngine;
using System.Collections;

public class PlayerController : CharacterController2D
{

	void Awake()
	{
		// Setup animator parameters
		animatorIsRunning = "isRunning";
		animatorFall = "Fall";
		animatorJump = "Jump";
		animatorLand = "Land";



	}
}
