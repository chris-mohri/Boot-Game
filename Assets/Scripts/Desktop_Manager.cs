using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Desktop_Manager : MonoBehaviour
{
    public Program_Data all_programs; 
    public float spawn_rate;

    [SerializeField]
    private float spawn_interval_initial = 20;
    [SerializeField]
    private float spawn_interval_min = 3;
    [SerializeField]
    private float time_to_min = 120; // seconds

    private IEnumerator spawner;
    private float time_since_spawn;
    private bool game_started;
    

    // Start is called before the first frame update
    void Start()
    {
        game_started = false;
        spawner = Spawn_Programs();
    }

    // Update is called once per frame
    void Update()
    {
        if(game_started)
        {
            time_since_spawn += Time.deltaTime;
            spawn_rate -= Time.deltaTime * (spawn_interval_initial-spawn_interval_min)/time_to_min;
        }
        
    }

    IEnumerator Spawn_Programs()
    {
        yield return new WaitForSeconds(2);
        while(true)
        {
            Debug.Log("Spawn: " + time_since_spawn);
            yield return new WaitForSeconds(spawn_rate);
        }
    }

    public void Start_Game()
    {
        game_started = true;
        StartCoroutine(spawner);
        time_since_spawn = 0;
        spawn_rate = spawn_interval_initial;
    }

    public void Stop_Game()
    {
        Debug.Log("Stop");
        game_started = false;
        StopAllCoroutines();
    }
}
