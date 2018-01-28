using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EarthBehaviour : MonoBehaviour, DestructablePlanet {

	private ForceFieldBehaviour forceField;

	// Use this for initialization
	void Start () 
	{
		forceField = GetComponent<ForceFieldBehaviour> ();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Hit(Vector3 contactPoint)
	{
		if (forceField.HasCharges ()) {
			forceField.Hit (contactPoint);
		} else {
			Destruct ();
		}
	}

	public void Hit(float damage)
	{
	}

	public void Destruct()
	{
		SceneManager.LoadScene ("GameOver");
	}
}
