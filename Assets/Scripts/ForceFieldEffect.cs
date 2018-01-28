using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldEffect : MonoBehaviour {

	[SerializeField]
	private Transform effectSpriteObject;

	private SpriteRenderer sr;

	private int alphaMarker;

	private IEnumerator fader;

	// Use this for initialization
	void Start () 
	{
		sr = GetComponentInChildren<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void AnimateHit(float angle)
	{
		effectSpriteObject.rotation = Quaternion.Euler (new Vector3(0, 0, angle));

		StartCoroutine (Fade());
	}

	IEnumerator Fade() {
		
		float alpha = 1;

		while (alpha > 0) {
			sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
			alpha -= Time.deltaTime * 2;
			Debug.Log (alpha);
			yield return null;
		}	
		sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
	}

}
