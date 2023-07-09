using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Card_Controller : MonoBehaviour, IPointerUpHandler
{

    [SerializeField]
    private Sprite orange_pic;
    [SerializeField]
    private Sprite coconut_pic;
    [SerializeField]
    private Sprite banana_pic;
    [SerializeField]
    private Sprite kiwi_pic;
    [SerializeField]
    private Sprite blueberry_pic;
    [SerializeField]
    private Sprite strawberry_pic;
    [SerializeField]
    private Sprite cardback_pic;

    private Sprite this_card; //this card will be 1 of the above 6 fruit
    private int fruit;

    private bool can_click;


    public void Turn_On(int card){
        can_click=true;
        fruit=card;

        this.gameObject.SetActive(true);

        if (card==0)
            this_card = orange_pic;
        else if (card==1)
            this_card = coconut_pic;
        else if (card==2)
            this_card = banana_pic;
        else if (card==3)
            this_card = kiwi_pic;
        else if (card==4)
            this_card = blueberry_pic;
        else if (card==5)
            this_card = strawberry_pic;
        
    }
    //flips card up

    // flip card
    // startcoroutine(wait)
    // flip card

    public int Get_Fruit(){
        return fruit;
    }


    void OnMouseDown()
    {
        Debug.Log(fruit);
        Debug.Log("Max: "+GetComponentInParent<RAM_Game>().Get_At_Max());
        Debug.Log("can_click: "+can_click);
        if (can_click==true && GetComponentInParent<RAM_Game>().Get_At_Max()==false) {
            gameObject.GetComponent<SpriteRenderer>().sprite=this_card;
            GetComponentInParent<RAM_Game>().Add_Card_To_Hand(this.gameObject);
        }

    }

    public void OnPointerUp( PointerEventData eventData ){
        //
        //gameObject.GetComponent<SpriteRenderer>().sprite=this_card;
    }

    public void Face_Down(){
        gameObject.GetComponent<SpriteRenderer>().sprite=cardback_pic;

    }

    public void Freeze(){
        can_click=false;
    }

    void Awake(){
        can_click=true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
