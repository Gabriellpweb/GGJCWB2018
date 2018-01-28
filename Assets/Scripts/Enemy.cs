using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    #region Properties

    [SerializeField]
    private GameObject explosionObject;

    #endregion


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKey(KeyCode.Space)) {
			Debug.Log ("apertado");
			GameObject explosion = (GameObject)Instantiate (
				explosionObject, 
				new Vector3(transform.position.x, transform.position.y, transform.position.z),
				Quaternion.identity
			);

			Destroy (explosion, 3f);
			Destroy (gameObject);
		}

	}

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
        {
            die();
        }
    }
    
    public void die()
    {

        if (explosionObject != null)
        {
            Instantiate(explosionObject, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
