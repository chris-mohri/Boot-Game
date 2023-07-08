using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fan_Mini_Game : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Hardware_Controller hardware_controller;

    private TextMeshPro text;
    private bool clockwise;
    private int revolutions;
    private float max_angle;
    float last_angle;
    private float current_angle;
    private float start_angle;
    private bool dragging;
    private Transform fan;
    private Transform end;

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
        fan = transform.Find("Fan");
        end = transform.Find("End");
        text = GetComponentInChildren<TextMeshPro>();
        System.Random r = new System.Random();
        revolutions = Get_Difficulty();
        max_angle = revolutions * 360;
        clockwise = r.Next(2) == 1;
        current_angle = 0;
        dragging = false;
        text.text = "Rotations: " + revolutions;
        if (clockwise)
        {
            max_angle *= -1;
            fan.GetComponent<SpriteRenderer>().flipY = true;
        }
        Debug.Log(revolutions);
        Debug.Log(max_angle);
        Debug.Log(clockwise);
        //StartCoroutine(Start_Routine());
    }


    /*
    IEnumerator Start_Routine()
    {
        fan = transform.Find("Fan");
        yield return new WaitForSeconds(.1f);
    }
    */


    private int Get_Difficulty()
    {
        double percent = hardware_controller.Get_Percent_Efficiency();
        if (percent > .85)
        {
            return 3;
        }
        else if (percent > .7)
        {
            return 4;
        }
        else if (percent > .55)
        {
            return 5;
        }
        else if (percent > .4)
        {
            return 6;
        }
        else if (percent > .25)
        {
            return 7;
        }
        return 7;
    }

    void Win()
    {
        current_angle = 0;
        if(clockwise)
        {
            fan.GetComponent<SpriteRenderer>().flipY = false;
        }
        hardware_controller.Reset();
        Debug.Log("Win");
    }


    public void Check_Win()
    {
        float diff = max_angle - current_angle;
        if((clockwise && diff > -180) || (!clockwise && diff < 180))
        {
            //Win();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        start_angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Debug.Log(start_angle);
        start_angle -= Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
        Debug.Log(start_angle);
        last_angle = start_angle;

        /*
        if(!end.gameObject.activeSelf)
        {
            end.rotation = Quaternion.AngleAxis(start_angle, Vector3.forward);
            end.gameObject.SetActive(true);
        }
        */
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - start_angle;
        float delta = angle - last_angle;
        if(delta <  -200)
        {
            delta += 360;
        } else if(delta > 200)
        {
            delta -= 360;
        }
        current_angle += delta;
        //Debug.Log(current_angle);
        last_angle = angle;
        fan.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if(clockwise)
        {
            text.text = "Rotations: " + (revolutions - (int)(current_angle / -360));
            if (current_angle < max_angle)
            {
                Win();
            }
        } else
        {
            text.text = "Rotations: " + (revolutions - (int)(current_angle / 360));
            if (current_angle > max_angle)
            {
                Win();
            }
        }

    }
}
