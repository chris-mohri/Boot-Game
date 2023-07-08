using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class Hardware_Controller : MonoBehaviour
{

    [SerializeField]
    private Sprite image_idle;

    [SerializeField]
    private Sprite image_hovered;

    //// OTHER INFO
    
    private Vector2 initial_DragXY;
    private bool locked;
    private bool drag_started;


    //[SerializeField]
    public double efficiency_max  = 100.0;

    //[SerializeField]
    public double efficiency_current = 100.0;

    //[SerializeField]
    public double depletion_rate = 1.50; // depletes efficiency_max by 1.5 per second

    //[SerializeField]
    public double depletion_multiplier = 1.00; //multiplier for depletion_rate

    private double additional_multipliers = 0.00;

    SpriteRenderer sprite_renderer;
 
    
   
    void Awake()
    {
        //drag_started=false;
        initial_DragXY = new Vector2(0,0);
        locked=true;
        initial_DragXY=transform.position;

        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    //  EVENTS ------------------------------------------------------------------------------------------------
    public void Add_Additional_Multiplier(double mult){
        additional_multipliers += mult;
    }

    void Deplete(){
        double total_depletion_multiplier = depletion_multiplier + additional_multipliers;
        if (total_depletion_multiplier<0.4)
            total_depletion_multiplier=0.4;

        efficiency_current -= Time.deltaTime * depletion_rate * total_depletion_multiplier;
    }

    void Update(){
        //if game started, start depleting
        if(Game_State.Instance.Get_Game_Started()==true){
            Deplete();
            Debug.Log(efficiency_current);
        }
    }


    // MOUSE EVENTS ------------------------------------------------------------------------------------------------

     void OnMouseOver(){
        Debug.Log("Mouse is over GameObject.");
        sprite_renderer.sprite = image_hovered;
        //image_displayed = image_hovered;
        
    }

    void OnMouseExit(){
        sprite_renderer.sprite = image_idle;
    }

    void OnMouseDown(){
        //Debug.Log("down");

    }

    void OnMouseDrag(){
        //Debug.Log("drag");

        //locked==false
        if (true) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position=mousePosition; 
        }

    }

 }


