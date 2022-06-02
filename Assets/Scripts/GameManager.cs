using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] public Image Panel;
    [SerializeField] private GameObject SettingMenu;

    public int[] starReached = new int[3] { 0, 0, 0 }; //스테이지 수 만큼 추가
    public int levelReached = 1;
    public int currentStageNum;

    public bool isCount = true;
    // Start is called before the first frame update

    public void LoadSelectScene()
    {
        //levelSelcetor 씬으로 이동
    }

    public void IngameSettingBtn()
    {
        //설정 창
    }

    public void IngameHomeBtn()
    {
        StartCoroutine("IngameGoHome");
    }
    private IEnumerator IngameGoHome()
    {
        FadeIn();
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Title");
    }


    public void FadeIn()
    {
        StartCoroutine("FadeInCoroutine");
    }

    #region 페이드 인
    private IEnumerator FadeInCoroutine()
    {
        Panel.color = new Color(0, 0, 0, 0);

        float fadeCount = 0;
        while(fadeCount < 1.0f)
        {   
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            Panel.color = new Color(0, 0, 0, fadeCount);
        }
    }
    #endregion

    public void FadeOut()
    {
        StartCoroutine("FadeOutCoroutine");
    }

    #region 페이드 아웃
    private IEnumerator FadeOutCoroutine()
    {
        Panel.color = new Color(0, 0, 0, 1);
        float fadeCount = 1;
        while (fadeCount > 0.0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            Panel.color = new Color(0, 0, 0, fadeCount);
        }
    }
    #endregion
}
