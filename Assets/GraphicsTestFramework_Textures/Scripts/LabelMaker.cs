using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelMaker : MonoBehaviour
{

	public static void MakeLabel (string name, Color col, Transform parent)
	{
		GameObject go = new GameObject (name + "_label");
		go.transform.SetParent (parent, false);
		go.transform.position = -Vector3.up * 0.55f;
		go.AddComponent<MeshRenderer> ();
		TextMesh tm = go.AddComponent<TextMesh> ();
		tm.text = name;
		tm.color = col;
		tm.anchor = TextAnchor.UpperCenter;
		tm.characterSize = 0.05f;
	}
}
