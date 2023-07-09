using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDD_Game : MonoBehaviour
{

    [SerializeField]
    private GameObject hardware;

    private int num_remaining=0;
    private bool started;

    /*
    private GameObject file=null;

    public void Add_File(GameObject new_File){
        file=new_file;
    }*/
   
    public void Start_Game(){
        Wipe();

        started=true;

        System.Random r = new System.Random();
        double percent = hardware.GetComponent<Hardware_Controller>().Get_Percent_Efficiency();

        int extra = (int) ((1-percent) * 6);
        
        int num_files = 4+extra;

        //garbo list
        List<int> list = new List<int>();

        //adds 0 through 6
        for (int i = 0; i<16; i++){
            list.Add(-1);
        }// 0       1       2       3 ...
        
        int fil;
        for (int i = 0; i<num_files; i++){
            while(true){
                fil = r.Next(16);
                if (list[fil]==-1)
                    break;
            }
            list[fil]=fil;

            transform.Find("File-"+(fil+1)).gameObject.GetComponent<File_Controller>().Turn_On();
        }


            //set the cards

        //transform.Find("card-"+(first_card+1)).gameObject.GetComponent<Card_Controller>().Turn_On(fruit);

        num_remaining=num_files;
        
    }

    private void Wipe(){
        for (int i = 1; i<17; i++){
            transform.Find("File-"+i).gameObject.SetActive(false);
        }
    }

    private void End(){
        started=false;
        this.gameObject.SetActive(false);

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
        if (started==true){
            if (num_remaining==0){
                Succeed();
            }
        }
    }
}
