using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    private static readonly int Start = Animator.StringToHash("Start");

    // public static LevelLoader Ll;
    
    /*public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;*/
    //
    // public void LoadLevelIE(int sceneIndex)
    // {
    //     StartCoroutine(LoadAsynchronously(sceneIndex));
    // }
    //
    // private IEnumerator LoadAsynchronously(int sceneIndex)
    // {
    //     var operation = SceneManager.LoadSceneAsync(sceneIndex);
    //     
    //     loadingScreen.SetActive(true);
    //
    //     while (!operation.isDone)
    //     {
    //         var progress = Mathf.Clamp01(operation.progress / .9f);
    //         
    //         slider.value = progress;
    //         progressText.text = progress * 100f + "%";
    //         
    //         yield return null;
    //     }
    // }
    
    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadNextLevel();
        }
    }*/

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadLevelIe(sceneIndex));
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevelIe(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private IEnumerator LoadLevelIe(int sceneIndex)
    {
        transition.SetTrigger(Start);
        
        yield return new WaitForSeconds(transition.GetCurrentAnimatorStateInfo(0).length);

        var operation = SceneManager.LoadSceneAsync(sceneIndex);

        //loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            var progress = Mathf.Clamp01(operation.progress / .9f);

            //slider.value = progress;
            //progressText.text = progress * 100f + "%";

            yield return null;
        }

        // SceneManager.LoadScene(sceneIndex);
    }
}














