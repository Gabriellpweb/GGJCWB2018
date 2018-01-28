﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Destructable {

    #region Properties

    [SerializeField]
    private GameObject explosionObject;

    #endregion

	void FixedUpdate()
	{
		movementAndRotation ();
	}

	private void movementAndRotation() {
		Vector3 tempPosition = transform.position;
		tempPosition.x += 2f * Time.deltaTime;
		transform.position = tempPosition;
		transform.RotateAround(transform.position, transform.up, Time.deltaTime * 300f);
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
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
		if (explosionObject != null)
		{
			Instantiate(explosionObject, transform.position, Quaternion.identity);
		}

		Destroy (gameObject);
	}
}