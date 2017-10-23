using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TexturesSuite
{
	public class AnisotrpicTextures : MonoBehaviour
	{
		MeshRenderer mr;
		public Texture2D tex;
		public Shader shad;

		void OnEnable ()
		{
			mr = GetComponent<MeshRenderer> ();

			mr.materials = new Material[5];

			Material[] mats = mr.materials;

			int aniso = 0;
			foreach (Material mat in mats) {
				mat.shader = shad;
				Texture2D ntex = new Texture2D (tex.width, tex.height);
				ntex.SetPixels (tex.GetPixels ());
				ntex.Apply ();
				ntex.anisoLevel = aniso;
				mat.mainTexture = ntex;
				aniso++;
			}
		}

	}
}
