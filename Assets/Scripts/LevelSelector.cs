using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Stage> levelBtns = new List<Stage>();
    [SerializeField] private Sprite GetStarSprite;

    void Awake()
    {
        GameManager.Instance.FadeOut();
        GetLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        StartCoroutine(SelectLevelCoroutine());
    }

    private IEnumerator SelectLevelCoroutine()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        GameManager.Instance.currentStageNum = clickObject.GetComponent<Stage>().Stage_Num;
        GameManager.Instance.FadeIn();

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene($"Stage");
    }
}
