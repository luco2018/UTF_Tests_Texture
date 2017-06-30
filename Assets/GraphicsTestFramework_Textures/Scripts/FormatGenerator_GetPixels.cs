using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormatGenerator_GetPixels: MonoBehaviour
{

	private Texture2D refTexture;
	private Texture2D warningTexture;
	private Texture2D errorTexture;
	public Shader _shader;
	private Color[] baseTex;
	private List<Texture2D> textures = new List<Texture2D> ();
	public bool getPixel = false;
    public int widthheight = 256;
	//public TextureFormat tf;

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
		Object[] texturesTemp;

		#if UNITY_IPHONE
		texturesTemp = Resources.LoadAll ("Textures/iOS");
		#endif

		#if UNITY_ANDROID
		texturesTemp = Resources.LoadAll ("Textures/Android");
		#endif

		#if UNITY_STANDALONE
		texturesTemp = Resources.LoadAll ("Textures/Standalone");
		#endif 

		foreach (Object tex in texturesTemp) {
			textures.Add (tex as Texture2D);
			yield return new WaitForEndOfFrame ();
			CreateTextureQuad (tex as Texture2D, new Vector3 (1.25f * Mathf.Repeat (i, 10f), -1.25f * Mathf.Floor (i / 10f), 0f));
			i++;
		}
	}

	void CreateTextureQuad (Texture2D _texture, Vector3 pos)
	{
		GameObject go = GameObject.CreatePrimitive (PrimitiveType.Quad);
		Transform t = go.GetComponent <Transform> ();
		MeshRenderer mr = go.GetComponent<MeshRenderer> ();
		Material mat = new Material (_shader);

		Texture2D tex = null;
		tex = new Texture2D (widthheight, widthheight, TextureFormat.ARGB32, false);
		Color[] pixels = new Color[widthheight * widthheight];
		if (!getPixel) {
			pixels = _texture.GetPixels ();
		} else {
			int i = 0;
			for (int y = 0; y < widthheight; y++) {
				for (int x = 0; x < widthheight; x++) {
					pixels [i] = _texture.GetPixel (x, y);
					i++;
				}
			}
		}
		tex.SetPixels (pixels);
		tex.Apply ();
		LabelMaker.MakeLabel (_texture.name.ToString ().Remove (0, 12), Color.white, t);
		tex.filterMode = FilterMode.Point;
		mat.mainTexture = tex;
		mat.name = _texture.name;
		go.name = _texture.name;
		mr.material = mat;
		tex.wrapMode = TextureWrapMode.Clamp;
		tex.Apply ();
		t.SetParent (transform);
		t.position = pos;
	}
}
