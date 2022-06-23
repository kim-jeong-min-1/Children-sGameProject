using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Star : MonoBehaviour
{
    [SerializeField] GameObject StarEffect;
    Vector2 previousScale = new Vector2(0, 0);
    
   
    public void GetStar()
    {
        gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(new Vector2(120, 120), 0.7f).SetEase(Ease.OutBack).OnComplete(StarParticle));
        sequence.InsertCallback(0.5f, StarParticle);        
    }

    private void StarParticle()
    {
        print("eee");
        Instantiate(StarEffect, transform.position, Quaternion.identity);
    }
    
    public void RecycleStar()
    {
        transform.localScale = previousScale;
        gameObject.SetActive(false);
    }
}
