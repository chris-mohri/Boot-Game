using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class Hardware_Controller : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler,
                              IPointerEnterHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
                            , IPointerExitHandler
{

    [SerializeField]
    public Sprite image_idle;

    [SerializeField]
    private Sprite image_hovered;

    [SerializeField]
    private GameObject info_box;

    [SerializeField]
    private TextMeshPro text;

    [SerializeField]
    private GameObject bar_fill;

    [SerializeField]
    private GameObject mini_game;


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

    private bool dragging;
 
    
   
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
        dragging=false;

    }

    //  EVENTS ------------------------------------------------------------------------------------------------
    public void Add_Additional_Multiplier(double mult){
        additional_multipliers += mult;
    }

    public double Get_Percent_Efficiency(){
        return (Math.Round(efficiency_current / efficiency_max, 2));
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

    public void Reset(){
        half_mark=true;
        three_quarter_mark = true;

        efficiency_current = efficiency_max;

        //mini_game.SetActive(false);
        StartCoroutine(Wait_Reset());
        //Game_State.Instance.Exited_Mini_Game();
       
    }

    IEnumerator Wait_Reset()
    {
        mini_game.SetActive(false);
        yield return new WaitForSeconds(.25f);
        Game_State.Instance.Exited_Mini_Game();
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

    public void OnPointerClick( PointerEventData eventData ){
        //Debug.Log("clicked");
    }

    public void OnEndDrag( PointerEventData eventData ){
        sprite_renderer.sortingOrder=1;
        transform.position=new Vector3(transform.position.x, transform.position.y, 0); 
        dragging=false;


    }
    public void OnBeginDrag( PointerEventData eventData ){
        dragging=true;
    }

    public void OnDrag( PointerEventData eventData ){

        //locked==false
        if (Game_State.Instance.Get_In_Mini_Game()==false) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position=mousePosition;
            transform.position=new Vector3(transform.position.x, transform.position.y, -1); 
            sprite_renderer.sortingOrder=2;

        }
    }
    
    public void OnPointerDown( PointerEventData eventData ){

    }
 
    public void OnPointerUp( PointerEventData eventData ){
       if (dragging==false && Game_State.Instance.Get_In_Mini_Game()==false) {
            Game_State.Instance.Entered_Mini_Game();

            mini_game.SetActive(true);

            if (this.name=="GPU"){

            }

            if (this.name=="CPU"){
                mini_game.GetComponent<CPU_Game>().Start_Game();

            }

            if (this.name=="HDD"){

            }

            if (this.name=="FAN"){

            }

            if (this.name=="RAM"){

            }

        } 
    }
    
     public void OnDrop( PointerEventData eventData )
     {
     }
     public void OnPointerEnter( PointerEventData eventData )
     {
        if (Game_State.Instance.Get_In_Mini_Game()==false) {
            sprite_renderer.sprite = image_hovered;
            info_box.SetActive(true);
        }
     }
     public void OnPointerExit( PointerEventData eventData )
     {
        sprite_renderer.sprite = image_idle;
        info_box.SetActive(false);
     }

 }


