using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


[RequireComponent (typeof(Controller2D))]
public class Player : MonoBehaviour
{

	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;

	float gravity;
	float jumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller2D controller;
	protected Animator animator;
	protected SpriteRenderer spriterenderer;

	public bool isFacingLeft = false;
	public bool isRunning = false;
	public bool isGrounded = false;
	public bool isFalling = false;
	public bool setJumpTrigger = false;
	public bool setFallTrigger = false;
	public bool setLandTrigger = false;

	protected string animatorIsRunning = "isRunning";
	protected string animatorJump = "Jump";
	protected string animatorFall = "Fall";
	protected string animatorLand = "Land";

	//public bool isTouchItem;

	void Start ()
	{
		controller = GetComponent<Controller2D> ();
		animator = GetComponent<Animator> ();
		spriterenderer = GetComponent<SpriteRenderer> ();

		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;
		print ("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
	}

	void Update ()
	{
		UpdateAnimatorParameters ();
		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));	

		spriterenderer.flipX = isFacingLeft;

		if(controller.isTouching){
			velocity.y = jumpVelocity;
			Destroy (controller.PickUp());
			GameObject.Find ("txtScore").GetComponent<Score> ().totalPoints++;
			controller.isTouching = false;
		}
		if(controller.isTouchingSP){
			print ("aqui");
			velocity.y = jumpVelocity;
			Destroy (controller.PickUpSP());
			GameObject.Find ("txtScore").GetComponent<Score> ().totalPoints +=1000;
			controller.isTouchingSP = false;
		}



		if (Input.GetKeyDown (KeyCode.Space) && controller.collisions.below) {
			velocity.y = jumpVelocity;
		}

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX,	ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;

		controller.Move (velocity * Time.deltaTime);

		if (input.x < 0 && !isFacingLeft) {
			isFacingLeft = true;
		} else if (input.x > 0 && isFacingLeft) {
			isFacingLeft = false;
		}
		if (input.x != 0f) {
			isRunning = true;
		} else {
			isRunning = false;
		}
		isGrounded = GetComponent<Controller2D> ().collisions.below;
		setJumpTrigger = !isGrounded;

		if (GetComponent<Controller2D> ().isDead) {
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		}
		//isFalling = !GetComponent<Controller2D> ().collisions.below;
		//setFallTrigger = isFalling;
	}
	void Awake()
	{
		// Setup animator parameters
		animatorIsRunning = "isRunning";
		animatorFall = "Fall";
		animatorJump = "Jump";
		animatorLand = "Land";

	}

	void UpdateAnimatorParameters ()
	{
		animator.SetBool (animatorIsRunning, isRunning);

		if (setJumpTrigger) {
			animator.SetTrigger (animatorLand);
			setJumpTrigger = false;
		} else {
			animator.ResetTrigger (animatorJump);
		}

		if (setFallTrigger) {
			animator.SetTrigger (animatorFall);
			setFallTrigger = false;
		} else {
			animator.ResetTrigger (animatorFall);
		}

		if (setLandTrigger) {
			animator.SetTrigger (animatorLand);
			setLandTrigger = false;
		} else {
			animator.ResetTrigger (animatorLand);
		}
	}

}
