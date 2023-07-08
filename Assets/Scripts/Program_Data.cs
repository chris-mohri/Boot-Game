using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program_Data : MonoBehaviour
{
    public float alive_time;
    public Desktop_Manager desktop_manager;
    public GameObject prefab;

    public float cpu_penalty;
    public float gpu_penalty;
    public float ram_penalty;
    public float cooling_penalty;
    public float storage_penalty;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Begin_Depression(float a_time, Desktop_Manager d_man, GameObject pre)
    {
        alive_time = a_time;
        desktop_manager = d_man;
        prefab = pre;
        StartCoroutine(Suicide());
    }

    IEnumerator Suicide()
    {
        yield return new WaitForSeconds(alive_time);
        desktop_manager.Destroy_Program(transform.gameObject, prefab);
    }
}
