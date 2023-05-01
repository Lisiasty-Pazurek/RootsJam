using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public List<Scene> levelList;
    public List<Canvas> menuCanvas;
    void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void LoadLevel (string scene) 
    {
        SceneManager.LoadSceneAsync(scene);
    }

    public void Quit() 
    {
        Application.Quit();
    }


}
