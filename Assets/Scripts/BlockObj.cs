using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockObj : MonoBehaviour
{
    [SerializeField] private GameObject DropObj;
    private Vector2 previousPos;
    private float startPosX;
    private float startPosY;

    private bool isDrag = false;
    private bool isFinish;
    private void Start()
    {
        previousPos = this.transform.position;
    }

    private void Update()
    {
        if (!isFinish)
        {
            if (isDrag == true)
            {
                Vector2 mousePos;
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                this.transform.position = mousePos;
            }
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            isDrag = true;
        }
    }
    private void OnMouseUp()
    {
        isDrag = false;

        float Distance = Vector2.Distance(this.transform.position, DropObj.transform.position);
        if (Distance < 0.5f)
        {
            this.transform.position = DropObj.transform.position;
            isFinish = true;
            StageManager.Instance.PutPuzzle();
        }
        else
        {
            this.transform.position = previousPos;
        }
    }
}
