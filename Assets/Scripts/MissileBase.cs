using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBase : MonoBehaviour {

	[SerializeField]
	private float maxForce;

	[SerializeField]
	private float forceAccel;

	private float force = 0;

	private Vector3 forceDirection;

	private Rigidbody rb;

	private bool enabled = false;

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}

	void Update () 
	{
		UpdateDirection ();
		UpdateMovement ();
	}

	void UpdateMovement()
	{
		if (!enabled)
			return;

		if (force <= maxForce)
			force += forceAccel * Time.deltaTime;

		rb.AddForce (forceDirection * force, ForceMode.Impulse);
	}

	void UpdateDirection()
	{
		// Direçao de impulso do corpo rigido
		Vector2 dir = rb.velocity;

		// Calcula o angulo de rotaçao convertendo de radianus pra graus
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

		// Gira o eixo Z para o angulo
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	public void WithForce(Vector3 force)
	{
		this.forceDirection = force;
	}

	public void Fire()
	{
		enabled = true;
	}


	void OnTriggerEnter(Collider collider)
	{
		if (collider.transform.parent.tag == "Player") {
			return;
		}

		Collectable collectable = collider.transform.parent.GetComponent<Collectable> ();

		if (collectable != null) {
			
			collectable.Collect ();

		} else {

			Destructable destructable = collider.transform.parent.GetComponent<Destructable> ();

			if (destructable != null) {
				destructable.Destruct ();
			}
		}

		Destroy (gameObject);
	}
	/*
	void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.parent.tag == "Player") {
			return;
		}

		Destructable destructable = collision.gameObject.transform.root.gameObject.GetComponent<Destructable> ();

		if (destructable != null) 
		{
			destructable.Destruct ();
		}

		Destroy (gameObject);
	}
	*/
}
