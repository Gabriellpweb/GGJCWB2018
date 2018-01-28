using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

	public MeshRenderer mapBackground;
	private Vector3 originalCamPos;

	public static CameraController instance;

    //Setando posição limite da câmera
    private float minPositionX, maxPositionX, minPositionY, maxPositionY;

	void Awake() {
		instance = this;
	}

    // Use this for initialization
    void Start () {
		originalCamPos = GetComponent<Camera>().transform.position;
	}

	void Update() {
		if (Input.GetKey(KeyCode.Space)) {
			CameraController.instance.shakeCamera (1f, 1f);
		}
	}
    
	/// <summary>
	/// Shake camera (Tremor).
	/// </summary>
	/// <param name="duration"></param>
	/// <param name="magnitude"></param>
	public void shakeCamera(float duration, float magnitude) {
		Dictionary<string, float> shakeParams = new Dictionary<string, float>();
		shakeParams["duration"] = duration;
		shakeParams["magnitude"] = magnitude;

        StopCoroutine("Shake");
		StartCoroutine("Shake", shakeParams);
    }

    /**
     * Dictonary
     * float duration
     * float magnitude
     */
    IEnumerator Shake(Dictionary<string, float> parameters) {
        yield return new WaitForSeconds(0.2f);

        float elapsed = 0.0f;

        while (elapsed < parameters["duration"])
        {
            elapsed += Time.deltaTime;

            float percentComplete = elapsed / parameters["duration"];
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= parameters["magnitude"] * damper;
			y *= parameters["magnitude"] * damper;

			Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

			yield return null;
        }

		Camera.main.transform.position = originalCamPos;
    }
}
