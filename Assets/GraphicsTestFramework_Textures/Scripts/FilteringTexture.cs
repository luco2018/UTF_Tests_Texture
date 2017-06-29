using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TexturesSuite
{
	public class FilteringTexture : MonoBehaviour
	{

		public MeshRenderer pointMesh;
		public MeshRenderer biMesh;
		public MeshRenderer triMesh;
		public Texture2D tex;
		public Shader shad;

		void OnEnable ()
		{
			pointMesh.material = new Material (shad);
			pointMesh.material.mainTexture = FilterSet (0);
			biMesh.material = new Material (shad);
			biMesh.material.mainTexture = FilterSet (1);
			triMesh.material = new Material (shad);
			triMesh.material.mainTexture = FilterSet (2);
		}


		Texture2D FilterSet (int mode)
		{
			Texture2D ntex = new Texture2D (tex.width, tex.height);
			ntex.SetPixels (tex.GetPixels ());
			ntex.Apply ();

			switch (mode) {
			case 0:
				ntex.filterMode = FilterMode.Point;
				break;
			case 1:
				ntex.filterMode = FilterMode.Bilinear;
				break;
			case 2:
				ntex.filterMode = FilterMode.Trilinear;
				break;
			default:
				break;
			}
			return ntex;
		}

	}
}
