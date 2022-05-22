using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockObj : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Transform Canvas; //UI�� �Ҽӵ��ִ� �ֻ���� ĵ����
    private RectTransform rect;                //UI�� RectTransform
    private Transform previousParent;          //UI�� �巡�� ���� �ҼӵǾ� �ִ� �θ��� Transform
    private CanvasGroup canvasGroup;           //UI ��� ���� CanvasGroup
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        previousParent = transform.parent; //�巡�� �� �θ��� transform�� ����

        transform.SetParent(Canvas);  // �θ� ������Ʈ�� Canvas�� ����
        transform.SetAsLastSibling(); // ȭ���� ���� ���� ���̵��� ĵ������ ������ �ڽ����� ����

        canvasGroup.alpha = 0.7f;           // �巡�� ���ΰ��� ǥ���ϱ����� alpha ���� ����
        canvasGroup.blocksRaycasts = false; // �巡�� ���� ������Ʈ�� ���콺�� ������ �浹�ϴ°� ���� �ʰ� �� ���� �浹ó���� ���ش�.
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position; // ���� ��ũ������ ���콺 ��ġ�� UI ��ġ�� ����(UI�� ���콺�� �Ѿ� �ٴϴ� ����)
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(transform.parent == Canvas)
        {
            transform.SetParent(previousParent);
            rect.position = previousParent.GetComponent<RectTransform>().position;
        }

        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }
}
