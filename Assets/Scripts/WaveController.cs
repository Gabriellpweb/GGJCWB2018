using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

	public SpawnController[] spawners;
	private int waveNumber;
	private int selectedSpawner;

	// Use this for initialization
	void Start () {
		waveNumber = 0;
		InvokeRepeating ("changeWave", 0, 2f);
	}

	private void changeWave() {
		if (selectedSpawner != null) {
			spawners [selectedSpawner].sending = false;
		}

		selectedSpawner = Random.Range (0, 7);
		spawners [selectedSpawner].sending = true;
		waveNumber++;
	}
}
