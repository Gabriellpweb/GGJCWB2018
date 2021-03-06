﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteBehaviour : MonoBehaviour, Destructable {

	[SerializeField]
	private Transform referenceObjectToAngle;

	[SerializeField]
	private Transform shotOriginPoint;

	[SerializeField]
	private GameObject basicMissile;

	[SerializeField]
	private GameObject explosionObject;

	[SerializeField]
	private GameObject[] ignoredObjects;

	[SerializeField]
	private GameObject sonarWaveEffect;

	[SerializeField]
	private Weapon basicWeapon;

	private Weapon equipedWeapon;

	private bool canFire = true;

	private float angle = 0;

	private float timeToFire = 50f;

	private float fireRateTime = 0;

	private float fireRate = 4.5f;


	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Fire1") && canFire) {
			if (sonarWaveEffect != null) {
				GameObject fx = Instantiate (sonarWaveEffect, transform.root.position, Quaternion.identity);
				Destroy (fx, 3);
			}
			StartCoroutine(Fire ());
			canFire = false;
		}

		angle = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));

		if (!canFire && fireRateTime < timeToFire) {
			fireRateTime += fireRate;
		}

		if (fireRateTime >= timeToFire) {
			canFire = true;
			fireRateTime = 0;
		}
	}

	IEnumerator Fire()
	{
		Vector3 direction = new Vector3(
			5f * Mathf.Cos((angle * Mathf.PI) / 180f) + transform.position.x,
			5f * Mathf.Sin((angle * Mathf.PI) / 180f) + transform.position.y,
			0
		);
		for (int i = 0; i < 3; i++) {
			yield return new WaitForSeconds (0.05f);
			GameObject missile = GameObject.Instantiate (basicMissile, shotOriginPoint.transform.position, transform.rotation);
			Physics.IgnoreCollision (GetComponentInChildren<Collider>(), missile.GetComponentInChildren<Collider>());
			foreach (GameObject igo in ignoredObjects) {
				if (igo != null) {
					Physics.IgnoreCollision (igo.GetComponentInChildren<Collider>(), missile.GetComponentInChildren<Collider>());
				}
			}

			Destroy (missile, 7);
			MissileBase missileBase = missile.GetComponent<MissileBase> ();
			missileBase.WithForce (direction);
			missileBase.Fire ();
		}
	}

	public void Hit()
	{
		Destruct ();
	}

	public void Hit(float damage)
	{
		//TODO: Must implements
	}

	public void Destruct()
	{
		GameObject[] sats = GameObject.FindGameObjectsWithTag ("Player");
		//GameObject moon = GameObject.Find("moon");

		Debug.Log (sats.Length);

		if (sats.Length == 2) {
			WaveController.Instance.callApocalipse ();
		}

		if (explosionObject != null)
		{
			GameObject fx = Instantiate(explosionObject, transform.position, Quaternion.identity);
			Destroy (fx, 7);
		}

		Destroy (gameObject);
	}
}
