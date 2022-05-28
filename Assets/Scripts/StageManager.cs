using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    public static StageManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<StageManager>();
            }
            return instance;
        }
    }

    [SerializeField] private GameObject ResultPopUP;
    [SerializeField] private GameObject PopUP;
    [SerializeField] private List<Star> StarObj = new List<Star>(3);

    private int Score;
    private int StarCount = 3;
    private bool isGameClear = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        GameManager.Instance.FadeOut();
    }
    // Update is called once per frame

    public void PutPuzzle()
    {
        Score++;

        if(Score >= 3)
        {
            isGameClear = true;
            GameManager.Instance.levelReached++;
            StartCoroutine(ResultPopUpCoroutine());
        }
    }

    private IEnumerator ResultPopUpCoroutine()
    {
        yield return new WaitForSeconds(1);
        ResultPopUP.SetActive(true);
        PopUP.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -67), 0.7f).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(0.9f);
        StartCoroutine(StarCoroutine());
    }

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
