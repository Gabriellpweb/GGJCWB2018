using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationBeaviour : MonoBehaviour {

	[SerializeField]
	private GameObject objectToOrbitTranslation;

	[SerializeField]
	private float distanceFromObject;

	private Vector3 objectPosition;

	private int translationAngle = 0;

	// Use this for initialization
	void Start () 
	{
		objectPosition = objectToOrbitTranslation.transform.position;

	}
	
	// Update is called once per frame
	void Update () 
	{
		float x = distanceFromObject * Mathf.Cos ((translationAngle * Mathf.PI) / 180) + objectPosition.x;
		float y = distanceFromObject * Mathf.Sin((translationAngle * Mathf.PI) / 180) + objectPosition.y;
		Vector3 newPosition = new Vector3 (x,y, transform.position.z);

		transform.position = newPosition;

		translationAngle++;
	}
}
