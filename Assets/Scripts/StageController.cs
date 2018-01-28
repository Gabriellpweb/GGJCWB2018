using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour {
	
	public Text txtAmountScore;
	public Text txtForceFieldAmount;

	private static StageController instance;

	public static StageController Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType<StageController> ();
				if (instance == null) {
					GameObject obj = new GameObject ();
					obj.hideFlags = HideFlags.HideAndDontSave;
					instance = obj.AddComponent<StageController> ();
				}
			}
			return instance;
		}
	}

	void Start() {
		InvokeRepeating ("updateScore", 0.4f, 0.4f);
		GameManagerController.Instance.score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		txtAmountScore.text = GameManagerController.Instance.score.ToString();
	}

	void updateScore() {
		GameManagerController.Instance.score++;
	}
}
