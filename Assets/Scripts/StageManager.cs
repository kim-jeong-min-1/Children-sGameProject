using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    private int Score;
    private int StarCount = 0;
    private bool isGameClear = false;
    private float timer = 0;

    //로딩하는 동안 스테이지 불러오기
    private void Awake()
    {
        LoadStage();
        StartCoroutine(CountCoroutine());
    }

    private void Update()
    {
        if(isGameClear == false && GameManager.Instance.isCount == false)
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
        GameManager.Instance.FadeOut();
        yield return new WaitForSeconds(1f);
        int countTime = 3;
        CountText.gameObject.SetActive(true);

        while(countTime > 0)
        {
            CountText.text = $"{countTime}";

            yield return new WaitForSeconds(1f);
            countTime--;
        }
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
        GameManager.Instance.IngameHomeBtn();
    }

    public void NextBtn()
    {
        StartCoroutine(GameManager.Instance.SelectLevelCoroutine(false));
    }
}
