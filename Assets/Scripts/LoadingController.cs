using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour {

	void Start () {
		StartCoroutine ("loadScene");
	}

	IEnumerator loadScene() {
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene (PlayerPrefs.GetString("sceneToLoad"));
	}
}
