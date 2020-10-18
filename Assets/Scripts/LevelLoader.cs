using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Malee.List;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    
    // TODO make these static?
    // TODO have levels as scriptable objects?
     
    
    
    [SerializeField] private string [] levels; // TODO  Remove this    
    [SerializeField]  [Reorderable]  private  ReorderableStringList levels2; // TODO -- Start using this
    [SerializeField] private int currentLevel=0;
    
    
    //There's a bug with Unity and rendering when an Object has no CustomEditor defined. As in this example
    //The list will reorder correctly, but depth sorting and animation will not update :(
    [System.Serializable]
    public class ReorderableStringList : ReorderableArray<string> {
    }
    
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
