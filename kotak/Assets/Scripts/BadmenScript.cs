using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadmenScript : MonoBehaviour {
	[SerializeField] [Range(0f, 4f)] float lerpSpeed;

	public GameObject ceilSpawn;
	public GameObject floorSpawn;
	public GameObject bullet;
	public GameObject pinkyBullet;
	public GameObject explosionFX;
	public GameObject smolExplosionFX;
	public HealthbarBehavior healthbar;

	public float timeInterval;
	public float timeElapsed;
	public float pos = 0f;
	public float side;
	public float health;
	public float maxHealth;
	public bool lastPosUp;
	public bool isReady;
	public bool isBossTime;

	private float pinkBullet;
	private PlayerController player;
	private Vector3 upPos;
	private Vector3 downPos;
	public Vector3 nextPos;
	private Vector3 readyPos;

	void Start() {
		maxHealth = 100f;
		health = maxHealth;
		healthbar.SetHealth(health, maxHealth);
		timeElapsed = 0f;
		isReady = false;
		isBossTime = false;
		player = FindObjectOfType<PlayerController>();
		readyPos = new Vector3(6.2f, 0f, transform.position.z);
	}

	private void FixedUpdate() {
		if (!isReady && isBossTime) {
			transform.position = Vector2.Lerp(transform.position, readyPos, lerpSpeed * Time.deltaTime);
			if (transform.position.x <= 6.3f) {
				isReady = true;
				upPos = new Vector3(6.2f, 1.3f, transform.position.z);
				downPos = new Vector3(6.2f, -1.36f, transform.position.z);
				nextPos = downPos;
			}
		}

		if (player.isActiveAndEnabled && isReady) {
			transform.position = Vector2.Lerp(transform.position, nextPos, lerpSpeed * Time.deltaTime);

			if (transform.position.y >= 1.25f) {
				nextPos = downPos;
			}
			else if (transform.position.y <= -1.3f) {
				nextPos = upPos;
			}
		}
	}

	void Update() {
		if (health <= 0f) {
			GameObject explode = Instantiate(explosionFX);
			explode.transform.position = transform.position;
			Destroy(explode, 2f);
			transform.gameObject.SetActive(false);
		}

		if (player.isActiveAndEnabled && isReady) {
			timeElapsed += 1f * Time.deltaTime;
			if (timeElapsed >= timeInterval) {
				timeInterval = Random.Range(0.4f, 1f);
				timeElapsed = 0f;
				side = Random.Range(0f, 1f);
				pinkBullet = Random.Range(0f, 1f);
				if (lastPosUp) {
					side += 0.2f;
				}
				else {
					side -= 0.2f;
				}
				if (side < 0.5f) {
					//Debug.Log("Instantiating ceiling spike");
					lastPosUp = true;
					if(pinkBullet <= .2f) {
						GameObject bulletCeiling = Instantiate(pinkyBullet);
						bulletCeiling.transform.position = ceilSpawn.transform.position;
						Destroy(bulletCeiling, 5f);
					}
					else {
						GameObject bulletCeiling = Instantiate(bullet);
						bulletCeiling.transform.position = ceilSpawn.transform.position;
						Destroy(bulletCeiling, 5f);
					}
				}
				else {
					//Debug.Log("Instantiating floor spike");
					lastPosUp = false;
					if (pinkBullet <= .2f) {
						GameObject bulletFloor = Instantiate(pinkyBullet);
						bulletFloor.transform.position = floorSpawn.transform.position;
						Destroy(bulletFloor, 5f);
					}
					else {
						GameObject bulletFloor = Instantiate(bullet);
						bulletFloor.transform.position = floorSpawn.transform.position;
						Destroy(bulletFloor, 5f);
					}
				}

			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag == "pinkBullet" && collision.GetComponent<BulletMove>().isBounced) {
			Debug.Log("Hit by pink bullet");
			GameObject explode = Instantiate(smolExplosionFX);
			explode.transform.position = collision.transform.position;
			Destroy(explode, 2f);
			Destroy(collision);
			health -= 10f;
			healthbar.SetHealth(health, maxHealth);
		}
	}
}
