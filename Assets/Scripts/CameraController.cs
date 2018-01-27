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

            float mapX = mapBackground.bounds.size.x;
            float mapY = mapBackground.bounds.size.y;

			float vertExtent = GetComponent<Camera>().orthographicSize;
            float horzExtent = vertExtent * Screen.width / Screen.height;

            // Calculations assume map is position at the origin
            minPositionX = horzExtent - mapX / 2.0f;
            maxPositionX = mapX / 2.0f - horzExtent;
            minPositionY = vertExtent - mapY / 2.0f;
            maxPositionY = mapY / 2.0f - vertExtent;

			GetComponent<Camera>().transform.position = new Vector3(
				Mathf.Clamp(GetComponent<Camera>().transform.position.x + x, minPositionX, maxPositionX),
				Mathf.Clamp(GetComponent<Camera>().transform.position.y + y, minPositionY, maxPositionY),
				originalCamPos.z
            );

            yield return null;
        }
    }

}
