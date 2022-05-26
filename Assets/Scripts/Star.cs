using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Star : MonoBehaviour
{
    Vector2 previousScale = new Vector2(250, 250);
   
    public void GetStar()
    {
        gameObject.SetActive(true);
        gameObject.transform.DOScale(new Vector2(120, 120), 0.7f).SetEase(Ease.OutQuad);
    }
    
    public void RecycleStar()
    {
        gameObject.transform.localScale = previousScale;
        gameObject.SetActive(false);
    }
}
