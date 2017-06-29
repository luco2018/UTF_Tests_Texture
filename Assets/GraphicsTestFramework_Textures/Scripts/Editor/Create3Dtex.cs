using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Create3Dtex : MonoBehaviour
{
	List<Texture2D> texList = new List<Texture2D> ();
	public MeshRenderer MR;

	void Start ()
	{
		Object[] slices = Resources.LoadAll ("Textures/3D");
		foreach (Object tex in slices) {
			texList.Add (tex as Texture2D);
		}
		//CreateTex (256, new Texture3D (256, 256, 256, TextureFormat.RGB9e5Float, false));

		MR.material.SetTexture ("_texture", Resources.Load ("Textures/3D/3dTex") as Texture3D);

	}

	void CreateTex (int dim, Texture3D tex3D)
	{
		Color[] newC = new Color[dim * dim * dim];
		//float oneOverDim = 1.0f / (1.0f * dim - 1.0f);
		for (int x = 0; x < dim; x++) {
			for (int y = 0; y < dim; y++) {
				for (int z = 0; z < dim; z++) {
					//newC [x + (y * dim) + (z * dim * dim)] = new Color ((x * 1.0f) * oneOverDim, (y * 1.0f) * oneOverDim, (z * 1.0f) * oneOverDim, 1.0f);
					newC [x + (y * dim) + (z * dim * dim)] = texList [y].GetPixel (x, z);
				}
			}
		}
		tex3D.SetPixels (newC);
		tex3D.Apply ();
		tex3D.wrapMode = TextureWrapMode.Repeat;
		AssetDatabase.CreateAsset (tex3D, "Assets/Resources/Textures/3D/3dTex_compressed.asset");
		MR.material.SetTexture ("_texture", tex3D);
	}
}
