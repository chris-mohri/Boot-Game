using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Game_Controller : MonoBehaviour
{

    public PlayerControls controls;

    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerControls();
    }

    // Update is called once per frame
    void Update()
    {
        if (controls.Keyboard.Q.WasPressedThisFrame())
        {
            Debug.Log("q");
            if (!Game_State.Instance.Get_Game_Started())
                Game_State.Instance.Start_Game();
            else
                Game_State.Instance.Stop_Game();
        }

    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
}
