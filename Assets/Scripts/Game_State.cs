using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_State : MonoBehaviour
{
    // Start is called before the first frame update
    public static Game_State Instance;

    [SerializeField]
    private Desktop_Manager desktop_manager;
    
    private bool game_started;
    private int money;

    void Awake()
    {
        Instance = this;

        game_started = false;
        money=0;
    }


    // Update is called once 
    void Update()
    {
        
    }

    public bool Get_Game_Started(){
        return game_started;
    }
    public int Get_Money(){
        return money;
    }

    public void Start_Game(){
        game_started=true;
        desktop_manager.Start_Game();
    }
    public void Stop_Game(){
        game_started=false;
        desktop_manager.Stop_Game();
    }
    public void Add_Money(int add){
        money+=add;
    }
}
