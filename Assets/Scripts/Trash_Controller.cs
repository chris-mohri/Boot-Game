using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class Trash_Controller : MonoBehaviour, IPointerUpHandler
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerUp( PointerEventData eventData ){
        //GetComponentInParent<RAM_Game>().Check_Garbage();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
