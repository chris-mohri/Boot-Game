using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU_Game : MonoBehaviour
{

    [SerializeField]
    private GameObject hardware;

    private int num_remaining=0;
    private bool started;
    // Start is called before the first frame update
    void Start()
    {
        started=false;
       // num_remaining=0;
    }

    public void Start_Game(){
        started=true;
        System.Random r = new System.Random();
        double percent = hardware.GetComponent<Hardware_Controller>().Get_Percent_Efficiency();

        int extra = (int) ((1-percent) * 10);
        
        int num_pairs = 3+extra;

        int pair;

        //turns on the pair
        List<int> list = new List<int>();
        for (int i = 0; i<13; i++){
            list.Add(-1);
        }

        bool new_num=false;
        for(int i =0; i<num_pairs; i++){ 
            new_num=false;
            while (!new_num){
                pair = r.Next(13);
                if (list[pair]==-1) {
                    list[pair]=pair;
                    new_num=true;
                }

            }
        }

        for (int i = 0; i<13; i++){
            if (list[i]!=-1){
                transform.Find("broke-"+(list[i]+1)).gameObject.SetActive(true);
                transform.Find("pin-"+(list[i]+1)).gameObject.SetActive(true);


            }
        }

        num_remaining=num_pairs;

    }

    private void End(){
        started=false;
        this.gameObject.SetActive(false);
        
        for (int i = 0; i<13; i++){
            transform.Find("broke-"+(i+1)).gameObject.SetActive(false);
            transform.Find("pin-"+(i+1)).gameObject.SetActive(false);

        }

    }

    public void Decrement(){
        num_remaining--;

    }

    private void Succeed(){
        hardware.GetComponent<Hardware_Controller>().Reset();
        End();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(num_remaining);
        if (started==true){
            if (num_remaining==0){
                Succeed();
            }
        }
    }
}
