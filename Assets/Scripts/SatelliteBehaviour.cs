using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteBehaviour : MonoBehaviour {

	[SerializeField]
	private Transform referenceObjectToAngle;

	private float angle = 0;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//angle = Vector3.Angle (
		//	              referenceObjectToAngle.position, 
		//	              transform.position);
		angle = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
	}
}
