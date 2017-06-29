using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

	public Vector3 rotVector;
	public bool random = false;
	public float randomSpeed;
	public Space _space = Space.Self;

	void Start ()
	{

		if (random) {
			rotVector = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), Random.Range (-1, 1));
			rotVector *= randomSpeed;
		}
	}

	void Update ()
	{
		transform.Rotate (rotVector * Time.deltaTime, _space);
	}
}
