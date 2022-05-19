using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    private static FadeManager instance;
    public static FadeManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<FadeManager>();
                if(instance == null)
                {
                    var instanceContainer = new GameObject("FadeManager");
                    instance = instanceContainer.AddComponent<FadeManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField] private Image Panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {

    }

    public void FadeOut()
    {

    }
}
