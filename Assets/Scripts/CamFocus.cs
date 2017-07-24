using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFocus : MonoBehaviour
{

	public Transform focusGroup;

	public void Focus ()
	{
		Vector3 vect = Vector3.zero;
		int i = 0;
		foreach (Transform t in focusGroup) {
			vect += t.position;
			i++;
		}

		vect = vect / i;
		vect.z = -2f;
		transform.position = vect;
	}
}
