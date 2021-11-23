using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
	[SerializeField] [Range(0f, 4f)] float lerpSpeed;

	public float speed;
	public float jumpForce;
	public float checkRadius;
	public float pos = 0f;
	public bool isGrounded;
	public bool isJumping;
	public bool isAlive;
	public GameObject explosionFX;
	public Transform groundCheck;
	public LayerMask groundMask;

	private float moveInput;
	private float jumpInput;
	private Rigidbody2D rb;
	private Vector3 jumpPos;
	private Vector3 landPos;

	void Start(){
		rb = GetComponent<Rigidbody2D>();
		isJumping = false;
		isAlive = true;
		jumpForce = 1f;
    }
	
    void FixedUpdate(){

		isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundMask);
		jumpInput = Input.GetAxis("Jump");

		moveInput = Input.GetAxisRaw("Horizontal");
		rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
		if (transform.position.x <= 8f && transform.position.x >= -8f) {
		}
	}

	private void Update() {
		jumpPos = new Vector3(transform.position.x, 3f, transform.position.z);
		landPos = new Vector3(transform.position.x, -3f, transform.position.z);

		if ((Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space)) && isGrounded && !isJumping) {
			isJumping = true;
			pos = 0f;
		}
		if (isJumping && transform.position != jumpPos) {
			transform.position = Vector2.Lerp(transform.position, jumpPos, pos);
			pos += lerpSpeed * Time.deltaTime;
		}
		if (!isGrounded && (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space))) {
			isJumping = false;
			pos = 0;
		}
		if(!isJumping && transform.position != landPos) {
			transform.position = Vector2.Lerp(transform.position, landPos, pos);
			pos += lerpSpeed * Time.deltaTime;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag == "spike") {
			Debug.Log("Hit spike");
			GameObject explode = Instantiate(explosionFX);
			explode.transform.position = transform.position;
			Destroy(explode, 2f);
			isAlive = false;
			transform.gameObject.SetActive(false);
		}
		else if (collision.tag == "pinkBullet") {
			Debug.Log("bounced pink bullet");
			collision.GetComponent<BulletMove>().isBounced = true;
			Debug.Log("Pink bullet's status is: " + collision.GetComponent<BulletMove>().isBounced);
			collision.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
		}
	}
}
