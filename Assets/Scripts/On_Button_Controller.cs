using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class On_Button_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position=new Vector3(transform.position.x, transform.position.y, transform.position.z-0.1f); 

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
                Game_State.Instance.Reset_Values();
                SceneManager.LoadScene("SampleSceneChris");
            }
            //otherwise proceed as normal
            else
                Game_State.Instance.Start_Game();
        }
        else
            Game_State.Instance.Stop_Game();   
        this.gameObject.SetActive(false);     
    }

    void OnMouseEnter(){
        transform.Find("Shadow").gameObject.SetActive(true);
    }

    void OnMouseExit(){
        transform.Find("Shadow").gameObject.SetActive(false);
    }

    /*
            
            */
}
