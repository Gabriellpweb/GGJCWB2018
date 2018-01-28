using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarWaveEffect : MonoBehaviour {

	private SpriteRenderer sr;

	[SerializeField]
	private Vector3 maxScale;

	[SerializeField]
	private Vector3 initialScale;

	[SerializeField]
	private Vector3 scaleRate;

	[SerializeField]
	private float alphaRate;

	// Use this for initialization
	void Start () 
	{
		sr = GetComponent<SpriteRenderer> ();
		transform.localScale = initialScale;
		StartCoroutine (Animate());
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	IEnumerator Animate()
	{
		float alpha = 1;

		while (transform.position != maxScale || alpha > 0) { 
			
			sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, alpha);
			alpha -= Time.deltaTime * alphaRate;

			float x = transform.localScale.x < maxScale.x ? transform.localScale.x + scaleRate.x : transform.localScale.x;
			float y = transform.localScale.y < maxScale.y ? transform.localScale.y + scaleRate.y : transform.localScale.y;
			float z = transform.localScale.z < maxScale.z ? transform.localScale.z + scaleRate.z : transform.localScale.z;
			transform.localScale = new Vector3 (x, y, z);
			yield return null;
		}

		sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
		Destroy (gameObject, 3);
	}
}
