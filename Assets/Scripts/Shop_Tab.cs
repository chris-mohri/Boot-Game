using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shop_Tab : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        Shop_Controller parent = transform.parent.GetComponent<Shop_Controller>();

        if(!parent.is_sliding && !Game_State.Instance.Get_In_Mini_Game())
        {
            parent.Tab_Click();
        }
    }

    private void OnMouseEnter()
    {
        Game_State.Instance.Entered_Shop();
    }

    private void OnMouseExit()
    {
        transform.parent.GetComponent<Shop_Controller>().Unhover_Tab();
    }
}
