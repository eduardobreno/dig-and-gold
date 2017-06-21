using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
public class Controller2D : MonoBehaviour {

	public LayerMask collisionMask;
	public LayerMask collisionPickUpMask;
	public LayerMask collisionKillMask;
	public LayerMask collisionSPMask;
	public LayerMask collisionJumpMask;

	const float skinWidth = .015f;
	public int horizontalRayCount = 4;
	public int verticalRayCount = 4;

	const int verticalPickUpRayCount = 0;

	float horizontalRaySpacing;
	float verticalRaySpacing;

	new BoxCollider2D collider;
	RaycastOrigins raycastOrigins;
	public CollisionInfo collisions;

	public bool isTouching=false;
	public bool isTouchingSP=false;

	public bool isJumpTouching=false;

	public bool isDead;
	public int SPTotal;

	GameObject pickUp;
	GameObject pickUpSP;

	void Start() {
		collider = GetComponent<BoxCollider2D> ();
		SPTotal = GameObject.FindGameObjectsWithTag ("SpecialPoints").Length;

		CalculateRaySpacing ();
	}

	public void Move(Vector3 velocity) {
		UpdateRaycastOrigins ();
		collisions.Reset ();
		if (velocity.x != 0) {
			HorizontalCollisions (ref velocity);
		}
		if (velocity.y != 0) {
			VerticalCollisions (ref velocity);
		}
		print (velocity);
		transform.Translate (velocity);
	}

	void HorizontalCollisions(ref Vector3 velocity) {
		float directionX = Mathf.Sign (velocity.x);
		float rayLength = Mathf.Abs (velocity.x) + skinWidth;
		
		for (int i = 0; i < horizontalRayCount; i ++) {
			Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			RaycastHit2D hitPickUp = Physics2D.Raycast(rayOrigin, Vector2.up * directionX, rayLength, collisionPickUpMask);

			RaycastHit2D killLayer = Physics2D.Raycast(rayOrigin, Vector2.up * directionX, rayLength, collisionKillMask);

			RaycastHit2D sp = Physics2D.Raycast(rayOrigin, Vector2.up * directionX, rayLength, collisionSPMask);


			Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength,Color.red);

			if (hit) {
				velocity.x = (hit.distance - skinWidth) * directionX;
				rayLength = hit.distance;

				collisions.left = directionX == -1;
				collisions.right = directionX == 1;
			}

			if (hitPickUp) {	
				pickUp = hitPickUp.collider.gameObject;
				isTouching = true;
			} 

			if (sp) {	
				pickUpSP = sp.collider.gameObject;
				isTouchingSP = true;
			} 

			if (killLayer) {	
				isDead = true;
			} 


		}
	}

	void VerticalCollisions(ref Vector3 velocity) {
		float directionY = Mathf.Sign (velocity.y);
		float rayLength = Mathf.Abs (velocity.y) + skinWidth;

		for (int i = 0; i < verticalRayCount; i ++) {
			Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
			RaycastHit2D hitPickUp = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionPickUpMask);
			RaycastHit2D killLayer = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionKillMask);
			RaycastHit2D jumpLayer = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionJumpMask);

			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength,Color.red);

			if (hit) {
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;

				collisions.below = directionY == -1;
				collisions.above = directionY == 1;

			}

			if (hitPickUp) {
				pickUp = hitPickUp.collider.gameObject;
				isTouching = true;
			}

			if (killLayer) {	
				isDead = true;
			} 

			if(jumpLayer){
				isJumpTouching = true;
			}


		}
	}

	void UpdateRaycastOrigins() {
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);

		raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
	}

	void CalculateRaySpacing() {
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);

		horizontalRayCount = Mathf.Clamp (horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp (verticalRayCount, 2, int.MaxValue);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}

	struct RaycastOrigins {
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}

	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;

		public void Reset() {
			above = below = false;
			left = right = false;
		}
	}

	public bool IsTouchItem(){
		return isTouching;
	}

	public GameObject PickUp(){
		return pickUp;
	}
	public GameObject PickUpSP(){
		return pickUpSP;
	}
}
