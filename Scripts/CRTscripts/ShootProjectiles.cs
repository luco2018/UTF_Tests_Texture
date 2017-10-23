using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectiles : MonoBehaviour
{
    # if UNITY_2017_1_OR_NEWER

    public Transform hitObject;
    public GameObject projectile;
    public GameObject hitEffect;
    public float size = 3f;
    public float speed = 20f;
    private Camera mainCamera;
    private CustomRenderTexture ct;
    private List<CustomRenderTextureUpdateZone> zones = new List<CustomRenderTextureUpdateZone>();
    private int original = 10;
    private int counter = 0;
    private Material mat;

    // Update is called once per frame

    void Start()
    {
        ct = hitObject.transform.GetComponent<Renderer>().sharedMaterial.mainTexture as CustomRenderTexture;
        mainCamera = Camera.main;




    }

    void OnDisable()
    {
        //reset zones
        if (ct)
        {
            ct.SetUpdateZones(new CustomRenderTextureUpdateZone[1]);
            ct.Initialize();
        }
    }

    private void HitEffect(Vector3 point, Vector2 uv)
    {
        //if (!ct)
        //{
         //   ct = hitObject.transform.GetComponent<Renderer>().sharedMaterial.mainTexture as CustomRenderTexture;
       // }

        Instantiate(hitEffect, point, Quaternion.identity);

        Vector2 uvPos = uv;
        ct.material.SetVector("_BulletUV", new Vector3(uvPos.x, uvPos.y, 0));
        ct.material.SetFloat("_Size", size * 2);

        //set update zone
        CustomRenderTextureUpdateZone[] newZone = new CustomRenderTextureUpdateZone[1];
        newZone[0].updateZoneCenter = new Vector3(uvPos.x, 1 - uvPos.y, 0);
        newZone[0].updateZoneSize = new Vector3(1 / (size * 2), 1 / (size * 2), 1 / (size * 2));
        ct.SetUpdateZones(newZone);
        ct.Update();
    }

    void Update()
    {
         if (Input.GetMouseButton(0))
        {

            RaycastHit hit;
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
            {
               // if (!ct)
                //{
               //     ct = hit.transform.GetComponent<Renderer>().sharedMaterial.mainTexture as CustomRenderTexture;
               // }
                Debug.Log("hit " + hit.point + " uv " + hit.textureCoord);
                HitEffect(hit.point,hit.textureCoord);
            }

        }

        Vector3 po;
        Vector2 uv;

        if (original == counter)
        {
            po = new Vector3(2.5f, -0.2f, -6.8f);
            uv = new Vector2(0.4f, 0.7f);
            HitEffect(po, uv);
        }
        else if(original+1 == counter)
        {
            po = new Vector3(2.4f, -1.1f, -6.8f);
            uv = new Vector2(0.3f, 0.3f);
            HitEffect(po, uv);
        }
        else if (original + 2 == counter)
        {
            po = new Vector3(2.2f, -0.6f, -6.6f);
            uv = new Vector2(0.2f, 0.5f);
            HitEffect(po, uv);
        }
        else if (original + 3 == counter)
        {
            po = new Vector3(2.5f, -0.1f, -6.8f);
            uv = new Vector2(0.4f, 0.8f);
            HitEffect(po, uv);
        }
        else if (original + 4 == counter)
        {
            po = new Vector3(2.5f, -0.7f, -6.8f);
            uv = new Vector2(0.4f, 0.5f);
            HitEffect(po, uv);
        }
        counter++;

    }
    #endif
}
