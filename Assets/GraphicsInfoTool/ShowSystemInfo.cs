using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]

public class ShowSystemInfo : MonoBehaviour
{
    public Text tm;
    public Text tm_hdr;
    public Text tm_vsync;
    public Text tm_renderpath;
    public GameObject[] showHideObjects;
    public GameObject dontDestroyGO;

    [SerializeField][HideInInspector]
    private string colorspace = "not assigned";
    [SerializeField][HideInInspector]
    private string grahicsjob = "not assigned";

    void Start()
    {
        if (Application.isPlaying)
        {
            DontDestroyOnLoad(dontDestroyGO);
        }

        if (Application.isPlaying && SceneManager.sceneCountInBuildSettings > 1)
        {
            SceneManager.LoadScene(1);
        }

        updateText();
    }

    public void updateText()
    {
        #if UNITY_EDITOR
                colorspace = PlayerSettings.colorSpace.ToString();
                grahicsjob = PlayerSettings.graphicsJobMode.ToString();
        #endif

        tm.text = "";

        tm.text = tm.text + TitleText("Unity : ") + Application.unityVersion + "\n";
        tm.text = tm.text + TitleText("Device : ") + SystemInfo.deviceModel + "\n";
        tm.text = tm.text + TitleText("OS : ") + SystemInfo.operatingSystem + "\n";
        tm.text = tm.text + TitleText("CPU : ") + SystemInfo.processorType + "\n";
        tm.text = tm.text + TitleText("Card : ") + SystemInfo.graphicsDeviceName + "\n";
        tm.text = tm.text + TitleText("API : ") + SystemInfo.graphicsDeviceType + "\n";
        tm.text = tm.text + TitleText("Platform : ") + Application.platform.ToString() + "\n";
        tm.text = tm.text + "<i>" + TitleText("Color Space : ") + colorspace + "</i>" + "\n";
        tm.text = tm.text + "<i>" + TitleText("GraphicsJob Mode : ") + grahicsjob + "</i>" + "\n";
        tm.text = tm.text + TitleText("Multi-thread : ") + BooleanText(SystemInfo.graphicsMultiThreaded) + "\n";
        tm.text = tm.text + TitleText("VSync : ") + QualitySettings.vSyncCount.ToString() + "\n";
        tm.text = tm.text + TitleText("S.M. Support : ") + SystemInfo.graphicsShaderLevel.ToString() + "\n";
        tm.text = tm.text + TitleText("Tier : ") + Graphics.activeTier.ToString() + "\n";

        if (Camera.main != null)
        {
            tm.text = tm.text + TitleText("Camera Rendering Path : ") + Camera.main.renderingPath.ToString() + "\n";
            if (Camera.main.allowHDR) tm_hdr.text = "HDR On"; else tm_hdr.text = "HDR Off";
            tm_renderpath.text = Camera.main.renderingPath.ToString();
        }
        else
        {
            tm.text = tm.text + WarningText("Camera is null") + "\n";
            tm_hdr.text = "HDR --";
            tm_renderpath.text = "Change Render Path";
        }

        tm.text = tm.text + H2Text("Compute Shader : ") + BooleanText(SystemInfo.supportsComputeShaders) + "\n";
        tm.text = tm.text + H2Text("GPU Instancing : ") + BooleanText(SystemInfo.supportsInstancing) + "\n";
        tm.text = tm.text + H2Text("2D Array Texture : ") + BooleanText(SystemInfo.supports2DArrayTextures) + "\n";
        tm.text = tm.text + H2Text("3D Texture : ") + BooleanText(SystemInfo.supports3DTextures) + "\n";
        tm.text = tm.text + H2Text("3D Render Texture : ") + BooleanText(SystemInfo.supports3DRenderTextures) + "\n";
        tm.text = tm.text + H2Text("Cubemap Array Texture : ") + BooleanText(SystemInfo.supportsCubemapArrayTextures) + "\n";
       // tm.text = tm.text + H2Text("Image Effect : ") + BooleanText(SystemInfo.supportsImageEffects) + "\n";
       // tm.text = tm.text + H2Text("Motion Vector : ") + BooleanText(SystemInfo.supportsMotionVectors) + "\n";
       // tm.text = tm.text + H2Text("Raw Shadow Depth Sampling : ") + BooleanText(SystemInfo.supportsRawShadowDepthSampling) + "\n";
        tm.text = tm.text + H2Text("Sparse Textures : ") + BooleanText(SystemInfo.supportsSparseTextures) + "\n";

        //tm.text = tm.text + "MSG"  + "\n";
        if (SceneManager.sceneCount > 1)
        {
            float count = SceneManager.sceneCountInBuildSettings - 1;
            tm.text = tm.text + TitleText(SceneManager.GetSceneAt(1).buildIndex + "/" + count) + " " +
            SceneManager.GetSceneAt(1).name.ToString() + "\n";
        }
        else
        {
            tm.text = tm.text + TitleText(SceneManager.GetActiveScene().buildIndex + "/" +
            SceneManager.sceneCountInBuildSettings) + " " +
            SceneManager.GetActiveScene().name.ToString() + "\n";
        }

        tm.text = tm.text + "Click Here to Show/Hide";

        //
        tm_vsync.text = "VSync " + QualitySettings.vSyncCount;
    }


    void Update()
    {
        #if UNITY_EDITOR
           if (!Application.isPlaying) updateText();
        #endif

        if (Application.isPlaying && Input.touchCount > 0 && Input.GetTouch(0).tapCount == 2 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetKeyDown(KeyCode.Space))
        {
            NextScene();
            updateText();
        }
    }


    //========Text Styles========
    private string TitleText(string text)
    {
        return "<color=#00ffff>" + text + "</color>";
    }

    private string WarningText(string text)
    {
        return "<color=#ffff00>" + text + "</color>";
    }

    private string H2Text(string text)
    {
        return "<color=#aaaaaa>" + text + "</color>";
    }

    private string BooleanText(bool b)
    {
        if (b)
        {
            return "<color=#00ff00>" + b.ToString() + "</color>";
        }
        else
        {
            return "<color=#ff0000>" + b.ToString() + "</color>";
        }
    }
    //============Change Setting Functions===========
    public void NextScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex < SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(sceneIndex + 1);
        else
            SceneManager.LoadScene(1);
    }

    public void PrevScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex > 1)
            SceneManager.LoadScene(sceneIndex - 1);
        else
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }

    public void ToggleShowInfo()
    {
        if (showHideObjects[0].activeSelf)
        {
            for (int i = 0; i < showHideObjects.Length; i++)
                showHideObjects[i].SetActive(false);
            
            InfoTexts infotextlist = (InfoTexts)FindObjectOfType(typeof(InfoTexts));
            if (infotextlist != null)
            {
                Debug.Log(infotextlist.gameObject.name + " " + infotextlist.golist.Length);
                infotextlist.ShowInfoText();
            }
        }
        else
        {
            for (int i = 0; i < showHideObjects.Length; i++)
                showHideObjects[i].SetActive(true);

            InfoTexts infotextlist = (InfoTexts)FindObjectOfType(typeof(InfoTexts));
            if (infotextlist != null)
            {
                Debug.Log(infotextlist.gameObject.name + " " + infotextlist.golist.Length);
                infotextlist.HideInfoText();
            }
        }
    }

    public void changeVsync()
    {
        int i = QualitySettings.vSyncCount;
        if (i<2) i++;
        else i = 0;

        QualitySettings.vSyncCount = i;

    }

    public void ChangeRenderPath()
    {
        switch(Camera.main.renderingPath)
        {
            case RenderingPath.Forward:
                Camera.main.renderingPath = RenderingPath.DeferredShading;
                break;
            case RenderingPath.DeferredShading:
                Camera.main.renderingPath = RenderingPath.Forward;
                break;
            default:
                Camera.main.renderingPath = RenderingPath.Forward;
                break;
        }
    }

    public void HDR_Toggle()
    {
        if(Camera.main.allowHDR) Camera.main.allowHDR = false;
        else Camera.main.allowHDR = true;    
    }

}

