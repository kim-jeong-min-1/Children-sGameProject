using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    private Image image;
    private RectTransform rect;
    private Color previousColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        previousColor = image.color;
    }

    public void OnDrop(PointerEventData eventData)
    {
        // pointerDrag == 현재 드래그 중인 대상
        if(eventData.pointerDrag != null)
        {
            //드래그 중인 대상의 부모를 현재 오브젝트로 설정하고, 위치를 현재 오브젝트와 동일하게 설정
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //슬롯의 색상을 변경
        image.color = new Color(0, 0, 0, 200);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //슬롯의 색상을 이전 색상으로 변경
        image.color = previousColor;
    }
}
