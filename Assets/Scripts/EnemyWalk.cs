using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
	[Header ("Name of object father's waypoints ")]
	public Transform gmObject;
	[Header ("Object child animator")]
	public Transform child;

	public float speed;

	private List<Transform> waypoints = new List<Transform> ();
	bool walkBack = false;
	int wayPointIndex = 0;

	public new Rigidbody2D rigidbody2D;
	public SpriteRenderer spriterenderer;

	void Start ()
	{
		initWayPoints ();
		rigidbody2D = GetComponent<Rigidbody2D> ();
		spriterenderer = GetComponent<SpriteRenderer> ();
	}


	void Update ()
	{
		checkNextWayPoint ();
		float dir = GetComponent<Rigidbody2D> ().velocity.x;
		if (dir > 0) {
			spriterenderer.flipX = true;
		} else {
			spriterenderer.flipX = false;
		}
	}

	void initWayPoints ()
	{
		Transform children = gmObject;
		for (int i = 0; i < children.childCount; i++) {
			Transform child = children.GetChild (i).transform;
			waypoints.Add (child);
		}
	}

	void checkNextWayPoint ()
	{
		if (waypoints.Count > 1) {
			float dist = Vector2.Distance (transform.position, waypoints [wayPointIndex].position);
			if (dist > 0.3) {
				walkTo (waypoints [wayPointIndex].position);
			} else {
				wayPointIndex = (walkBack) ? --wayPointIndex : ++wayPointIndex;
				if (wayPointIndex == (waypoints.Count - 1))
					walkBack = true;
				if (wayPointIndex == 0)
					walkBack = false;
			}
		}
	}

	void walkTo (Vector2 position)
	{
		Vector2 velocity = new Vector2((transform.position.x - position.x) * speed, (transform.position.y - position.y) * speed);
		rigidbody2D.velocity = -velocity;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		print ("collider enemy");
		if (other.gameObject.CompareTag("Player"))
			Destroy(gameObject);
	}
}
