using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour {

	public RectTransform panelLogo;
	public string sceneToLoad;

	void Start() {
		panelLogo.GetComponent<Animator> ().SetBool ("released", true);

		StartCoroutine("fadeOut");
	}

	void FixedUpdate() {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	IEnumerator fadeOut() {
		yield return new WaitForSeconds (3);

		//não faz nada ¬¬
		panelLogo.GetComponent<Animator> ().SetBool ("released", false);

		SceneManager.LoadScene (sceneToLoad);
	}
}