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

    [SerializeField]
    private GameObject info_box;

    [SerializeField]
    private TextMeshPro text;

    [SerializeField]
    private GameObject bar_fill;

    //// OTHER INFO
    
    private Vector2 initial_DragXY;
   // private bool locked;
    private bool drag_started;

    private bool mini_game_open;


    //[SerializeField]
    public double efficiency_max  = 100.0;

    //[SerializeField]
    public double efficiency_current = 100.0;

    //[SerializeField]
    public double depletion_rate = 5; // depletes efficiency_max by 5 per second. programs will affect this

    //[SerializeField]
    public double depletion_multiplier = 1.00; //base multiplier for depletion_rate

    private double additional_multipliers = 0.00; // other hardware will affect this

    SpriteRenderer sprite_renderer;
 
    
   
    void Awake()
    {
        //drag_started=false;
        initial_DragXY = new Vector2(0,0);
        info_box.SetActive(false);
        //locked=true;
        initial_DragXY=transform.position;

        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        mini_game_open=false;
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

        text.text=(Math.Round(efficiency_max,2)).ToString()+"\n\n"+(Math.Round(efficiency_current,2)).ToString()+"\n\n"+(Math.Round(depletion_rate,2)).ToString();
        bar_fill.transform.localScale=new Vector3(1, (float)efficiency_current/(float)efficiency_max, 1);


    }


    // MOUSE EVENTS ------------------------------------------------------------------------------------------------

     void OnMouseOver(){

        if (mini_game_open==false) {
            sprite_renderer.sprite = image_hovered;
            info_box.SetActive(true);
        }
        //image_displayed = image_hovered;
        
    }

    void OnMouseExit(){
        sprite_renderer.sprite = image_idle;
        info_box.SetActive(false);
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


