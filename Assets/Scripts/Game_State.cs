using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Game_State : MonoBehaviour
{
    // Start is called before the first frame update
    public static Game_State Instance;

    [SerializeField]
    private Desktop_Manager desktop_manager;

    [SerializeField]
    private GameObject On_Button;

    private bool game_started;
    private int money;

    private float CPU_penalty;
    private float GPU_penalty;
    private float RAM_penalty;
    private float storage_penalty;
    private float cooling_penalty;

    private bool in_mini_game;
    private bool in_shop;

    private double happiness;
    private int num_sleep_components;
    private int num_dead_components;

    private int num_CPU;
    private int num_GPU;
    private int num_RAM;
    private int num_Storage;
    private int num_Cooling;

    private bool game_over;



    void Awake()
    {
        Instance = this;

        Reset_Values();

    }

    public void Reset_Values(){
    
        game_started = false;
        money=0;
        happiness=100;
        in_mini_game=false;
        num_sleep_components=0;
        num_dead_components=0;
        game_over=false;
        num_CPU = 1;
        num_GPU = 1;
        num_RAM = 1;
        num_Storage = 1;
        num_Cooling = 1;
    }

    public void Remove_By_Percent(double percent){
        happiness *= (1-percent);
        happiness = Math.Round(happiness, 2);

    }

    public bool Get_Game_Over(){
        return game_over;
    }

    public void Add_Sleep_Component(){
        num_sleep_components++;

        //if game ends
        if (num_sleep_components>=2){
            game_over=true;
            On_Button.SetActive(true);
            Stop_Game();
        }
    }
    public void Remove_Sleep_Component(){
        num_sleep_components--;
    }

    public void Add_Happiness(double val){
        happiness += val;
        happiness = Math.Round(happiness, 2);
        if (happiness<0) happiness = 0;

    }

    public int Get_Happiness(){

        return ((int)happiness);

    }

    public void Entered_Mini_Game(){
        in_mini_game=true;
    }

    public void Exited_Mini_Game(){
        in_mini_game=false;
    }

    public bool Get_In_Mini_Game(){
        return in_mini_game;
    }

    public void Entered_Shop()
    {
        in_shop = true;
    }

    public void Exited_Shop()
    {
        in_shop = false;
    }

    public bool Get_In_Shop()
    {
        return in_shop;
    }

    // Update is called once 
    void Update()
    {
        //Debug.Log(happiness);
    }

    public bool Get_Game_Started(){
        return game_started;
    }
    public int Get_Money(){
        return money;
    }

    public void Start_Game(){
        game_started=true;
        game_over=false;
        CPU_penalty = 0;
        GPU_penalty = 0;
        RAM_penalty = 0;
        storage_penalty = 0;
        cooling_penalty = 0;
        desktop_manager.Start_Game();
    }
    public void Stop_Game(){
        game_started=false;
        desktop_manager.Stop_Game();
    }
    public void Add_Money(int add){
        money+=add;
    }



    public float Get_CPU_Penalty()
    {
        return CPU_penalty;
    }
    public void Add_CPU_Penalty(float cpu)
    {
        CPU_penalty += cpu;
    }

    public float Get_GPU_Penalty()
    {
        return GPU_penalty;
    }
    public void Add_GPU_Penalty(float gpu)
    {
        GPU_penalty += gpu;
    }

    public float Get_RAM_Penalty()
    {
        return RAM_penalty;
    }
    public void Add_RAM_Penalty(float ram)
    {
        RAM_penalty += ram;
    }

    public float Get_Storage_Penalty()
    {
        return storage_penalty;
    }
    public void Add_Storage_Penalty(float storage)
    {
        storage_penalty += storage;
    }

    public float Get_Cooling_Penalty()
    {
        return cooling_penalty;
    }
    public void Add_Cooling_Penalty(float cooling)
    {
        cooling_penalty += cooling;
    }

    public int Get_Num_CPUS(){
        return num_CPU;
    }
    public int Get_Num_GPUS(){
        return num_GPU;
    }
    public int Get_Num_RAM(){
        return num_RAM;
    }
    public int Get_Num_Storage(){
        return num_Storage;
    }
    public int Get_Num_Cooling(){
        return num_Cooling;
    }

// ------------

    public void Add_Num_CPUS(){
        num_CPU++;
    }
    public void Add_Num_GPUS(){
        num_GPU++;
    }
    public void Add_Num_RAM(){
        num_RAM++;
    }
    public void Add_Num_Storage(){
        num_Storage++;
    }
    public void Add_Num_Cooling(){
        num_Cooling++;
    }
}
