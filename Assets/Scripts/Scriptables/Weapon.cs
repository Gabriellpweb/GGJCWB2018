using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Game Design/New Weapon", order = 1)]
public class Weapon : ScriptableObject {
	public GameObject bulletPrefab;
	public float timeToFire = 0;
	public float fireRate = 0;
	public bool infinite = false;
	public int ammor = 0;
}
