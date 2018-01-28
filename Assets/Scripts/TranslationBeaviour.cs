using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationBeaviour : MonoBehaviour {

	[SerializeField]
	private GameObject objectToOrbitTranslation;

	[SerializeField]
	private float distanceFromObject;

	[SerializeField]
	private bool flatTranslation = true;

	[SerializeField]
	private Vector3 angleVariation;

	[SerializeField]
	private Vector3 translationInitialAngle;

	private Vector3 translationAngle = Vector3.zero;

	private Vector3 objectPosition;

	// Use this for initialization
	void Start () 
	{
		objectPosition = objectToOrbitTranslation.transform.position;
		translationAngle = translationInitialAngle;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (flatTranslation) {
			transform.position = TranslateFlat ();
		} else {
			transform.position = TranslateAxis ();	
		}

		translationAngle = new Vector3 (
			translationAngle.x + angleVariation.x,
			translationAngle.y + angleVariation.y,
			translationAngle.z + angleVariation.z
		);
	}

	private Vector3 TranslateFlat()
	{
		float x = distanceFromObject * Mathf.Cos ((translationAngle.x * Mathf.PI) / 180) + objectPosition.x;
		float y = distanceFromObject * Mathf.Sin((translationAngle.y * Mathf.PI) / 180) + objectPosition.y;
		Vector3 newPosition = new Vector3 (x,y, transform.position.z);
		return newPosition;
	}

	private Vector3 TranslateAxis()
	{
		float x = distanceFromObject * Mathf.Cos ((translationAngle.x * Mathf.PI) / 180) + objectPosition.x;
		float y = distanceFromObject * Mathf.Sin((translationAngle.y * Mathf.PI) / 180) + objectPosition.y;
		float z = distanceFromObject * Mathf.Cos((translationAngle.z * Mathf.PI) / 180) + objectPosition.y;
		Vector3 newPosition = new Vector3 (x, y, z);
		return newPosition;
	}
}
