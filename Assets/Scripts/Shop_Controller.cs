using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Controller : MonoBehaviour
{
    private STATE shop_state;
    public bool is_sliding;
    public Shop_Item[] items;

    // Start is called before the first frame update
    void Start()
    {
        shop_state = STATE.Tab;
        is_sliding = false;
        /*
        for(int i=1; i<4; i++)
        {
            Debug.Log("Item_" + i);
            items[i - 1] = transform.Find("Items").Find("Item_" + i).gameObject.GetComponent<Shop_Item>();
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tab_Click()
    {
        Debug.Log(shop_state);
        StartCoroutine(Tab_Slide());
    }

    public void Icon_Click(int id)
    {
        if (!items[2].gameObject.activeSelf)
            items[2].transform.gameObject.SetActive(true);
        Debug.Log(shop_state);
        // update items
        for(int i=0; i<3; i++)
        {
            items[i].Update_Items(id);
            if(id == 3 && i == 2)
            {
                items[i].transform.gameObject.SetActive(false);
            }
        }

        if(shop_state == STATE.Icons)
        {
            shop_state = STATE.Full;
            StartCoroutine(Icon_Slide());
        }
    }

    
    IEnumerator Tab_Slide()
    {
        is_sliding = true;
        if(shop_state == STATE.Tab)
        {
            for(int i=0; i<18; i++)
            {
                transform.Translate(-.1f, 0, 0);
                yield return new WaitForSeconds(.01f);
            }
            Game_State.Instance.Entered_Shop();
            shop_state = STATE.Icons;
        }
        else if (shop_state == STATE.Icons)
        {
            for (int i = 0; i < 18; i++)
            {
                transform.Translate(.1f, 0, 0);
                yield return new WaitForSeconds(.01f);
            }
            shop_state = STATE.Tab;
            Game_State.Instance.Exited_Shop();
        }
        else
        {
            for (int i = 0; i < 30; i++)
            {
                transform.Translate(.205f, 0, 0);
                yield return new WaitForSeconds(.01f);
            }
            shop_state = STATE.Tab;
            Game_State.Instance.Exited_Shop();
        }
        is_sliding = false;
    }

    IEnumerator Icon_Slide()
    {
        //Debug.Log("shit");
        is_sliding = true;
        for (int i = 0; i < 29; i++)
        {
            transform.Translate(-.15f, 0, 0);
            yield return new WaitForSeconds(.01f);
        }

        is_sliding = false;
    }

    public void Close_Tab()
    {
        Debug.Log("fart");
        StartCoroutine(Close_Full());
    }

    public void Open_Tab()
    {
        StartCoroutine(Open_Full());
    }

    IEnumerator Close_Full()
    {
        for (int i = 0; i < 30; i++)
        {
            transform.Translate(.205f, 0, 0);
            yield return new WaitForSeconds(.01f);
        }
        shop_state = STATE.Tab;
        Game_State.Instance.Exited_Shop();
    }

    IEnumerator Open_Full()
    {
        for (int i = 0; i < 30; i++)
        {
            transform.Translate(-.205f, 0, 0);
            yield return new WaitForSeconds(.01f);
        }
        shop_state = STATE.Full;
        Game_State.Instance.Entered_Shop();
    }

    public void Unhover_Tab()
    {
        if(shop_state == STATE.Tab)
        {
            Game_State.Instance.Exited_Shop();
        }
    }



    private enum STATE
    {
        Tab,
        Icons,
        Full
    }
}
