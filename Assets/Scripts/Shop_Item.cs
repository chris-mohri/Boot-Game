using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shop_Item : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IDragHandler
{
    public GameObject[] prefabs;
    public Sprite[] sprites;
    public Sprite[] dark_sprites;
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
        Vector2 sprite_size = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = sprite_size;
        //gameObject.GetComponent<BoxCollider2D>().offset = new Vector2((sprite_size.x / 2), 0);
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
            initial_position = transform.localPosition;
            GetComponentInChildren<TextMeshPro>().gameObject.SetActive(false);
            SpriteRenderer sr = current_prefab.transform.GetComponentInChildren<SpriteRenderer>(false);
            sprite_renderer.sprite = sr.sprite;
            transform.localScale /= 3.7f;
            Vector2 sprite_size = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
            gameObject.GetComponent<BoxCollider2D>().size = sprite_size;
            transform.parent.parent.GetComponent<Shop_Controller>().Close_Tab();
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
                transform.parent.parent.GetComponent<Shop_Controller>().Open_Tab();
            }
            else
            {
                GameObject fab = Instantiate(current_prefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                fab.transform.parent = transform.parent.parent.parent;
            }
            sprite_renderer.sprite = sprites[id];
            transform.localScale *= 3.7f;
            Vector2 sprite_size = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
            gameObject.GetComponent<BoxCollider2D>().size = sprite_size;
            this.gameObject.transform.localPosition = initial_position;
            GetComponentInChildren<TextMeshPro>().gameObject.SetActive(true);

            dragging = false;
            num_collisions = 0;
        }

    }
}
