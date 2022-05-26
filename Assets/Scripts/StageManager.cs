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
                if(instance == null)
                {
                    var instanceContainer = new GameObject("StageManager");
                    instance = instanceContainer.AddComponent<StageManager>();
                }
            }
            return instance;
        }
    }

    private int Score;
    [SerializeField] private GameObject GameClearText;
    [SerializeField] private GameObject ResultPopUP;

    private bool isGameClear = false;

    private void Awake()
    {
        var obj = FindObjectsOfType<GameManager>();
        if (obj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        GameManager.Instance.FadeOut();
    }
    // Update is called once per frame
    
    public void PutPuzzle()
    {
        Score++;

        if(Score >= 3)
        {
            GameClearText.SetActive(true);
            isGameClear = true;
            Debug.Log("Game Clear!");
        }
    }
}
