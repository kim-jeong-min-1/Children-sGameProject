using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
struct TitleUI
{
    public GameObject UI;
    public Vector2 startPos;
    public Vector2 MovePos;
}

public class TitleManager : MonoBehaviour
{
    private static TitleManager instance;
    public static TitleManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<TitleManager>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("TitleManager");
                    instance = instanceContainer.AddComponent<TitleManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField] private List<TitleUI> TitleUI = new List<TitleUI>();

    // Start is called before the first frame update
    void Start()
    {
        StartTitle();
    }

    public void StartTitle()
    {
        GameManager.Instance.FadeOut();
        TitleUI[0].UI.GetComponent<RectTransform>().DOAnchorPos(TitleUI[0].MovePos, 2f).SetEase(Ease.OutBounce);
        for(int i = 1; i < TitleUI.Count; i++)
        {
            TitleUI[i].UI.GetComponent<RectTransform>().DOAnchorPos(TitleUI[i].MovePos, 2f).SetEase(Ease.OutQuad);
        }
    }

    public void EndTitle()
    {
        TitleUI[0].UI.GetComponent<RectTransform>().DOAnchorPos(TitleUI[0].startPos, 2f).SetEase(Ease.OutQuad);
        for (int i = 1; i < TitleUI.Count; i++)
        {
            TitleUI[i].UI.GetComponent<RectTransform>().DOAnchorPos(TitleUI[i].startPos, 2f).SetEase(Ease.OutQuad);
        }
        GameManager.Instance.FadeIn();
    }
}
