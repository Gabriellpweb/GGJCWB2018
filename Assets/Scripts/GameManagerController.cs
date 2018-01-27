using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Game manager controller.
/// </summary>
public class GameManagerController : MonoBehaviour {

	public int currentLifes;
	public int amountCrystals;

	public static class Scenes {
		public const string STAGE1 = "Stage1";
        public const string STAGE2 = "Stage2";
        public const string MENU = "Menu";
		public const string GAME_OVER = "GameOver";
	}

    private static GameManagerController instance;

    public static GameManagerController Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType<GameManagerController> ();
				if (instance == null) {
					GameObject obj = new GameObject ();
					obj.hideFlags = HideFlags.HideAndDontSave;
					instance = obj.AddComponent<GameManagerController> ();
				}
			}
			return instance;
		}
	}

	void Awake() {
		//Nao deixa iniciar a funçao sleep nos aplicativos
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		//continua executando mesmo em background
		Application.runInBackground = true;
	}

	void Start () {
		DontDestroyOnLoad (gameObject);
    }

    /*
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			//enable the script referent to the level
			switch(SceneManager.GetActiveScene().name){
				case Scenes.STAGE1: {
					quitGame ();
					break;
				}
			}
		}

	}
    */

	public void loadLevel(string levelName) {
		PlayerPrefs.SetString ("sceneToLoad", levelName);
		SceneManager.LoadScene("Loading");
	}

	public void quitGame() {
		Application.Quit();
	}

	void OnHideUnity(bool isGameShown) {
		if (!isGameShown)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}

	public void removeOneLife() {
		currentLifes--;

		StartCoroutine ("gameOverOrPlayAgain");
	}

	private IEnumerator gameOverOrPlayAgain() {
		yield return new WaitForSeconds (2f);

		if (currentLifes > 0) {
			loadLevel (SceneManager.GetActiveScene().name);
		} else {
			SceneManager.LoadScene (GameManagerController.Scenes.GAME_OVER);
		}
	}
}
