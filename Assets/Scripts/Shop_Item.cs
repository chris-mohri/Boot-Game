using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shop_Item : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IDragHandler
{
    public GameObject[] prefabs;
    public Sprite[] sprites;
    public float[] prices;
    private SpriteRenderer sprite_renderer;

    private int id;
    private float current_price;
    private GameObject current_prefab;
    private TextMeshPro tmp;
    private Vector3 initial_position;
    private bool dragging;
    private int num_collisions;
    private bool can_afford;

    // Start is called before the first frame update
    void Start()
    {
        initial_position = transform.position;
        sprite_renderer = this.GetComponent<SpriteRenderer>();
        tmp = transform.Find("Text").GetComponent<TextMeshPro>();
        id = 0;
        dragging = false;
        num_collisions = 0;
        Update_Items(id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Update_Items(int id)
    {
        sprite_renderer.sprite = sprites[id];
        current_prefab = prefabs[id];
        current_price = prices[id];
        tmp.text = "$" + current_price;
        can_afford = current_price <= Game_State.Instance.Get_Happiness();
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
        //num_collisions = 0;
        if (can_afford)
        {
            dragging = true;
            initial_position = transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (dragging && col.gameObject.tag=="Hardware")
        {
            num_collisions++;
            Debug.Log("entered: " + col.gameObject.name + " : " + num_collisions);
        }
        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (dragging && col.gameObject.tag=="Hardware")
        {
            num_collisions--;
            if (num_collisions<0) num_collisions=0;
            Debug.Log("exited: " + col.gameObject.name+ " : " + num_collisions);

        }
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (can_afford)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            sprite_renderer.sortingOrder = 12;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (can_afford)
        {
            sprite_renderer.sortingOrder = 11;

            //if collided
            if (num_collisions > 0)
            {
                this.gameObject.transform.position = initial_position;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
            dragging = false;
            num_collisions = 0;
        }

    }
}
