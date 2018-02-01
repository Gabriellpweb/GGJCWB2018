using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

	public SpawnController[] spawners;
	private int waveNumber;
	private int selectedSpawner;
	public bool endGame = false;
	private int numberOfSpawners = 1;

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
		waveNumber = 1;
		InvokeRepeating ("changeWave", 0, 0.2f);
	}

	private void changeWave() {

		/*if (selectedSpawner != null) {
			spawners [selectedSpawner].sending = false;
		}*/

		if (endGame) {
			return;
		}

		selectedSpawner = Random.Range (0, numberOfSpawners);
		spawners [selectedSpawner].sending = true;
		waveNumber++;

		if (waveNumber % 40 == 0 && numberOfSpawners < 7) {
			numberOfSpawners++;
		}
	}

	public void callApocalipse () {
		endGame = true;

		Enemy[] enemys = GameObject.FindObjectsOfType<Enemy> ();
		MissileBase[] missiles = GameObject.FindObjectsOfType<MissileBase> ();

		foreach (SpawnController spawnController in spawners) {
			spawnController.sending = false;
		}

		foreach (Enemy enemy in enemys) {
			enemy.Destruct ();
		}

		foreach (MissileBase missile in missiles) {
			Destroy (missile.gameObject);
		}

		GameObject apocalipse = (GameObject)Instantiate(
			Resources.Load<GameObject>("Prefabs/Apocalipse"),
			new Vector3(-3885, 3531, 0),
			Quaternion.identity
		);

		apocalipse.GetComponent<EnemyFollow> ().setTarget (spawners [selectedSpawner].target);
		spawners [selectedSpawner].target.GetComponent<ForceFieldBehaviour> ().forceFieldHits = 0;
	}
}
