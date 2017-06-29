using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubemap_GetPixels: MonoBehaviour
{

	private Texture2D refTexture;
	private Texture2D warningTexture;
	private Texture2D errorTexture;
	public Shader _shader;
	private Color[] baseTex;
	private List<Cubemap> textures = new List<Cubemap> ();
	public bool getPixel = false;
	public float scale = 1f;
	public int column = 10;
	//public TextureFormat tf;

	private List<CubemapFace> cubemapFaces = new List<CubemapFace> () {
		CubemapFace.NegativeX,
		CubemapFace.NegativeY,
		CubemapFace.NegativeZ,
		CubemapFace.PositiveX,
		CubemapFace.PositiveY,
		CubemapFace.PositiveZ
	};

	public CamFocus focusScript;

	private List<Transform> spheres = new List<Transform> ();

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
		texturesTemp = Resources.LoadAll ("Cubemaps/iOS");
		#endif

		#if UNITY_ANDROID
		texturesTemp = Resources.LoadAll ("Cubemaps/Android");
		#endif

		#if UNITY_STANDALONE
		texturesTemp = Resources.LoadAll ("Cubemaps/Standalone");
		#endif 

		float dist = 1.25f * scale;

		foreach (Object tex in texturesTemp) {
			textures.Add (tex as Cubemap);
			CreateSphere (tex as Cubemap, new Vector3 (dist * Mathf.Repeat (i, column), -dist * Mathf.Floor (i / column), 0f));
			i++;
		}
		focusScript.Focus ();
		yield return new WaitForEndOfFrame ();
	}

	void CreateSphere (Cubemap _texture, Vector3 pos)
	{
		GameObject go = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		GameObject temp = new GameObject (_texture.ToString ());
		spheres.Add (go.transform);
		go.transform.SetParent (temp.transform);
		Transform t = temp.GetComponent <Transform> ();
		t.localScale = Vector3.one * scale;
		MeshRenderer mr = go.GetComponent<MeshRenderer> ();
		Material mat = new Material (_shader);

		Cubemap tex = null;
		tex = new Cubemap (_texture.width, TextureFormat.ARGB32, false);
		Color[][] pixels = new Color[6][];
		if (!getPixel) {
			for (int i = 0; i < 6; i++) {
				pixels [i] = new Color[_texture.width * _texture.width];
				pixels [i] = _texture.GetPixels (cubemapFaces [i]);
				tex.SetPixels (pixels [i], cubemapFaces [i]);
			}
		} else {
			for (int ii = 0; ii < 6; ii++) {
				pixels [ii] = new Color[_texture.width * _texture.width];
				int i = 0;
				for (int y = 0; y < _texture.width; y++) {
					for (int x = 0; x < _texture.width; x++) {
						pixels [ii] [i] = _texture.GetPixel (cubemapFaces [ii], x, y);
						i++;
					}
				}
				tex.SetPixels (pixels [ii], cubemapFaces [ii]);
			}
		}
		//tex.SetPixels (pixels);
		tex.Apply ();
		LabelMaker.MakeLabel (_texture.name.ToString ().Remove (0, 5), Color.white, t);
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

	void Update ()
	{
		foreach (Transform t in spheres) {
			t.Rotate ((Vector3.up * 6f) * Time.deltaTime);
		}
	}

}