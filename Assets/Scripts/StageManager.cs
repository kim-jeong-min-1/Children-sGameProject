using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private GameObject ResultPopUP;
    [SerializeField] private GameObject PopUP;
    [SerializeField] private List<Star> StarObj = new List<Star>(3);
    [SerializeField] private SpriteRenderer[] BlockObjectSprite = new SpriteRenderer[3];
    [SerializeField] private SpriteRenderer[] HallObjectSprite = new SpriteRenderer[3];
    [SerializeField] private Image stageBackGround;

    [SerializeField] StageSO stageSO;
    private int Score;
    private int StarCount = 3;
    private bool isGameClear = false;

    private void Awake()
    {
        LoadStage();
    }

    private void Start()
    {
        GameManager.Instance.FadeOut();
    }
    // Update is called once per frame
    

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
            StartCoroutine(ResultPopUpCoroutine());
        }
    }

    //스테이지 불러오기
    private void LoadStage()
    {
        BlockObjectSprite[0].sprite = stageSO.stageDatas[GameManager.Instance.currentStageNum-1].BlockObj_1;
        BlockObjectSprite[1].sprite = stageSO.stageDatas[GameManager.Instance.currentStageNum-1].BlockObj_2;
        BlockObjectSprite[2].sprite = stageSO.stageDatas[GameManager.Instance.currentStageNum-1].BlockObj_3;

        HallObjectSprite[0].sprite = stageSO.stageDatas[GameManager.Instance.currentStageNum-1].BlocHallkObj_1;
        HallObjectSprite[1].sprite = stageSO.stageDatas[GameManager.Instance.currentStageNum-1].BlocHallkObj_2;
        HallObjectSprite[2].sprite = stageSO.stageDatas[GameManager.Instance.currentStageNum-1].BlockHallObj_3;

        stageBackGround.sprite = stageSO.stageDatas[GameManager.Instance.currentStageNum - 1].BackGround;
    }

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
    
    public void HomeBtn()
    {
        GameManager.Instance.IngameHomeBtn();
    }
}
