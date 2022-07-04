using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class StageManager : Singleton<StageManager> 
{
    [SerializeField] StageSO stageSO;

    [SerializeField] private GameObject ResultPopUP;
    [SerializeField] private GameObject PopUP;

    [SerializeField] private List<Star> StarObj = new List<Star>(3);
    [SerializeField] private SpriteRenderer[] BlockObjectSprite = new SpriteRenderer[3];
    [SerializeField] private SpriteRenderer[] HallObjectSprite = new SpriteRenderer[3];

    [SerializeField] private Image stageBackGround;
    [SerializeField] private GameObject[] BlockObject;
    [SerializeField] private TMP_Text CountText;
    [SerializeField] private GameObject PauseMenu;

    private int Score;
    private int StarCount = 0;
    private bool isGameClear = false;
    private bool isOne = false;
    private float timer = 0;

    //로딩하는 동안 스테이지 불러오기
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<StageManager>();
        }
        else
        {
            Destroy(this.gameObject);
        }
        GameManager.Instance.FadeOut();
        SoundManager.Instance.PlayBGM(0.2f);
        LoadStage();
        StartCoroutine(CountCoroutine());
    }

    private void Update()
    {
        if(isGameClear == false && GameManager.Instance.isCount == false && GameManager.Instance.isPause == false)
        {
            timer += Time.deltaTime;
        }
    }

    //퍼즐 맞췄을때 호출
    public void PutPuzzle()
    {
        Score++;

        if(Score >= 3)
        {
            if(GameManager.Instance.currentStageNum == GameManager.Instance.levelReached)
            {
                GameManager.Instance.levelReached++;
            }
            isGameClear = true;  
            
            if(timer <= 10.0f)
            {
                StarCount = 3;
            }
            else if(timer <= 20.0f)
            {
                StarCount = 2;
            }
            else
            {
                StarCount = 1;
            }

            StartCoroutine(ResultPopUpCoroutine());
        }
    }

    //카운트 다운
    private IEnumerator CountCoroutine()
    {
        yield return new WaitForSeconds(2f);
        int countTime = 3;
        CountText.gameObject.SetActive(true);

        while(countTime > 0)
        {
            SoundManager.Instance.PlaySound(SoundEffect.Count);
            CountText.text = $"{countTime}";

            yield return new WaitForSeconds(1f);
            countTime--;
        }
        SoundManager.Instance.PlaySound(SoundEffect.Start);
        CountText.text = "Start!";

        yield return new WaitForSeconds(0.5f);
        CountText.gameObject.SetActive(false);
        GameManager.Instance.isCount = false;
    }


    #region 게임시작
    //스테이지 불러오기 (스테이지 시작시 해야할 처리 들)
    private void LoadStage()
    {
        Shuffle();
        GameManager.Instance.isCount = true;
        isOne = false;

        BlockObjectSprite[0].sprite = stageSO.stageDatas[GameManager.Instance.currentStageNum-1].BlockObj_1;
        BlockObjectSprite[1].sprite = stageSO.stageDatas[GameManager.Instance.currentStageNum-1].BlockObj_2;
        BlockObjectSprite[2].sprite = stageSO.stageDatas[GameManager.Instance.currentStageNum-1].BlockObj_3;

        HallObjectSprite[0].sprite = stageSO.stageDatas[GameManager.Instance.currentStageNum-1].BlocHallkObj_1;
        HallObjectSprite[1].sprite = stageSO.stageDatas[GameManager.Instance.currentStageNum-1].BlocHallkObj_2;
        HallObjectSprite[2].sprite = stageSO.stageDatas[GameManager.Instance.currentStageNum-1].BlockHallObj_3;

        stageBackGround.sprite = stageSO.stageDatas[GameManager.Instance.currentStageNum - 1].BackGround;
    }

    //시작할때 마다 퍼즐이 랜덤하게 위치하도록 하는 함수
    private void Shuffle()
    {
        int num = BlockObject.Length;
        for(int i = 0; i < BlockObject.Length; i++)
        {
            int rand = Random.Range(0, num);

            Vector3 temp = BlockObject[rand].transform.position;
            BlockObject[rand].transform.position = BlockObject[num-1].transform.position;
            BlockObject[num-1].transform.position = temp;

            num--;
        }
    }
    #endregion

    #region 게임 종료
    //게임 종료시 팝업
    private IEnumerator ResultPopUpCoroutine()
    {
        yield return new WaitForSeconds(1);
        SoundManager.Instance.PlaySound(SoundEffect.Clear, 0.75f);
        ResultPopUP.SetActive(true);
        PopUP.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -67), 0.7f).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(0.9f);
        StartCoroutine(StarCoroutine());
    }

    //별 시스템
    private IEnumerator StarCoroutine()
    {
        if (GameManager.Instance.starReached[GameManager.Instance.currentStageNum - 1] < StarCount)
        {
            GameManager.Instance.starReached[GameManager.Instance.currentStageNum - 1] = StarCount;
        }
        for(int i =0; i< StarCount; i++)
        {
            StarObj[i].GetStar();
            yield return new WaitForSeconds(0.6f);
        }
    }
    #endregion

    public void HomeBtn()
    {
        if (isOne) return;
        SoundManager.Instance.PlaySound(SoundEffect.Button);
        GameManager.Instance.IngameHomeBtn();
        isOne = true;
    }

    public void NextBtn()
    {
        if (isOne) return;
        SoundManager.Instance.PlaySound(SoundEffect.Button);
        StartCoroutine(GameManager.Instance.SelectLevelCoroutine(false));
        isOne = true;
    }

    public void PauseBtn()
    {
        SoundManager.Instance.PlaySound(SoundEffect.Button);
        bool pause = (GameManager.Instance.isPause) ? false : true;
        GameManager.Instance.isPause = pause;

        if (pause)
        {
            PauseMenu.SetActive(pause);
            SoundManager.Instance.PlaySound(SoundEffect.Menu);
            PauseMenu.transform.GetChild(1).GetComponent<RectTransform>()
                .DOAnchorPosY(-450, 0.5f).SetEase(Ease.OutQuad);
        }
        else
        {
            SoundManager.Instance.PlaySound(SoundEffect.Menu);
            PauseMenu.transform.GetChild(1).GetComponent<RectTransform>()
                .DOAnchorPosY(-1450, 0.5f).SetEase(Ease.InQuad)
                .OnComplete(() => { PauseMenu.SetActive(pause); });
        }
    }

    public void EndBtn()
    {
        SoundManager.Instance.PlaySound(SoundEffect.Button);
        GameManager.Instance.Termination();
    }

    public void GoLevelSelect()
    {
        StartCoroutine(QuitBtn());
    }

    private IEnumerator QuitBtn()
    {
        GameManager.Instance.FadeIn();
        yield return new WaitForSeconds(1.6f);
        SceneManager.LoadScene("Ingame");
    }
}
