using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

[System.Serializable]
struct TitleUI
{
    public GameObject UI;
    public Vector2 startPos;
    public Vector2 MovePos;
}

public class TitleManager : Singleton<TitleManager>
{
    [SerializeField] private List<TitleUI> TitleUI = new List<TitleUI>();
    [SerializeField] private GameObject TitleMenu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<TitleManager>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartTitle");
    }

    public void HomeBtn()
    {
        StartCoroutine(EndTitle("Title"));
    }

    public void StartBtn()
    {
        StartCoroutine(EndTitle("Ingame"));
    }

    public void ExitBtn()
    {
        GameManager.Instance.Termination();
    }

    private IEnumerator StartTitle()
    {
        TitleMenu.SetActive(false);
        GameManager.Instance.FadeOut();

        yield return new WaitForSeconds(1f);

        TitleMenu.SetActive(true);
        TitleUI[0].UI.GetComponent<RectTransform>().DOAnchorPos(TitleUI[0].MovePos, 1.3f).SetEase(Ease.OutBounce);
        for(int i = 1; i < TitleUI.Count; i++)
        {
            TitleUI[i].UI.GetComponent<RectTransform>().DOAnchorPos(TitleUI[i].MovePos, 1.3f).SetEase(Ease.OutQuad);
        }

        yield return null;
    }

    private IEnumerator EndTitle(string loadScene)
    {
        TitleUI[0].UI.GetComponent<RectTransform>().DOAnchorPos(TitleUI[0].startPos, 1.3f).SetEase(Ease.OutQuad);
        for (int i = 1; i < TitleUI.Count; i++)
        {
            TitleUI[i].UI.GetComponent<RectTransform>().DOAnchorPos(TitleUI[i].startPos, 1.3f).SetEase(Ease.OutQuad);
        }

        yield return new WaitForSeconds(1f);

        TitleMenu.SetActive(false);
        GameManager.Instance.FadeIn();

        yield return new WaitForSeconds(1f);
        Loading.LoadingScene(loadScene);
    }
    
}
