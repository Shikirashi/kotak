using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{

	public SpikeSpawner spikeSpawner;
	public BadmenScript badmen;
	public GameObject controls;
	public float bossDelay;

	private float time;

    void Start(){
		spikeSpawner = FindObjectOfType<SpikeSpawner>();
		badmen = FindObjectOfType<BadmenScript>();
    }
	
    void Update(){
		if (time <= bossDelay) {
			time += 1f * Time.deltaTime;
		}
		else {
			spikeSpawner.isSpiking = false;
			badmen.isBossTime = true;
		}

		if(time >= 5f) {
			controls.SetActive(false);
		}
    }
}
