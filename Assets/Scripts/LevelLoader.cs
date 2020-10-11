using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    
    // TODO make these static?
    // TODO have levels as scriptable objects?
    [SerializeField] private string [] levels; // TODO -- keep array or turn into list?
    [SerializeField] private int currentLevel=0;
    
    public void LoadNextScene(string sceneName)
    {

        if (sceneName == "")
        {
            sceneName = levels[++currentLevel];
        }
        
        // TODO : reuse scene controller
       //SceneControllerManager.instance.FadeAndLoadScene(sceneName);
        SceneManager.LoadScene(sceneName);
    }


    public void RestartScene()
    {
        // TODO : reuse scene controller
        //SceneControllerManager.instance.FadeAndLoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        
#if UNITY_EDITOR
        if(EditorApplication.isPlaying) 
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        Application.Quit();
    }
    
    // TODO Make Singlton
    private void Start()
    {
        //DontDestroyOnLoad(this);
    }
}
