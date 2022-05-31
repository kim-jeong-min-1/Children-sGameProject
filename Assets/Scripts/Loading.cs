using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private static string nextScene;

    [SerializeField] Image LoadingBar;
    public static void LoadingScene(string loadScene)
    {
        nextScene = loadScene;
        SceneManager.LoadScene("Loading");
    }

    void Start()
    {
        StartCoroutine(StartLoading());
    }

    private IEnumerator StartLoading()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        float wTime = 0f;

        while(!op.isDone)
        {
            wTime += 0.1f;

            if(wTime < 2f)
            {
                LoadingBar.fillAmount = wTime;
            }
            else
            {
                timer += Time.unscaledTime;
                LoadingBar.fillAmount = Mathf.Lerp(0.9f, 1, timer);
                if(LoadingBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }

            }
            yield return new WaitForSeconds(0.1f);
        }
    }

}
