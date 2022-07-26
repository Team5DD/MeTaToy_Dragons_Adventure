using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoLobby : MonoBehaviour
{
    public GameObject loadingUI;

    public void GoLobbyBtn_Clik()
    {
        loadingUI.SetActive(true);

        StartCoroutine(SceneLoading());
    }

    IEnumerator SceneLoading()
    {
        var mAsyncOperation = SceneManager.LoadSceneAsync("MainTitle", LoadSceneMode.Additive);
        yield return mAsyncOperation;

        mAsyncOperation = SceneManager.UnloadSceneAsync("Char_Choice");
        yield return mAsyncOperation;
    }
}
