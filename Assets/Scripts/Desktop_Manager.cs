using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Desktop_Manager : MonoBehaviour
{
    public List<GameObject> all_programs; 
    public float spawn_rate;

    [SerializeField]
    private float spawn_interval_initial = 20;
    [SerializeField]
    private float spawn_interval_min = 3;
    [SerializeField]
    private float time_to_min = 120; // seconds

    [SerializeField]
    private Sprite image_normal;
    [SerializeField]
    private Sprite image_blue_screen;


    private IEnumerator spawner;
    //private float time_since_spawn;
    private bool game_started;
    private List<GameObject> spawned;
    private System.Random random;
    

    // Start is called before the first frame update
    void Start()
    {
        game_started = false;
        spawner = Spawn_Programs();
        random = new System.Random();
    }

    public void Enter_Blue_Screen(){
        this.gameObject.GetComponent<SpriteRenderer>().sprite=image_blue_screen;
    }

    // Update is called once per frame
    void Update()
    {
        if(game_started)
        {
            //time_since_spawn += Time.deltaTime;
            if(spawn_rate >= spawn_interval_min)
                spawn_rate -= Time.deltaTime * (spawn_interval_initial-spawn_interval_min)/time_to_min;
        }
        
    }

    IEnumerator Spawn_Programs()
    {
        yield return new WaitForSeconds(2);
        while(true)
        {
            GameObject next_prog = all_programs[random.Next(all_programs.Count)];
            Instantiate_Program(next_prog);

            yield return new WaitForSeconds(spawn_rate);
        }
    }

    public void Instantiate_Program(GameObject prog)
    {
        all_programs.Remove(prog);
        spawned.Add(prog);
        Vector3 bounds = prog.GetComponent<Renderer>().bounds.size;
        GameObject p = Instantiate(prog, Get_Random_Position(bounds.x, bounds.y), Quaternion.identity);
        Program_Data p_data = p.GetComponent<Program_Data>();
        AddPenalties(p_data);
        float alive_time = (float)random.NextDouble()*5 + 5;
        p_data.Begin_Depression(alive_time, this, prog);
        //Print_Penalties();   
    }

    public void Destroy_Program(GameObject prog, GameObject prefab)
    {
        spawned.Remove(prefab);
        all_programs.Add(prefab);
        RemovePenalties(prog.GetComponent<Program_Data>());
        Destroy(prog);
    }

    public void AddPenalties(Program_Data p_data)
    {
        Game_State.Instance.Add_CPU_Penalty(p_data.cpu_penalty);
        Game_State.Instance.Add_GPU_Penalty(p_data.gpu_penalty);
        Game_State.Instance.Add_RAM_Penalty(p_data.ram_penalty);
        Game_State.Instance.Add_Storage_Penalty(p_data.storage_penalty);
        Game_State.Instance.Add_Cooling_Penalty(p_data.cooling_penalty);
    }

    public void RemovePenalties(Program_Data p_data)
    {
        Game_State.Instance.Add_CPU_Penalty(-p_data.cpu_penalty);
        Game_State.Instance.Add_GPU_Penalty(-p_data.gpu_penalty);
        Game_State.Instance.Add_RAM_Penalty(-p_data.ram_penalty);
        Game_State.Instance.Add_Storage_Penalty(-p_data.storage_penalty);
        Game_State.Instance.Add_Cooling_Penalty(-p_data.cooling_penalty);
    }

    public Vector3 Get_Random_Position(float pre_w, float pre_h)
    {
        Vector3 position = new Vector3();
        float width = GetComponent<Renderer>().bounds.size.x;
        float left_bound = transform.position.x - (width/2);
        float right_bound = transform.position.x + (width/2);
        position.x = ((float)random.NextDouble() * ((right_bound - (pre_w / 2)) - (left_bound + (pre_w / 2)))) + (left_bound + (pre_w / 2));

        float height = GetComponent<Renderer>().bounds.size.y;
        float lower_bound = transform.position.y - (height/2);
        float upper_bound = transform.position.y + (height/2);
        position.y = ((float)random.NextDouble() * ((upper_bound - (pre_h / 2)) - (lower_bound + (pre_h / 2)))) + (lower_bound + (pre_h / 2));

        position.z = -.2f;

        return position;
    }

    public void Start_Game()
    {
        game_started = true;
        this.gameObject.GetComponent<SpriteRenderer>().sprite=image_normal;

        StartCoroutine(spawner);
        //time_since_spawn = 0;
        spawn_rate = spawn_interval_initial;
        spawned = new List<GameObject>();
    }

    public void Stop_Game()
    {
        Enter_Blue_Screen();
        game_started = false;
        StopAllCoroutines();
    }

    public void Print_Penalties()
    {
        Debug.Log("CPU: " + Game_State.Instance.Get_CPU_Penalty());
        Debug.Log("GPU: " + Game_State.Instance.Get_GPU_Penalty());
        Debug.Log("RAM: " + Game_State.Instance.Get_RAM_Penalty());
        Debug.Log("Storage: " + Game_State.Instance.Get_Storage_Penalty());
        Debug.Log("Cooling: " + Game_State.Instance.Get_Cooling_Penalty());
    }
}
