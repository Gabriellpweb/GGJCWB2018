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
		GameObject[] sats = GameObject.FindGameObjectsWithTag ("Player");

		Debug.Log (sats.Length);

		if (sats.Length == 1) {
			WaveController.Instance.callApocalipse ();
		} else {
			Debug.Log (sats [0].name);
		}

		if (explosionObject != null)
		{
			Instantiate(explosionObject, transform.position, Quaternion.identity);
		}

		Destroy (gameObject);
	}
}
