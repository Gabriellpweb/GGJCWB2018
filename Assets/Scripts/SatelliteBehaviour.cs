using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteBehaviour : MonoBehaviour {

	[SerializeField]
	private Transform referenceObjectToAngle;

	[SerializeField]
	private Transform shotOriginPoint;

	[SerializeField]
	private GameObject basicMissile;

	private bool canFire = true;

	private float angle = 0;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Fire1") && canFire) {
			Fire ();
			//canFire = false;
		}

		angle = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
	}

	void Fire()
	{
		Vector3 direction = new Vector3(
			5f * Mathf.Cos((angle * Mathf.PI) / 180f) + transform.position.x,
			5f * Mathf.Sin((angle * Mathf.PI) / 180f) + transform.position.y,
			0
		);

		GameObject missile = GameObject.Instantiate (basicMissile, shotOriginPoint.transform.position, transform.rotation);
		MissileBase missileBase = missile.GetComponent<MissileBase> ();
		missileBase.WithForce (direction);
		missileBase.Fire ();
	}

}
