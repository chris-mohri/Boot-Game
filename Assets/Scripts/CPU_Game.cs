using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU_Game : MonoBehaviour
{

    [SerializeField]
    private GameObject hardware;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Start_Game(){
        //Debug.Log("aaaa");

    }

    private void Succeed(){
        hardware.GetComponent<Hardware_Controller>().Reset();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
