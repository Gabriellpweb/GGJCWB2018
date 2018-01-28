using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldBehaviour : MonoBehaviour {

	[SerializeField]
	private int forceFieldHits;

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

	private void DisableForceField()
	{
		forceFieldColliderObject.gameObject.SetActive (false);
	}

	public void Hit(Vector3 contactPoint)
	{
		float angle = Mathf.Atan2(contactPoint.y, contactPoint.x) * Mathf.Rad2Deg;

		forceFieldHudEffect.AnimateHit (angle);

		forceFieldHits--;

		if (forceFieldHits <= 0) {
			DisableForceField ();
		}
	}
}
