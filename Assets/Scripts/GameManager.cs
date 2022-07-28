using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public Image Panel;
    [SerializeField] private GameObject SettingPopUP;
    [SerializeField] private GameObject ProducerPopUP;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] private GameObject AskObj;
    [SerializeField] private Slider soundBar;

    //별 개수 저장
    public int[] starReached = new int[18]; //스테이지 수 만큼 추가

    //스테이지 진척도 저장
    public int levelReached = 13;
    public int currentStageNum;

    [HideInInspector]
    public bool isCount = true;
    [HideInInspector]
    public bool isPause = false;
    private bool isProducer = false;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<GameManager>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        //불러오기
        //Save();
        Load();
    }
    public void SoundSetting()
    {
        SoundManager.Instance.BGM.volume =
            soundBar.value;
    }

    public void Termination()
    {
        SoundManager.Instance.PlaySound(SoundEffect.Button);
        canvasGroup.blocksRaycasts = true;
        AskObj.SetActive(true);
        AskObj.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutElastic);
    }
    public void Yes()
    {
        SoundManager.Instance.PlaySound(SoundEffect.Button);
        //저장
        Save();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void No()
    {
        SoundManager.Instance.PlaySound(SoundEffect.Button);
        canvasGroup.blocksRaycasts = false;
        AskObj.transform.DOScale(Vector3.zero, 0.45f).SetEase(Ease.InQuad).OnComplete
            (() => { AskObj.transform.localScale = Vector3.one * 0.8f; AskObj.SetActive(false); });
    }
    public void IngameSettingBtn()
    {
        SoundManager.Instance.PlaySound(SoundEffect.Button);
        canvasGroup.blocksRaycasts = true;
        SettingPopUP.transform.DOScale(new Vector3(1, 1, 1), 0.7f).SetEase(Ease.OutQuad);
    }

    public void CloseBtn()
    {
        SoundManager.Instance.PlaySound(SoundEffect.Button);
        if (isProducer)
        {
            isProducer = false;
            ProducerPopUP.GetComponent<RectTransform>().DOSizeDelta(new Vector2(743, 0), 0.5f);
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            SettingPopUP.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutQuad);
        }
    }

    public void ProducerBtn()
    {
        SoundManager.Instance.PlaySound(SoundEffect.Button);
        isProducer = true;
        ProducerPopUP.GetComponent<RectTransform>().DOSizeDelta(new Vector2(743, 493), 0.5f);
    }

    public void IngameHomeBtn()
    {
        SoundManager.Instance.PlaySound(SoundEffect.Button);
        StartCoroutine("IngameGoHome");
    }
    private IEnumerator IngameGoHome()
    {
        FadeIn();
        yield return new WaitForSeconds(1.6f);

        SceneManager.LoadScene("Title");
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    #region 페이드 인
    private IEnumerator FadeInCoroutine()
    {
        float fadeCount = 0;
        while(fadeCount < 1.0f)
        {   
            fadeCount += 0.01f;
            Panel.color = new Color(0, 0, 0, fadeCount);
            yield return new WaitForSeconds(0.01f);
        }
    }
    #endregion

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    #region 페이드 아웃
    private IEnumerator FadeOutCoroutine()
    {
        float fadeCount = 1;
        while (fadeCount > 0.0f)
        {
            fadeCount -= 0.01f;
            Panel.color = new Color(0, 0, 0, fadeCount);
            yield return new WaitForSeconds(0.01f);
        }
    }
    #endregion

    public IEnumerator SelectLevelCoroutine(bool isStageBtn)
    {
        if (isStageBtn)
        {
            GameObject clickObject = EventSystem.current.currentSelectedGameObject;
            GameManager.Instance.currentStageNum = clickObject.GetComponent<Stage>().Stage_Num;
        }
        else
        {
            GameManager.Instance.currentStageNum++;
        }
        GameManager.Instance.FadeIn();

        yield return new WaitForSeconds(1.6f);
        SceneManager.LoadScene($"Stage");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("Level", levelReached);
        for (int i = 0; i < starReached.Length; i++)
        {
            PlayerPrefs.SetInt($"Star{i}", starReached[i]);
        }
    }

    private void Load()
    {
        levelReached = PlayerPrefs.GetInt("Level");
        for(int i = 0; i < starReached.Length; i++)
        {
            starReached[i] = PlayerPrefs.GetInt($"Star{i}");
        }

        if(levelReached == 0)
        {
            levelReached = 1;
        }
    }


    private void OnApplicationQuit()
    {
        Save();
    }
}
