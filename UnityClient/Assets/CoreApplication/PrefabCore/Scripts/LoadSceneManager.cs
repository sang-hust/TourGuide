using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : PersistentSingleton<LoadSceneManager>
{
    protected override void Awake()
    {
        Application.targetFrameRate = 60;
        base.Awake();
    }
    public void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(2);
        LoadScene("HomeScene");
    }
}