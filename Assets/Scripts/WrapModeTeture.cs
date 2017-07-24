using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TexturesSuite
{
	public class WrapModeTeture : MonoBehaviour
	{

		public MeshRenderer repeatMesh;
		public MeshRenderer clampMesh;
		public MeshRenderer mirrorMesh;
		public MeshRenderer mirrorOnceMesh;
		public MeshRenderer repeatVerticalMesh;
		public Texture2D tex;
		public Shader shad;

		void OnEnable ()
		{
			if (repeatMesh != null) {
				repeatMesh.material = new Material (shad);
				repeatMesh.material.mainTexture = WrapSet (TextureWrapMode.Repeat, repeatMesh.material);
			}
			if (clampMesh != null) {
				clampMesh.material = new Material (shad);
				clampMesh.material.mainTexture = WrapSet (TextureWrapMode.Clamp, clampMesh.material);
			}

			#if UNITY_2017_1_OR_NEWER
			if (mirrorMesh != null) {
				mirrorMesh.material = new Material (shad);
				mirrorMesh.material.mainTexture = WrapSet (TextureWrapMode.Mirror, mirrorMesh.material);
			}
			if (mirrorOnceMesh != null) {
				mirrorOnceMesh.material = new Material (shad);
				mirrorOnceMesh.material.mainTexture = WrapSet (TextureWrapMode.MirrorOnce, mirrorOnceMesh.material);
			}
			if (repeatVerticalMesh != null) {
				repeatVerticalMesh.material = new Material (shad);
				repeatVerticalMesh.material.mainTexture = WrapSet (TextureWrapMode.Clamp, TextureWrapMode.Repeat, repeatVerticalMesh.material);
			}
			#endif
		}



		Texture2D WrapSet (TextureWrapMode wrap, Material mat)
		{
			Texture2D ntex = new Texture2D (tex.width, tex.height);
			ntex.SetPixels (tex.GetPixels ());
			ntex.Apply ();
			ntex.filterMode = FilterMode.Point;
			ntex.wrapMode = wrap;
			mat.mainTextureOffset = new Vector2 (-0.5f, -0.5f);
			mat.mainTextureScale = Vector2.one * 2f;
			return ntex;
		}

		Texture2D WrapSet (TextureWrapMode u, TextureWrapMode v, Material mat)
		{
			Texture2D ntex = new Texture2D (tex.width, tex.height);
			ntex.SetPixels (tex.GetPixels ());
			ntex.Apply ();
			ntex.filterMode = FilterMode.Point;
			#if UNITY_2017_1_OR_NEWER
			ntex.wrapModeU = u;
			ntex.wrapModeV = v;
			#else
			ntex.wrapMode = u;
			#endif
			mat.mainTextureOffset = new Vector2 (-0.5f, -0.5f);
			mat.mainTextureScale = Vector2.one * 2f;
			return ntex;
		}


	}
}
