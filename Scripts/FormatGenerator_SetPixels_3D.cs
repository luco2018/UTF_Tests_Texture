﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormatGenerator_SetPixels_3D: MonoBehaviour
{
	public Texture3D refTexture;
	//private Texture2D warningTexture;
	//private Texture2D errorTexture;
	public Shader _shader;
	private Color[] baseTex;
	//public bool setPixel = false;
	public bool debugMode = false;
    public int widthheight = 256;

    public List<TextureFormat> setPixelSupport = new List<TextureFormat> () {TextureFormat.Alpha8,
		TextureFormat.RGB24,
		TextureFormat.ARGB32,
		TextureFormat.RGBA32,
		TextureFormat.RHalf,
		TextureFormat.RGHalf,
		TextureFormat.RGBAHalf,
		TextureFormat.RFloat,
		TextureFormat.RGFloat,
		TextureFormat.RGBAFloat,
		TextureFormat.RGB9e5Float,
		TextureFormat.RGB565
	};

	void Start ()
	{
		//refTexture = Resources.Load ("Textures/testTexture") as Texture3D;
		//warningTexture = Resources.Load ("Textures/Warning") as Texture2D;
		//errorTexture = Resources.Load ("Textures/Error") as Texture2D;
		baseTex = refTexture.GetPixels ();

		string[] texFormats = System.Enum.GetNames (typeof(TextureFormat));

		StartCoroutine ("QuadArray");
	}

	IEnumerator QuadArray ()
	{
		int i = 0;
		foreach (TextureFormat tf in System.Enum.GetValues (typeof(TextureFormat))) 
		{
			if (debugMode)
				Debug.Log ((int)tf + " = " + tf.ToString () + " || " + tf);
			yield return new WaitForEndOfFrame ();
			CreateTextureQuad (tf, new Vector3 (1.25f * Mathf.Repeat (i, 10f), -1.25f * Mathf.Floor (i / 10f), 0f));
			i++;
		}
        //GraphicsTestFramework.TestTypeManager.Instance.GetActiveTestLogic().WaitCallback();
    }

	void CreateTextureQuad (TextureFormat format, Vector3 pos)
	{
		if ((int)format >= 0) {
			GameObject go = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			Transform t = go.GetComponent <Transform> ();
			MeshRenderer mr = go.GetComponent<MeshRenderer> ();
			Material mat = new Material (_shader);

			Texture3D tex = null;
			if (SystemInfo.SupportsTextureFormat (format)) 
			{
				if (setPixelSupport.Contains (format)) 
				{
					if (debugMode)
						Debug.Log ("Texture supported " + format.ToString ());
					tex = new Texture3D (widthheight, widthheight, widthheight, format, false);
					tex.SetPixels (refTexture.GetPixels());

					LabelMaker.MakeLabel (format.ToString (), Color.green, t);
				} 
				else 
				{
					if (debugMode)
						Debug.LogWarning ("Texture not supported with SetPixels " + format.ToString ());
					LabelMaker.MakeLabel (format.ToString (), Color.yellow, t);
					//tex = warningTexture;
				}
			} 
			else 
			{
				if (debugMode)
					Debug.LogWarning ("Texture not supported on platform " + format.ToString ());
				LabelMaker.MakeLabel (format.ToString (), Color.red, t);
				//tex = errorTexture;
			}

			if(tex != null)
			{
			tex.filterMode = FilterMode.Point;
			mat.SetTexture("_texture",tex);
			mat.name = format.ToString ();
			go.name = format.ToString ();
			mr.material = mat;
			tex.wrapMode = TextureWrapMode.Repeat;
			tex.Apply ();
			}

			t.SetParent (transform);
			t.position = pos;
		}

	}

}
