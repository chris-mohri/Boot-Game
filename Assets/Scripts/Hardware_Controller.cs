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
    private double efficiency_current = 100.0;

    //[SerializeField]
    public double depletion_rate = 5; // depletes efficiency_max by 5 per second. programs will affect this
    private double additional_depletion_rate=0.00;

    //[SerializeField]
    public double depletion_multiplier = 1.00; //base multiplier for depletion_rate

    public double increase_happiness_rate = 0.8;

    private double additional_multipliers = 0.00; // other hardware will affect this

    //boolean to determine if the component has yet to reach this threshold in its current lifespan
    private bool half_mark;
    private bool three_quarter_mark;

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

        efficiency_current=efficiency_max;

        half_mark=true;
        three_quarter_mark=true;

    }

    //  EVENTS ------------------------------------------------------------------------------------------------
    public void Add_Additional_Multiplier(double mult){
        additional_multipliers += mult;
    }

    void Deplete(){
        double total_depletion_multiplier = depletion_multiplier + additional_multipliers;
        if (total_depletion_multiplier<0.4)
            total_depletion_multiplier=0.4;

        efficiency_current -= Time.deltaTime * (depletion_rate+additional_depletion_rate) * total_depletion_multiplier;

        //depletes happiness
        if (half_mark==true && efficiency_current <= efficiency_max/2){
            Game_State.Instance.Add_Happiness(-8);
            half_mark=false;
        }
        if (three_quarter_mark==true && efficiency_current <= efficiency_max/4){
            Game_State.Instance.Add_Happiness(-18);  
            three_quarter_mark=false;
        }

        if (efficiency_current<=0) efficiency_current=0;
    }

    void Reset(){
        half_mark=true;
        three_quarter_mark = true;

        efficiency_current = efficiency_max;


    }

    void Update(){

        if (this.name=="GPU")
            additional_depletion_rate = Game_State.Instance.Get_GPU_Penalty();

        if (this.name=="CPU")
            additional_depletion_rate = Game_State.Instance.Get_CPU_Penalty();

        if (this.name=="HDD")
            additional_depletion_rate = Game_State.Instance.Get_Storage_Penalty();

        if (this.name=="FAN")
            additional_depletion_rate = Game_State.Instance.Get_Cooling_Penalty();

        if (this.name=="RAM")
            additional_depletion_rate = Game_State.Instance.Get_RAM_Penalty();

        double fraction = Math.Round(efficiency_current / efficiency_max,2);

        //if game started, start depleting
        if(Game_State.Instance.Get_Game_Started()==true){
            Deplete();
            Game_State.Instance.Add_Happiness(Math.Round(Time.deltaTime * increase_happiness_rate * fraction,2));

            //Debug.Log(efficiency_current);
        }

        
        text.text=(Math.Round(efficiency_max,2)).ToString()+"\n\n"+(Math.Round(efficiency_current,2)).ToString()+"\n\n"+(depletion_rate + additional_depletion_rate).ToString();
        bar_fill.transform.localScale=new Vector3(1, (float)efficiency_current/(float)efficiency_max, 1);

        //increases Happiness

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

    void OnMouseUp(){
        sprite_renderer.sortingOrder=1;
        transform.position=new Vector3(transform.position.x, transform.position.y, 0); 


    }

    void OnMouseDrag(){
        //Debug.Log("drag");

        //locked==false
        if (true) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position=mousePosition;
            transform.position=new Vector3(transform.position.x, transform.position.y, -1); 
            
        }

        sprite_renderer.sortingOrder=2;

    }

 }


