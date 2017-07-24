using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FormatGeneratorConvert : MonoBehaviour
{

	private Texture2D refTexture;
	private Texture2D warningTexture;
	private Texture2D errorTexture;
	public Shader _shader;
    public int widthheight = 256;

    private List<TextureFormat> setPixelSupport = new List<TextureFormat> ();

	void Start ()
	{
		refTexture = Resources.Load ("Textures/testTexture") as Texture2D;
		warningTexture = Resources.Load ("Textures/Warning") as Texture2D;
		errorTexture = Resources.Load ("Textures/Error") as Texture2D;

		StartCoroutine ("QuadArray");
	}

	IEnumerator QuadArray ()
	{
		int i = 0;
		foreach (RenderTextureFormat tf in System.Enum.GetValues (typeof(RenderTextureFormat))) {
			yield return new WaitForEndOfFrame ();
			CreateTextureQuad (tf, new Vector3 (1.25f * Mathf.Repeat (i, 10f), -1.25f * Mathf.Floor (i / 10f), 0f));
			i++;
		}
	}

	void CreateTextureQuad (RenderTextureFormat format, Vector3 pos)
	{
		if ((int)format >= 0) {
			GameObject go = GameObject.CreatePrimitive (PrimitiveType.Quad);
			Transform t = go.GetComponent <Transform> ();
			MeshRenderer mr = go.GetComponent<MeshRenderer> ();
			Material mat = new Material (_shader);

			TextureFormat tf = TextureFormat.Alpha8;
			bool isFormat = false;
			string s = format.ToString ();
			try {
				tf = (TextureFormat)Enum.Parse (typeof(TextureFormat), s);
				isFormat = true;
			} catch (ArgumentException e) {
				isFormat = false;
			}
			Texture2D tex = null;
			if (SystemInfo.SupportsRenderTextureFormat (format) && isFormat) {
				Debug.Log ("Texture supported " + format.ToString ());
				tex = new Texture2D (widthheight, widthheight, tf, false);
				Graphics.ConvertTexture (refTexture, tex);
				LabelMaker.MakeLabel (format.ToString (), Color.green, t);
			} else {
				Debug.LogWarning ("Texture not supported on platform of ConvertTexture " + format.ToString ());
				tex = errorTexture;
				LabelMaker.MakeLabel (format.ToString (), Color.red, t);
			}
			tex.filterMode = FilterMode.Point;
			mat.mainTexture = tex;
			mat.name = format.ToString ();
			mr.material = mat;
			tex.wrapMode = TextureWrapMode.Clamp;
			go.name = format.ToString ();
			t.SetParent (transform);
			t.position = pos;
		}

	}
}
