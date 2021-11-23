using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour{

	public GameObject ceilSpawn;
	public GameObject floorSpawn;
	public GameObject ceilSpike;
	public GameObject floorSpike;
	private PlayerController player;

	public float timeInterval;
	public float timeElapsed;
	public float side;
	public bool lastPosUp;
	public bool isSpiking;

	void Start(){
		timeElapsed = 0f;
		player = FindObjectOfType<PlayerController>();
		isSpiking = true;
	}
	
    void Update(){
		if(player.isActiveAndEnabled && isSpiking) {
			timeElapsed += 1f * Time.deltaTime;
			if (timeElapsed >= timeInterval) {
				timeInterval = Random.Range(0.4f, 1f);
				timeElapsed = 0f;
				side = Random.Range(0f, 1f);
				if (lastPosUp) {
					side += 0.2f;
				}
				else {
					side -= 0.2f;
				}
				if (side < 0.5f) {
					//Debug.Log("Instantiating ceiling spike");
					lastPosUp = true;
					GameObject spikeCeiling = Instantiate(ceilSpike, ceilSpawn.transform);
					Destroy(spikeCeiling, 5f);
				}
				else {
					//Debug.Log("Instantiating floor spike");
					lastPosUp = false;
					GameObject spikeFloor = Instantiate(floorSpike, floorSpawn.transform);
					Destroy(spikeFloor, 5f);
				}

			}
		}
    }
}
