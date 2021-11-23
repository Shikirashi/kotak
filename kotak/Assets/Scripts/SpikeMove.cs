using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMove : MonoBehaviour{

	private Vector2 finalPos;
	private float pos;
	private float lerpSpeed;
	private PlayerController player;

    void Start(){
		lerpSpeed = 10f;
		player = FindObjectOfType<PlayerController>();
    }
	
    void Update() {
		if (player  != null) {
			transform.Translate(-Vector3.right * lerpSpeed * Time.deltaTime);
		}
	}
}
