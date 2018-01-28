using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour, Destructable {

    #region Properties

    [SerializeField]
    private GameObject explosionObject;

	public bool fastRotate = true;

    #endregion

	void FixedUpdate()
	{
		movementAndRotation ();
	}

	private void movementAndRotation() {
		Vector3 tempPosition = transform.position;
		tempPosition.x += 2f * Time.deltaTime;
		transform.position = tempPosition;
		if (fastRotate) {
			transform.RotateAround(transform.position, transform.up, Time.deltaTime * 300f);	
		} else {
			transform.RotateAround(transform.position, transform.up, Time.deltaTime * 10f);
		}

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

		GameManagerController.Instance.score += 100f;

		if (explosionObject != null)
		{
			GameObject fx = (GameObject)Instantiate(explosionObject, transform.position, Quaternion.identity);
			Destroy (fx, 3f);
		}

		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider collider)
	{
		Destructable destructable = collider.transform.parent.GetComponent<Destructable> ();

		if (destructable != null) {
			destructable.Hit ();
		}

		DestructablePlanet destructablePlanet = collider.gameObject.transform.parent.gameObject.GetComponent<DestructablePlanet> ();

		if (destructablePlanet != null) {
			destructablePlanet.Hit (transform.position);
		}

		Destruct ();
	}

	void OnCollisionEnter(Collision collision)
	{
		Destructable destructable = collision.gameObject.transform.parent.gameObject.GetComponent<Destructable> ();

		if (destructable != null) 
		{
			destructable.Destruct ();
		}

		DestructablePlanet destructablePlanet = collision.gameObject.transform.parent.gameObject.GetComponent<DestructablePlanet> ();

		if (destructablePlanet != null) {
			destructablePlanet.Hit (collision.contacts[0].point);
		}

		Destruct ();
	}
}
