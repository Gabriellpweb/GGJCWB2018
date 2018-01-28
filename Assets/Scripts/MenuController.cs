using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	private bool pressedKey = false;

	void Start () {
		GameManagerController.Instance.currentLifes = 5;
		GameManagerController.Instance.score = 0;
	}
	
	void Update () {
		if (Input.anyKey && !pressedKey) {
			pressedKey = true;
			StartCoroutine ("loadGame");
		}	
	}

	IEnumerator loadGame() {

		yield return new WaitForSeconds (1);

		GameManagerController.Instance.loadLevel (GameManagerController.Scenes.STAGE1);

	}
}
