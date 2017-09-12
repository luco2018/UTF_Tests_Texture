using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CustomTextureHandler : MonoBehaviour {

    public CustomRenderTexture customRenderTexture;
    public CustomRenderTextureUpdateZone[] zones;
    public int zonesX = 1;
    public int zonesY = 1;
    public bool doTheThing;

    public float updateDuration = 0.5f;
    public float currentUpdateDuration = 0f;
    private Coroutine updateCoroutine;
    private List<CustomRenderTextureUpdateZone> activeZones = new List<CustomRenderTextureUpdateZone>();

    // Use this for initialization
    void Start () {
        UpdateZones();
        if(customRenderTexture)
            customRenderTexture.Initialize();
    }
	
	// Update is called once per frame
	void Update () {
        if (doTheThing)
        {
            doTheThing = false;
            UpdateZones();
        }
	}

    void UpdateZones()
    {
        customRenderTexture = GetComponent<Renderer>().sharedMaterial.mainTexture as CustomRenderTexture;
        int totalCount = zonesX * zonesY;
        zones = new CustomRenderTextureUpdateZone[totalCount];
        float size = 1 / Mathf.Sqrt(totalCount);

        for (int i = 0; i < totalCount; i++)
        {
            float xPos = i % zonesX;
            float yPos = i / zonesY;

            CustomRenderTextureUpdateZone newZone = new CustomRenderTextureUpdateZone();
            newZone.updateZoneCenter = new Vector3(xPos * size + size/2, yPos * size + size / 2, 0);
            newZone.updateZoneSize = new Vector3(size, size, size);
            zones[i] = newZone;
        }
        CustomRenderTextureUpdateZone[] clearZone = new CustomRenderTextureUpdateZone[1];
        clearZone[0].updateZoneCenter = new Vector2(0.5f, 0.5f);
        clearZone[0].updateZoneSize = Vector2.zero;
        customRenderTexture.SetUpdateZones(clearZone);
    }

    public void Hit(Vector3 hitPoint)
    {

        activeZones.Add(zones[GetClosestZone(hitPoint)]);

        CustomRenderTextureUpdateZone[] tempZoneArray = activeZones.ToArray();
        customRenderTexture.SetUpdateZones(tempZoneArray);

        if (updateCoroutine == null)
            updateCoroutine = StartCoroutine(ToggleUpdate());

        customRenderTexture.Update();
        currentUpdateDuration += updateDuration;
    }

    private int GetClosestZone(Vector3 coord)
    {
        int zoneID = -1;
        float distance = Mathf.Infinity;

        for (int i = 0; i < zones.Length; i++)
        {
            float newDistance = (zones[i].updateZoneCenter - coord).sqrMagnitude;
            if (newDistance < distance)
            {
                distance = newDistance;
                zoneID = i;
            }
        }
       // Debug.Log(zoneID + "/" + zones[zoneID].updateZoneOrigin);
        return zoneID;
    }


    IEnumerator ToggleUpdate()
    {
        while (currentUpdateDuration > 0)
        {
            currentUpdateDuration -= Time.deltaTime;
            yield return null;
        }

        currentUpdateDuration = updateDuration;
        updateCoroutine = null;
        customRenderTexture.Initialize();
    }

}
