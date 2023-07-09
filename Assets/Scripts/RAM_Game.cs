using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAM_Game : MonoBehaviour
{
    [SerializeField]
    private GameObject hardware;

    private int num_remaining=0;
    private bool started;

    private GameObject first_card;

    private GameObject second_card;

    private bool At_Max=false;

    //2 card max
    public void Add_Card_To_Hand(GameObject card){
        if (first_card==null)
            first_card=card;
        else if (second_card==null){
            At_Max=true;
            second_card=card;

            //check if cards match
            
            if (first_card.GetComponent<Card_Controller>().Get_Fruit()==second_card.GetComponent<Card_Controller>().Get_Fruit()){
                first_card.GetComponent<Card_Controller>().Freeze();
                second_card.GetComponent<Card_Controller>().Freeze();
                first_card=null;
                second_card=null;
                At_Max=false;
                num_remaining--;
            }
            else{
                StartCoroutine(Not_Matched());
            }

            

        }
            

    }


    IEnumerator Not_Matched(){
        yield return new WaitForSeconds(.5f);
        Debug.Log(second_card);
        second_card.GetComponent<Card_Controller>().Face_Down();
        Debug.Log(first_card);
        yield return new WaitForSeconds(.2f);
        first_card.GetComponent<Card_Controller>().Face_Down();

        first_card=null;
        second_card=null;
        At_Max=false;
        

    }

    public bool Get_At_Max(){
        return At_Max;
    }



    // Start is called before the first frame update
    void Awake()
    {
        started=false;
        first_card=null;
        second_card=null;
    }

    public void Start_Game(){

        Wipe();//toggle all cards to inactive

        started=true;

        System.Random r = new System.Random();
        double percent = hardware.GetComponent<Hardware_Controller>().Get_Percent_Efficiency();

        int extra = (int) ((1-percent) * 4);
        
        int num_pairs = 2+extra;

        //DELETE LATER ============================================== >:(
        //num_pairs=r.Next(5)+2;
        //num_pairs = 6;

        //card list
        List<int> list = new List<int>();

        //fruit directory
        List<int> fruit_directory = new List<int>();

        //adds 0 through 6
        for (int i = 0; i<7; i++){
            fruit_directory.Add(i);
        }// 0       1       2       3 ...
        //orange, coconut, banana, kiwi, blueberry, strawberry, cardback

        //adds dummy values for num of cards
        for (int i = 0; i<num_pairs*2; i++){
            list.Add(-1);
        }

        int fruit;

        int first_card;
        int second_card;

        
        //for number of pairs of cards
        for(int i =0; i<num_pairs; i++){ 
            //choose fruit

            while(true){
                fruit = r.Next(6); //0 through 5: orange, coconut, banana, kiwi, blueberry, strawberry
                //find a unique value // (+) means it is unused
                if (fruit_directory[fruit]!=-1) break;
            
            }
            
            fruit_directory[fruit]=-1;//fruit is now taken


            //fruit = a unique fruit


            //now find 2 unique, non taken cards
            
            while(true){
                first_card=r.Next(num_pairs*2);
                if (list[first_card]==-1) break;
            }
            list[first_card]=first_card;

            while(true){
                second_card=r.Next(num_pairs*2);
                if (list[second_card]==-1) break;
            }
            list[second_card]=second_card;


            //set the cards

            transform.Find("card-"+(first_card+1)).gameObject.GetComponent<Card_Controller>().Turn_On(fruit);
            transform.Find("card-"+(second_card+1)).gameObject.GetComponent<Card_Controller>().Turn_On(fruit);

        }

        num_remaining=num_pairs;
        

    }

    private void End(){
        started=false;
        first_card=null;
        second_card=null;
        this.gameObject.SetActive(false);
        
        Wipe();

    }

    private void Wipe(){
        for (int i = 1; i<13; i++){
            transform.Find("card-"+i).gameObject.GetComponent<Card_Controller>().Face_Down();
            transform.Find("card-"+i).gameObject.SetActive(false);
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
        if (started==true){
            if (num_remaining==0){
                Succeed();
            }
        }
    }
}
