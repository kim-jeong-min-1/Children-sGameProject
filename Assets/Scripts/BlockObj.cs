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
        if (Input.GetMouseButton(0) && GameManager.Instance.isCount == false && GameManager.Instance.isPause == false)
        {
            isDrag = true;
        }
    }
    private void OnMouseUp()
    {
        isDrag = false;

        float Distance = Vector2.Distance(this.transform.position, DropObj.transform.position);
        if (Distance < 0.35f)
        {
            SoundManager.Instance.PlaySound(SoundEffect.Success);
            this.transform.position = DropObj.transform.position;
            isFinish = true;
            StageManager.Instance.PutPuzzle();
        }
        else
        {
            SoundManager.Instance.PlaySound(SoundEffect.Fail);
            this.transform.position = previousPos;
        }
    }
}
