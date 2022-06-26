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
        sequence.Append(transform.DOScale(new Vector2(2, 2), 0.7f).SetEase(Ease.OutBack));
        sequence.InsertCallback(0.3f, StarParticle);
    }

    private void StarParticle()
    {
        var particle = Instantiate(StarEffect, transform.position, Quaternion.identity);
        Destroy(particle.gameObject, 1.2f);
    }

    public void RecycleStar()
    {
        transform.localScale = previousScale;
        gameObject.SetActive(false);
    }
}
