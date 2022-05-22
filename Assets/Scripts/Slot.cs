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
        // pointerDrag == ���� �巡�� ���� ���
        if(eventData.pointerDrag != null)
        {
            //�巡�� ���� ����� �θ� ���� ������Ʈ�� �����ϰ�, ��ġ�� ���� ������Ʈ�� �����ϰ� ����
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //������ ������ ����
        image.color = new Color(0, 0, 0, 200);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //������ ������ ���� �������� ����
        image.color = previousColor;
    }
}
