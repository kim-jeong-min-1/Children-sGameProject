using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockObj : MonoBehaviour
{
    private Vector3 previousPos;
    private float startPosX;
    private float startPosY;

    private bool isDrag = false;
    private void Start()
    {
        previousPos = this.transform.position;
    }

    private void Update()
    {
        if(isDrag == true)
        {
            Vector2 mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            this.transform.position = mousePos;
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
        this.transform.position = previousPos;
    }
}
