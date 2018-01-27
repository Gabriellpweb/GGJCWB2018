using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverController : MonoBehaviour {

	private bool pressedKey;

	void Start() {
		pressedKey = false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.anyKey && !pressedKey) {
			pressedKey = true;
			StartCoroutine ("loadGame");
		}	
	}

	IEnumerator loadGame() {
		yield return new WaitForSeconds (1);

		GameManagerController.Instance.loadLevel (GameManagerController.Scenes.MENU);
	}
}
