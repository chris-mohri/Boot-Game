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
    private bool locked;
    private bool drag_started;
   
    void Awake()
    {
        //drag_started=false;

        locked=true;
        initialDragXY=transform.position;
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

        if (locked==true) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position=mousePosition; 
        }

    }

 }


