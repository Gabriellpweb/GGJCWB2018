using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyboxBehaviour : MonoBehaviour {

	[SerializeField]
	private Material skyBoxMaterial;

	[SerializeField]
	private int rotateInterval;

	private float rotateTimer;

	private float rotation = 0;

	// Use this for initialization
	void Start () 
	{
		RenderSettings.skybox = skyBoxMaterial;
		rotateTimer = rotateInterval;
	}
	
	// Update is called once per frame
	void Update () 
	{
		skyBoxMaterial.SetFloat ("_Rotation", rotation);
		rotateTimer--;

		if (rotateTimer <= 0) {
			rotation += Time.deltaTime;
			rotateTimer = rotateInterval;
		}
	}
}
