using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class On_Button_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //turns on the game
    void OnMouseUp(){
        if (!Game_State.Instance.Get_Game_Started()){

            //player had already lost and blue screen
            if (Game_State.Instance.Get_Game_Over()==true){
                //restart scene
            }
            //otherwise proceed as normal
            else
                Game_State.Instance.Start_Game();
        }
        else
            Game_State.Instance.Stop_Game();   
        this.gameObject.SetActive(false);     
    }

    /*
            
            */
}
