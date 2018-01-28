using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceShieldUpBox : MonoBehaviour, Destructable, Collectable {

	[SerializeField]
	private int extraCharges;

	[SerializeField]
	private GameObject explosionEffect;

	// Use this for initialization
	void Start () 
	{
		
	}

	public void Hit()
	{
		Destruct ();
	}

	public void Hit(float damage)
	{
	}

	public void Destruct()
	{

		if (explosionEffect != null) {
			GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
			Destroy (effect, 3f);
		}

		Destroy (gameObject);
	}

	public void Collect()
	{
		ForceFieldBehaviour fieldBehaviour =  GameObject.FindObjectOfType<ForceFieldBehaviour>();

		if (fieldBehaviour != null) {
			fieldBehaviour.AddCharges (extraCharges);
		}
			
		Destruct ();
	}
}
