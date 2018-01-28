using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBehaviour : MonoBehaviour, Destructable {

	[SerializeField]
	private GameObject explosionObject;


	void OnCollisionEnter(Collision collision)
	{
		Destructable destructable = collision.gameObject.transform.root.gameObject.GetComponent<Destructable> ();

		if (destructable != null) 
		{
			destructable.Destruct ();
			Destroy (gameObject);
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
		if (explosionObject != null)
		{
			GameObject fx = Instantiate(explosionObject, transform.position, Quaternion.identity);
			Destroy (fx, 7);
		}

		Destroy (gameObject);
	}
}
