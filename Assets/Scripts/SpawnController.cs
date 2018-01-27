using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

	public GameObject target;

	[HideInInspector]
	public bool sending = false;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("callMeteor", 0, 3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void callMeteor() {

		if (!sending) {
			return;
		}

		GameObject meteor = (GameObject)Instantiate(
			Resources.Load<GameObject>("Prefabs/Meteor"),
			new Vector3(transform.position.x, transform.position.y, transform.position.z),
			Quaternion.identity
		);

		meteor.GetComponent<EnemyFollow> ().setTarget (target);
	}
}
