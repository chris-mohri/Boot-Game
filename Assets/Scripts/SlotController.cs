using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class SlotController : MonoBehaviour
{
    
    [SerializeField]
    private Image image;

    //[SerializeField]
    //private GameObject HoldColor;

    //[SerializeField]
    //private HandleDragAndDrop drag_drop;

    //// OTHER INFO
    private Vector2 initialDragXY;
    private bool drag_started;
   
    void Awake()
    {
        //drag_drop=GameObject.FindWithTag("UI").GetComponent<HandleDragAndDrop>();
        drag_started=false;
        Debug.Log("awake");
    }


     void OnMouseOver()
    {
        //Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {

    }

    void OnMouseDown()
    {
        //Debug.Log("down");
    }

    void OnMouseDrag()
    {
        //Debug.Log("drag");

    }

 }


