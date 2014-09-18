﻿using UnityEngine;
using System.Collections;

public class PlayerBallControl : MonoBehaviour {

	public float moveForce = 150f;			// Amount of force added to move the player left and right
	public float moveTorque = 150f; 
	public float stoppingForceMultiplier = 2.5f;
	public float maxSpeed = 5f;
	public float maxAngularSpeed = 100.0f;
	public float bounceForce = 0.2f;
	public float maxBounce = 0.8f;
	public float minBounce = 0.1f;
	public float jumpForce = 100f;

	//private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.

	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update() {
		//Sprite sprite = GetComponent<SpriteRenderer>();
		//do line cast to ground
		//Transform groundVec = new Transform(transform.position.x,
		 //                               transform.position.y - transform.localScale.y * sprite.bounds.size.y,
		  //                              transform.position.z);

		//grounded = Physics2D.Linecast(transform.position, groundVec.position, 1 << LayerMask.NameToLayer("Ground"));  

	}

	void OnCollisionEnter2D(Collision2D collision) {
		foreach (ContactPoint2D contact in collision.contacts)
			Debug.DrawRay (contact.point, contact.normal, Color.white);
		foreach (ContactPoint2D contact in collision.contacts)
		{
			if (contact.normal.y > 0)
				grounded = true;
		}
		//Deform (collision);
		//ListenForJump (collision);
	}

	void OnCollisionStay2D(Collision2D collision) {
		foreach (ContactPoint2D contact in collision.contacts)
			Debug.DrawRay (contact.point, contact.normal, Color.yellow);
		foreach (ContactPoint2D contact in collision.contacts)
		{
			if (contact.normal.y > 0)
				grounded = true;
		}
		//Reform ();
		//ListenForJump (collision);
		//Deform (collision);
	}

	void OnCollisionExit2D(Collision2D collision) {
		//Reform ();
		//print ("exited");
	}

	private void ListenForJump(Collision2D collision) {
		float v = Input.GetAxis ("Vertical");
		foreach (ContactPoint2D contact in collision.contacts)
		{
			if (v > 0 && contact.normal.y > 0)
			{
				this.rigidbody2D.AddForce(Vector2.up * jumpForce);
				break;
			}
		}
	}

	private void Deform(Collision2D collision) {
		float angle = this.transform.rotation.z;
		Vector3 newScale = new Vector3 (2f - Mathf.Sin (angle), 2f - Mathf.Cos (angle), 1f);
		this.transform.localScale = newScale;
		print (angle);
	}

	private void Reform() {
		Vector3 newScale = new Vector3 (2f, 2f, 1f);
		this.transform.localScale = newScale;
	}

	void FixedUpdate () {

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		if (h != 0) 
		{
			// Check if we want to move in direction opposite motion
			// If so, we allow for faster stopping
			if (Mathf.Sign (h) != Mathf.Sign (this.rigidbody2D.velocity.x)) 
			{
				this.rigidbody2D.AddForce (Vector2.right * h * moveForce * stoppingForceMultiplier);
				this.rigidbody2D.AddTorque (-h * moveTorque * stoppingForceMultiplier);
			} 
			else
			{
				if (Mathf.Abs (this.rigidbody2D.velocity.x) < maxSpeed)
				{
					this.rigidbody2D.AddForce(Vector2.right * h * moveForce);
				}
				if (Mathf.Abs (this.rigidbody2D.angularVelocity) < maxAngularSpeed)
				{
					this.rigidbody2D.AddTorque(-h * moveTorque);
				}
			}
			Debug.Log("" + rigidbody2D.angularVelocity + " " + rigidbody2D.velocity.magnitude);
		}

		// cap translational speed and angular speed
		/*
		if (Mathf.Abs (this.rigidbody2D.velocity.x) > maxSpeed)
		{
			this.rigidbody2D.velocity = new Vector2(
				Mathf.Sign(this.rigidbody2D.velocity.x) * maxSpeed, this.rigidbody2D.velocity.y);
		}
		if (Mathf.Abs (this.rigidbody2D.angularVelocity) > maxAngularSpeed)
		{
			this.rigidbody2D.angularVelocity = Mathf.Sign(
				this.rigidbody2D.angularVelocity) * maxAngularSpeed;
		}
		*/

		/*
		if(h * this.rigidbody2D.velocity.x < maxSpeed)
		{
			// translational movement
			this.rigidbody2D.AddForce(Vector2.right * h * moveForce);

			//Allow faster stopping
			if(Mathf.Sign(h) != Mathf.Sign (this.rigidbody2D.velocity.x))
			{
				this.rigidbody2D.AddForce(Vector2.right * h * Mathf.Abs(this.rigidbody2D.velocity.x)* moveForce * stoppingForceMultiplier);
				this.rigidbody2D.AddTorque(Vector2.right * h * Mathf.Abs(this.rigidbody2D.velocity.x)* moveTorque * stoppingForceMultiplier);
			}

			//Add torque based on direction
			if(Mathf.Abs (this.rigidbody2D.angularVelocity) < maxTorque)
			{
				this.rigidbody2D.AddTorque(-h * moveForce);
			}

		}
		*/

		/*
		if(Mathf.Abs(this.rigidbody2D.velocity.x) > maxSpeed)
			this.rigidbody2D.velocity = new Vector2(Mathf.Sign(this.rigidbody2D.velocity.x) * maxSpeed, this.rigidbody2D.velocity.y);
		*/
		if (v > 0 && grounded) {
			this.rigidbody2D.AddForce (Vector2.up * jumpForce);
		}
		grounded = false;	
	}
}
