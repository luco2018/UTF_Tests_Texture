using UnityEngine;

public class chain_1_updating : MonoBehaviour
{
	#if UNITY_2017_1_OR_NEWER

    //public float factor = 0;

	public CustomRenderTexture	m_CustomRenderTexture;
	public Texture2D		m_ClearTexture;
	public float			m_RotationSpeed = 90.0f;

	const float m_ClearFrequency = 3.0f;
	private float m_ClearTimer = 0.0f;
	public int m_ClearColorIndex = 0;
	private Color[] m_ClearColors = { new Color(1.0f, 0.0f, 0.0f, 0.0f), new Color(0.0f, 1.0f, 0.0f, 0.0f), new Color(0.0f, 0.0f, 1.0f, 0.0f) };
	private float[] m_CheckerSizes = { 2.0f, 5.0f, 10.0f };
	private CustomRenderTextureUpdateZone[] m_UpdateZones = { new CustomRenderTextureUpdateZone(), new CustomRenderTextureUpdateZone() };

	// Use this for initialization
	void Start ()
	{
		m_UpdateZones[0].updateZoneCenter = new Vector3(0.15f, 0.15f, 0.0f);
		m_UpdateZones[0].updateZoneSize = new Vector3(0.2f, 0.2f, 0.0f);
		m_UpdateZones[0].rotation = 20.0f;
		m_UpdateZones[1].updateZoneCenter = new Vector3(0.5f, 0.5f, 0.0f);
		m_UpdateZones[1].updateZoneSize = new Vector3(0.4f, 0.4f, 0.0f);
		m_UpdateZones[1].rotation = 45.0f;

        UpdateTexture();

    }
	
	// Update is called once per frame
	void UpdateTexture ()
	{
		if(m_CustomRenderTexture != null)
		{
			//m_ClearTimer += Time.deltaTime;
			//if(m_ClearTimer > m_ClearFrequency)
			//{
				//m_ClearTimer = 0.0f;
				//m_ClearColorIndex = (m_ClearColorIndex + 1 ) % 3;
				m_CustomRenderTexture.initializationColor = m_ClearColors[m_ClearColorIndex];

				if (m_CustomRenderTexture.initializationTexture == null)
				{
					m_CustomRenderTexture.initializationTexture = m_ClearTexture;
				}
				else
				{
					m_CustomRenderTexture.initializationTexture = null;
				}


				if (m_ClearColorIndex == 0 || m_ClearColorIndex == 2)
				{
					m_CustomRenderTexture.ClearUpdateZones();
				}

				m_CustomRenderTexture.material.SetFloat("_CheckerSize", m_CheckerSizes[m_ClearColorIndex]);
				m_CustomRenderTexture.Initialize();

				if (m_ClearColorIndex == 2)
				{
					m_CustomRenderTexture.material.SetFloat("_Alpha", 0.1f);
					m_CustomRenderTexture.updateMode = CustomRenderTextureUpdateMode.OnDemand;
					m_CustomRenderTexture.Update(10);
				}
				else
				{
					m_CustomRenderTexture.material.SetFloat("_Alpha", 0.005f);
					m_CustomRenderTexture.updateMode = CustomRenderTextureUpdateMode.Realtime;
                }
		//	}

			//if (m_ClearColorIndex == 1)
			//{
			//	m_UpdateZones[0].rotation = (m_UpdateZones[0].rotation + m_RotationSpeed * Time.deltaTime) % 360.0f;
			//	m_UpdateZones[1].rotation = (m_UpdateZones[1].rotation + m_RotationSpeed * Time.deltaTime) % 360.0f;
                //m_UpdateZones[0].rotation = (m_UpdateZones[0].rotation + m_RotationSpeed * factor) % 360.0f;
               // m_UpdateZones[1].rotation = (m_UpdateZones[1].rotation + m_RotationSpeed * factor) % 360.0f;
             //   m_CustomRenderTexture.SetUpdateZones(m_UpdateZones);
			//}
		}
	}

	#endif
}
