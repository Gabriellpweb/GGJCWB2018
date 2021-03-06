﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldBehaviour : MonoBehaviour {

	[SerializeField]
	public int forceFieldHits;

	[SerializeField]
	private Transform forceFieldColliderObject;

	[SerializeField]
	private ForceFieldEffect forceFieldHudEffect;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public bool HasCharges()
	{
		return forceFieldHits > 0;
	}

	public void DisableForceField()
	{
		forceFieldColliderObject.gameObject.SetActive (false);
	}

	public void EnableForceField()
	{
		forceFieldColliderObject.gameObject.SetActive (true);
	}


	public void Hit(Vector3 contactPoint)
	{
		float angle = Mathf.Atan2(contactPoint.y, contactPoint.x) * Mathf.Rad2Deg;

		forceFieldHudEffect.AnimateHit (angle);

		forceFieldHits--;
		StageController.Instance.txtForceFieldAmount.text = "x" + forceFieldHits.ToString ();

		if (forceFieldHits <= 0) {
			DisableForceField ();
		}
	}

	public void AddCharges(int charges)
	{
		forceFieldHits += charges;

		if (forceFieldColliderObject.gameObject.activeSelf) {
			EnableForceField ();
		}
	}
}
