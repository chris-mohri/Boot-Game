using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Pin_Controller : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnPointerDown(PointerEventData data){
        GetComponentInParent<CPU_Game>().Decrement();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
