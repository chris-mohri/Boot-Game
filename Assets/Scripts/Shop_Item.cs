using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shop_Item : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IDragHandler
{
    //public GameObject[] prefabs;
    public Sprite[] sprites;
    public float[] prices;
    private SpriteRenderer sprite_renderer;

    private int id;
    private float current_price;
    private GameObject current_prefab;
    private TextMeshPro tmp;
    private Vector3 initial_position;

    // Start is called before the first frame update
    void Start()
    {
        initial_position = transform.position;
        sprite_renderer = this.GetComponent<SpriteRenderer>();
        tmp = transform.Find("Text").GetComponent<TextMeshPro>();
        id = 0;
        Update_Items(id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Update_Items(int id)
    {
        sprite_renderer.sprite = sprites[id];
        //current_prefab = prefabs[id];
        current_price = prices[id];
        tmp.text = "$" + current_price;
    }


    public int Get_ID()
    {
        return id;
    }

    public void Set_ID(int i)
    {
        id = i;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        /*
        dragging = true;
        initial_DragXY = gameObject.transform.position;
        */
    }

    public void OnDrag(PointerEventData eventData)
    {
        /*
        //locked==false
        if (Game_State.Instance.Get_In_Mini_Game() == false && !Game_State.Instance.Get_In_Shop())
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            sprite_renderer.sortingOrder = 2;
            //Get_Restricted_Position();
        }
        */
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        /*
        sprite_renderer.sortingOrder = 1;

        //if collided
        if (num_collisions > 0)
        {
            this.gameObject.transform.position = initial_DragXY;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        dragging = false;
        num_collisions = 0;
        */

    }
}
