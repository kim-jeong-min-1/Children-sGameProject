using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockObj : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Transform Canvas; //UI가 소속되있는 최상단의 캔버스
    private RectTransform rect;                //UI의 RectTransform
    private Transform previousParent;          //UI가 드래그 전에 소속되어 있던 부모의 Transform
    private CanvasGroup canvasGroup;           //UI 제어를 위한 CanvasGroup
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        previousParent = transform.parent; //드래그 전 부모의 transform을 저장

        transform.SetParent(Canvas);  // 부모 오브젝트를 Canvas로 설정
        transform.SetAsLastSibling(); // 화면의 가장 위에 보이도록 캔버스의 마지막 자식으로 설정

        canvasGroup.alpha = 0.7f;           // 드래그 중인것을 표시하기위해 alpha 값을 변경
        canvasGroup.blocksRaycasts = false; // 드래그 중인 오브젝트가 마우스와 슬롯이 충돌하는걸 막지 않게 끔 광선 충돌처리를 꺼준다.
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position; // 현재 스크린상의 마우스 위치를 UI 위치로 설정(UI가 마우스를 쫓아 다니는 상태)
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
