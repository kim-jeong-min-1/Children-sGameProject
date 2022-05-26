using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Stage> levelBtns = new List<Stage>();

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
            if (i + 1 > GameManager.Instance.levelReached)
            {
                levelBtns[i].Stage_Btn.interactable = false;
                levelBtns[i].Lock.SetActive(true);
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
        GameManager.Instance.FadeIn();

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene($"Stage_{clickObject.GetComponent<Stage>().Stage_Num}");
    }
}
