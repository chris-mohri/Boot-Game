using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class File_Controller : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField]
    private Sprite image_idle;
    [SerializeField]
    private Sprite image_hovered;
    [SerializeField]
    private GameObject Trash_Object;

    private SpriteRenderer sprite_renderer;

    private Vector2 initial_DragXY; 


    void Awake(){
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        initial_DragXY  = gameObject.transform.position;

    }

    public void Turn_On(){
        this.gameObject.SetActive(true);
    }


    public void OnBeginDrag( PointerEventData eventData ){

    }

    public void OnDrag( PointerEventData eventData ){

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position=mousePosition;
        transform.position=new Vector3(transform.position.x, transform.position.y, -1); 
        sprite_renderer.sortingOrder=7;

    }

    public void OnEndDrag( PointerEventData eventData ){
        sprite_renderer.sortingOrder=6;
        transform.position=new Vector3(transform.position.x, transform.position.y, 0); 

        //check if colliding with trash
        if (this.GetComponent<BoxCollider2D>().IsTouching(Trash_Object.GetComponent<BoxCollider2D>())){
            sprite_renderer.sprite = image_idle;
            Die();
        }

    }

    public void Die(){
        this.gameObject.SetActive(false);
        this.gameObject.transform.position = initial_DragXY;
        GetComponentInParent<HDD_Game>().Decrement();
        
    }

    public void OnPointerEnter( PointerEventData eventData )
     {
        if (true) {
            sprite_renderer.sprite = image_hovered;
        }
     }
     public void OnPointerExit( PointerEventData eventData )
     {
        sprite_renderer.sprite = image_idle;
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
