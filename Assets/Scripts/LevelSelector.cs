using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Stage> levelBtns = new List<Stage>();
    [SerializeField] private Sprite GetStarSprite;

    void Awake()
    {
        GetLevel();
        SoundManager.Instance.PlayBGM(0.2f);
    }
    private void Start()
    {
        GameManager.Instance.FadeOut();
    }

    private void GetLevel()
    {
        for(int i = 0; i < levelBtns.Count; i++)
        {
            if (GameManager.Instance.starReached[i] > 0)
            {
                for (int j = 0; j < GameManager.Instance.starReached[i]; j++)
                {
                    levelBtns[i].StarIcon[j].GetComponent<Image>().sprite = GetStarSprite;
                }
            }

            if (i + 1 > GameManager.Instance.levelReached)
            {
                levelBtns[i].Stage_Btn.interactable = false;
                levelBtns[i].Lock.SetActive(true);
                levelBtns[i].StartEmpty.SetActive(false);
            } 
        }
    }

    public void SelectLevel()
    {
        SoundManager.Instance.PlaySound(SoundEffect.Button);
        StartCoroutine(GameManager.Instance.SelectLevelCoroutine(true));
    }

}
