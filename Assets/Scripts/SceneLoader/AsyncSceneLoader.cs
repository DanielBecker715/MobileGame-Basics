using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{
    float progressBar;
    string currentScene;

    //User input (scenes to be load)
    private List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    public void addScene(string sceneName)
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
    }
    public void loadAllScenes()
    {
        StartCoroutine(loadingScreen());
    }

    public string getCurrentScene()
    {
        return currentScene;
    }
    public float getTotalProgress()
    {
        return progressBar;
    }

    private IEnumerator loadingScreen()
    {
        float totalProgress = 0;
        for (int i=0; i<scenesToLoad.Count; ++i)
        {
            while (!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                progressBar = totalProgress/scenesToLoad.Count;
                currentScene = scenesToLoad[i].ToString();
                yield return null;
            }
        }
    }
}
