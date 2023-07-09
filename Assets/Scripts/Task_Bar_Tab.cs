using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Bar_Button : MonoBehaviour
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
}
