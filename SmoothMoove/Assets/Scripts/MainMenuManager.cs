using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject _quitAsk;

    public void LoadSceneBtn(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitBtn()
    {
        _quitAsk.SetActive(true);
    }

    public void AreYouSureYesBtn()
    {
        Application.Quit();
    }

    public void AreYouSureNoBtn()
    {
        _quitAsk.SetActive(false);
    }
}
