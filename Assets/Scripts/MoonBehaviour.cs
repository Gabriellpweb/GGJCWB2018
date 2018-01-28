using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBehaviour : MonoBehaviour {

	[SerializeField]
	private GameObject explosionEffect;


	void OnCollisionEnter(Collision collision)
	{
		Destructable destructable = collision.gameObject.transform.parent.gameObject.GetComponent<Destructable> ();

		if (destructable != null) 
		{
			destructable.Destruct ();
		}

		Destroy (gameObject);
	}
}
