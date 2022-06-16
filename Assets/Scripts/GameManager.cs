using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] public Image Panel;
    [SerializeField] private GameObject SettingPopUP;
    [SerializeField] private GameObject ProducerPopUP;
    [SerializeField] CanvasGroup canvasGroup;

    public int[] starReached = new int[6]; //스테이지 수 만큼 추가

    public int levelReached = 1;
    public int currentStageNum;

    public bool isCount = true;
    private bool isProducer = false;
    // Start is called before the first frame update

    public void LoadSelectScene()
    {
        //levelSelcetor 씬으로 이동
    }

    public void IngameSettingBtn()
    {
        canvasGroup.blocksRaycasts = true;
        SettingPopUP.transform.DOScale(new Vector3(1, 1, 1), 0.9f).SetEase(Ease.OutQuad);
    }

    public void CloseBtn()
    {
        if (isProducer)
        {
            isProducer = false;
            ProducerPopUP.GetComponent<RectTransform>().DOSizeDelta(new Vector2(743, 0), 0.5f);
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            SettingPopUP.transform.DOScale(new Vector3(0, 0, 0), 0.8f).SetEase(Ease.OutQuad);
        }
    }

    public void ProducerBtn()
    {
        isProducer = true;
        ProducerPopUP.GetComponent<RectTransform>().DOSizeDelta(new Vector2(743, 492), 0.5f);
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
