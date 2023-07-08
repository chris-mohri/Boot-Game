using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Desktop_Manager : MonoBehaviour
{
    public Program_Data all_programs; 
    public float spawn_rate = 20;

    //[SerializeField]
    //private float spawn_interval_initial = 20;
    [SerializeField]
    private float spawn_interval_min = 3;
    [SerializeField]
    private float time_to_min = 120; // seconds

    private IEnumerator spawner;
    private float time_since_spawn;
    private bool game_over;
    

    // Start is called before the first frame update
    void Start()
    {
        game_over = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator Spawn_Program()
    {
        yield return new WaitForSeconds(2);
        while(true)
        {
            Debug.Log("Spawn");
            yield return new WaitForSeconds(spawn_rate);
        }
    }
}
