using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TexturesSuite
{
	public class MipTexture : MonoBehaviour
	{

		public MeshRenderer zeroMesh;
		public MeshRenderer twoMesh;
		public MeshRenderer fiveMesh;
		public Texture2D tex;
		public Shader shad;

		void OnEnable ()
		{
			zeroMesh.material = new Material (shad);
			zeroMesh.material.mainTexture = MipSet (0, zeroMesh.material);
			twoMesh.material = new Material (shad);
			twoMesh.material.mainTexture = MipSet (2, twoMesh.material);
			fiveMesh.material = new Material (shad);
			fiveMesh.material.mainTexture = MipSet (5, fiveMesh.material);
		}


		Texture2D MipSet (int level, Material mat)
		{
			Texture2D ntex = new Texture2D (tex.width, tex.height, TextureFormat.RGB24, true);
			ntex.SetPixels (tex.GetPixels ());
			ntex.Apply ();
			mat.SetFloat ("_mipLevel", level);
			return ntex;
		}

	}
}
