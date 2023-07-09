using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GPU_Mini_Game : MonoBehaviour
{
    [SerializeField]
    private Hardware_Controller hardware_controller;
    [SerializeField]
    private Sprite[] sprites;
    //[SerializeField]
    //private int min_difficulty= 2;
    //[SerializeField]
    //private int max_difficulty = 6;

    private SpriteRenderer sprite_renderer;
    private Sprite current_sprite;
    private bool sequence_playing;

    // 0 - Left
    // 1 - Up
    // 2 - Right
    // 3 - Down
    // 4 - A
    // 5 - B

    private List<int> correct_sequence;
    private List<int> input_sequence;

    // Start is called before the first frame update
    void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        current_sprite = sprite_renderer.sprite;
        sequence_playing = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        sequence_playing = true;
        correct_sequence = new List<int>();
        input_sequence = new List<int>();
        Generate_Sequence(Get_Difficulty());
        StartCoroutine(Play_Sequence());
    }


    private int Get_Difficulty()
    {
        double percent = hardware_controller.Get_Percent_Efficiency();
        if (percent > .85)
        {
            return 2;
        }
        else if (percent > .7)
        {
            return 3;
        }
        else if (percent > .55)
        {
            return 4;
        }
        else if (percent > .4)
        {
            return 5;
        }
        else if (percent > .25)
        {
            return 6;
        }
        return 6;
    }


    void Generate_Sequence(int length)
    {
        string test = "";
        System.Random random = new System.Random();
        for(int i=0; i<length; i++)
        {
            int next = random.Next(6);
            test += next + " ";
            correct_sequence.Add(next);
        }
        Debug.Log(test);
    }


    IEnumerator Play_Sequence()
    {
        sequence_playing = true;
        yield return new WaitForSeconds(1);
        for(int i=0; i<correct_sequence.Count; i++)
        {
            sprite_renderer.sprite = sprites[correct_sequence[i]];
            yield return new WaitForSeconds(.5f);
            sprite_renderer.sprite = current_sprite;
            yield return new WaitForSeconds(.1f);
        }
        sequence_playing = false;
        //sprite_renderer.sprite = current_sprite;
    }

    void Check_Sequence()
    {
        if (input_sequence.Count > correct_sequence.Count)
        {
            Fail();
            return;
        }
        
        bool fail = false;
        for(int i=0; i<input_sequence.Count; i++)
        {
            fail = input_sequence[i] != correct_sequence[i];
        }

        if(fail)
        {
            Fail();
        }
        else if(input_sequence.Count == correct_sequence.Count)
        {
            Win();
        }
    }

    void Fail()
    {
        sequence_playing = true;
        Debug.Log("Fail");
        input_sequence = new List<int>();
        StartCoroutine(Screen_Flash());
    }

    void Win()
    {
        // reset to full, close
        sprite_renderer.sprite = current_sprite;
        hardware_controller.Reset();
        Debug.Log("Win");
    }
    
    public void On_Button_Down(int id)
    {
        if (!sequence_playing)
        {
            sprite_renderer.sprite = sprites[id];
            input_sequence.Add(id);
            Debug.Log(id);

            Check_Sequence();
        }
    }

    public void On_Button_Up()
    {
        if(!sequence_playing)
            sprite_renderer.sprite = current_sprite;
    }

    IEnumerator Screen_Flash()
    {
        GameObject red_screen = transform.Find("Red Screen").gameObject;
        for (int i = 0; i < 3; i++)
        {
            red_screen.SetActive(true);
            yield return new WaitForSeconds(.25f);
            red_screen.SetActive(false);
            yield return new WaitForSeconds(.25f);
        }
        sprite_renderer.sprite = current_sprite;
        StartCoroutine(Play_Sequence());
    }
}
