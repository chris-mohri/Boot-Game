using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Happy_Controller : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text=Game_State.Instance.Get_Happiness().ToString();
    }
}
