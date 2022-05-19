using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Stage> levelBtns = new List<Stage>();

    public int levelReached = 1;
    private int levelIndex = 1;

    void Start()
    {
        SelectLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelectLevel()
    {
        for(int i = 0; i < levelBtns.Count; i++)
        {
            if (i + 1 > levelReached)
            {
                levelBtns[i].Stage_Btn.interactable = false;
                levelBtns[i].Lock.SetActive(true);
            }              
        }
    }
}
