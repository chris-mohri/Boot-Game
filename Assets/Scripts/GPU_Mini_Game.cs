using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GPU_Mini_Game : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;
    private SpriteRenderer sprite_renderer;
    private Sprite current_sprite;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        correct_sequence = new List<int>();
        input_sequence = new List<int>();
        Generate_Sequence(5);
        StartCoroutine(Play_Sequence());
    }



    void Generate_Sequence(int length)
    {
        System.Random random = new System.Random();
        for(int i=0; i<length; i++)
        {
            //Debug.Log("Pooopy");
            correct_sequence.Add(random.Next(6));
        }
    }


    IEnumerator Play_Sequence()
    {
        yield return new WaitForSeconds(1);
        for(int i=0; i<correct_sequence.Count; i++)
        {
            sprite_renderer.sprite = sprites[correct_sequence[i]];
            yield return new WaitForSeconds(1f);
        }
        sprite_renderer.sprite = current_sprite;
    }

    void Check_Sequence()
    {
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

    }

    void Win()
    {

    }
    
    public void On_Button_Down(int id)
    {
        sprite_renderer.sprite = sprites[id];
        input_sequence.Add(id);

        Check_Sequence();
    }

    public void On_Button_Up()
    {
        sprite_renderer.sprite = current_sprite;
    }
}
