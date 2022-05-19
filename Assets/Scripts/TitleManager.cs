using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        StartCoroutine(StartTitle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartTitle()
    {
        while (true)
        {
            for(int i = 0; i< TitleUI.Count; i++)
            {
                TitleUI[i].UI.transform.position = Vector2.MoveTowards(TitleUI[i].UI.transform.position, TitleUI[i].MovePos, 1f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
