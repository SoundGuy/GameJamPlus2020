using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneControllerManager : MonoBehaviour
{
    public static SceneControllerManager instance;

    private bool isFading;
    [SerializeField] private float fadeDuration;
    [SerializeField] private CanvasGroup faderCanvasGroup = null;
    [SerializeField] private Image faderImage;
    [SerializeField] private string startingScene = null;
    [SerializeField] private string nextScene = null;
    private int sceneIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private IEnumerator Start()
    {
        faderImage.color = new Color(0f, 0f, 0f, 1f);
        faderCanvasGroup.alpha = 1f;

        //Start the first scene loading and wait for it to finish
        yield return StartCoroutine(LoadSceneAndSetActive(startingScene));
        sceneIndex = 0;
        StartCoroutine(Fade(0));
    }


    private IEnumerator Fade(float finalAlpha)
    {
        isFading = true;

        faderCanvasGroup.blocksRaycasts = true;

        float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;

        while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);

            yield return null;
        }

        isFading = false;

        faderCanvasGroup.blocksRaycasts = false;
    }



    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        SceneManager.SetActiveScene(newlyLoadedScene);
    }

    private IEnumerator FadeAndSwitchScenes(string sceneName)
    {
        yield return StartCoroutine(Fade(1f));

        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

        yield return StartCoroutine(Fade(0f));
    }

    public void FadeAndLoadScene(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneName));
        }
    }


}

