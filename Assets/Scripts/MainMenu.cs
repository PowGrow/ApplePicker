using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int _gameSceneIndex = 1;

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(_gameSceneIndex);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
