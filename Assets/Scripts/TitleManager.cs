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
    [SerializeField] private GameObject TitleMenu;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartTitle");
    }

    public void HomeBtn()
    {
        StartCoroutine(EndTitle("Title"));
    }
    public void SettingBtn()
    {

    }
    public void StartBtn()
    {
        StartCoroutine(EndTitle("Ingame"));
    }
    public void ExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
        SceneManager.LoadScene(loadScene);
    }
}
