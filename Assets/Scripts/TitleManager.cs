using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
struct TitleBtn
{
    GameObject Btn;
    float 
}

public class TitleManager : MonoBehaviour
{
    private static TitleManager instance;
    public static TitleManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }

        Destroy(gameObject);
    }

    [SerializeField] private GameObject TitleText;

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



        yield return null;
    }
}
