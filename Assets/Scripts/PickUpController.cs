using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour {

	public Transform gmObject;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

//		if (Collider2D.IsTouching(gmObject)) {
//			print ("COLLIDER TOUCHING!!!!!");
////			if (other.gameObject.tag == "Death") {
////				transform.position = new Vector2 (-6,0);
////				LivesLeft --;
////			}
//		}
		if (GetComponent<PolygonCollider2D> ().IsTouching (GameObject.FindWithTag ("Player").GetComponent<BoxCollider2D> ())) {
			print ("COLLIDER TOUCHING!!!!!");
			//	isTouch = "Touch Yes";
			//GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);
		}


	}

//	//OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
	void OnTriggerEnter2D(Collider2D other) 
	{
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.tag == "Player") {
			print ("COLLIDER!!!!!");
		} else {
			print ("NOT COLLIDER!!!!!");
		}
	}

//	void OnCollisionEnter2D(Collision2D other) {
//		if (other.gameObject.tag == "Player") {
//			//other.gameObject.SendMessage ("ApplyDamage", 10);
//			print ("COLLIDER!!!!!");
//		} else {
//			print ("COLLIDER!!!!!");
//		}
//
//	}
}
