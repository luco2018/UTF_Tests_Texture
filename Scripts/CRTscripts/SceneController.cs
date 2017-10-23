using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	private int numberOfScenes;
	private int currentScene;

	// Use this for initialization
	void Awake () {
		Object.DontDestroyOnLoad (gameObject);

		numberOfScenes = SceneManager.sceneCountInBuildSettings;
		currentScene = SceneManager.GetActiveScene().buildIndex;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.LeftArrow))
		{
			if (currentScene > 1)
				currentScene--;
			else
				currentScene = numberOfScenes - 1;

			SceneManager.LoadScene (currentScene);
		}
		if (Input.GetKeyUp (KeyCode.RightArrow))
		{
			if (Input.GetKeyUp (KeyCode.RightArrow))
			{
				if (currentScene < numberOfScenes - 1)
					currentScene++;
				else
					currentScene = 1;

				SceneManager.LoadScene (currentScene);
			}
		}

        if (Input.GetKeyUp(KeyCode.R))
        {
            currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }

    }




}
