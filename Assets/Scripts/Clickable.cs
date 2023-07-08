using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    [SerializeField]
    int id;

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
        Debug.Log("Print");
        GPU_Mini_Game parent = GetComponentInParent<GPU_Mini_Game>();
        parent.On_Button_Down(id);
    }

    private void OnMouseUp()
    {
        GPU_Mini_Game parent = GetComponentInParent<GPU_Mini_Game>();
        parent.On_Button_Up();
    }
}
