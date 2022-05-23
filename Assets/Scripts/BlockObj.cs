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
    public bool isInDrop;
    private Vector3 drophallPos;
    private void Start()
    {
        previousPos = this.transform.position;
    }

    private void OnMouseDown()
    {
        
    }
}
