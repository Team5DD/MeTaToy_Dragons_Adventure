using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePasue : MonoBehaviour
{
    public GameObject continue_Btn;
    public GameObject exit_Btn;
    public GameObject loadingUI;
    public void ContinueBtn()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    public void ExitBtn()
    {
        loadingUI.SetActive(true);
        Time.timeScale = 1;
        StartCoroutine(SceneLoading());
    }

    IEnumerator SceneLoading()
    {
        var mAsyncOperation = SceneManager.LoadSceneAsync("MainTitle", LoadSceneMode.Additive);
        yield return mAsyncOperation;

        mAsyncOperation = SceneManager.UnloadSceneAsync("Stage1");
        yield return mAsyncOperation;
    }
}
