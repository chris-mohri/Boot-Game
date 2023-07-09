using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Icon : MonoBehaviour
{
    public int id;

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
        Debug.Log("Icon");
        Shop_Controller parent = transform.parent.GetComponent<Shop_Controller>();

        if (!parent.is_sliding)
        {
            parent.Icon_Click(id);
        }
    }

    private void OnMouseEnter()
    {
        //Game_State.Instance.Entered_Shop();
    }

    private void OnMouseExit()
    {
        //Game_State.Instance.Exited_Shop();
    }
}
