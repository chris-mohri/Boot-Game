using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPU_Mini_Game : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;

    // 0 - Left
    // 1 - Up
    // 2 - Right
    // 3 - Down
    // 4 - A
    // 5 - B

    private List<int> input_sequence;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Generate_Sequence(5);
    }

    void Generate_Sequence(int length)
    {
        System.Random random = new System.Random();
        for(int i=0; i<length; i++)
        {
            input_sequence.Add(random.Next(6));
        }
    }
}
