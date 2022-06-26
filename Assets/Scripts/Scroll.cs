using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Scroll : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Scrollbar scrollbar;
    const int SIZE = 3;
    float[] pos = new float[SIZE];

    float targetPos;
    float distance;
    bool isDrag;

    void Start()
    {
        distance = 1f / (SIZE - 1);
        for (int i = 0; i < pos.Length; i++) pos[i] = distance * i;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        for (int i = 0; i < SIZE; i++)
            if(scrollbar.value < pos[i] + (distance / 2) && scrollbar.value > pos[i] - (distance / 2))
            {
                targetPos = pos[i];
            } 
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDrag) scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);
    }
}
