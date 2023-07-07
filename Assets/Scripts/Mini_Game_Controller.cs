using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Game_Controller : MonoBehaviour
{

    public PlayerControls controls;

    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
    }

    // Update is called once per frame
    void Update()
    {
        if (controls.Keyboard.Q.ReadValue<float>()==1){
            Debug.Log("q");
        }
    }
}
