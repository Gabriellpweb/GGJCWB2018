using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

	public SpawnController[] spawners;
	private int waveNumber;
	private int selectedSpawner;
	public bool endGame = false;

	private static WaveController instance;

	public static WaveController Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType<WaveController> ();
				if (instance == null) {
					GameObject obj = new GameObject ();
					obj.hideFlags = HideFlags.HideAndDontSave;
					instance = obj.AddComponent<WaveController> ();
				}
			}
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		waveNumber = 0;
		InvokeRepeating ("changeWave", 0, 2f);
	}

	private void changeWave() {

		if (selectedSpawner != null) {
			spawners [selectedSpawner].sending = false;
		}

		if (endGame) {
			return;
		}

		selectedSpawner = Random.Range (0, 7);
		spawners [selectedSpawner].sending = true;
		waveNumber++;
	}

	public void callApocalipse () {
		endGame = true;

		Enemy[] enemys = GameObject.FindObjectsOfType<Enemy> ();

		Debug.Log (enemys.Length);

		foreach (Enemy enemy in enemys) {
			enemy.Destruct ();
		}

		GameObject apocalipse = (GameObject)Instantiate(
			Resources.Load<GameObject>("Prefabs/Apocalipse"),
			new Vector3(-3885, 3531, 0),
			Quaternion.identity
		);

		apocalipse.GetComponent<EnemyFollow> ().setTarget (spawners [selectedSpawner].target);
	}
}
