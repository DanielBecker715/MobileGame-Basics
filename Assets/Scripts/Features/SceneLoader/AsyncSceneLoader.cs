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
    
    /// <summary>
    /// Adds the scene to a queue.
    /// </summary>
    public void addSceneToPreloading(string sceneName)
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
        Debug.Log("Added scene " + sceneName + " to the preloading");
    }

    /// <summary>
    /// Loads the scenes from the queue and waits for approval
    /// </summary>
    public void startPreloading()
    {
        StartCoroutine(LoadAsynchronously());
    }

    /// <summary>
    /// Activates the preloaded scenes. Instandly loads the scene after they finished preloading
    /// </summary>
    public void approveScenesToLoad()
    {
        foreach (AsyncOperation operation in scenesToLoad)
        {
            operation.allowSceneActivation = true;
        }
    }

    IEnumerator LoadAsynchronously()
    {
        //Start an asynchronous operation to load the main game scene
        Debug.Log("Preloading started");
        float totalProgress = 0;
        int counter = 0;
        foreach (AsyncOperation operation in scenesToLoad)
        {
            counter++;
            Debug.Log("Counter:" + counter+ " Scenes:" + scenesToLoad.Count);
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                totalProgress += operation.progress;
                progressBar = totalProgress / scenesToLoad.Count;
                currentScene = operation.ToString();
                yield return null;
            }
        }
    }
}
