using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{
    public bool isMovingLane = false;
    public int numLane = 0;
    public float speed;
    public byte velTurn;
    private float x0, x1, x2, xless1, xless2;
    private GameManager refGM;
    private Animator anim;

    public KeyCode moveR, moveL;

    public bool left, right;

    

    float offsetValue = 0.2f;

    public GameObject offSetValueText;

    float fpsCounter = 0;
    float frequency = 0.5f;

    public int FramesPerSec { get; protected set; }

    private void Start()
    {
        StartCoroutine(FPS());
        refGM = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();

        // Take x values from the public transform in Game Manager
        x0 = refGM.lane_0.localPosition.x;
        x1 = refGM.lane_1.localPosition.x;
        x2 = refGM.lane_2.localPosition.x;
        xless1 = refGM.lane_less_1.localPosition.x;
        xless2 = refGM.lane_less_2.localPosition.x;
    }

    private IEnumerator FPS()
    {
        for (;;)
        {
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;

            // Display it
            FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
            offSetValueText.GetComponent<Text>().text = FramesPerSec.ToString() + " fps";
        }
    }


    //public void TurnLeft()
    //{
    //    if (!isMovingLane && numLane > -2)
    //    {
    //        numLane--;
    //        anim.SetBool("isTurnLeft", true);
    //        delCurrentLane(numLane);
    //        ChangeLane(numLane);
    //        StartCoroutine(SetFalseBool("isTurnLeft"));
    //    }
    //}

    //public void TurnRight()
    //{
    //    if (!isMovingLane && numLane < 2)
    //    {
    //        anim.SetBool("isTurnRight", true);
    //        numLane++;
    //        delCurrentLane(numLane);
    //        ChangeLane(numLane);
    //        StartCoroutine(SetFalseBool("isTurnRight"));
    //    }
    //}


    public void SetLeftBool(bool value)
    {
        left = value;
    }

    public void SetRightBool(bool value)
    {
        right = value;
    }

    private void Update()
    {
        // Move forward the player
        transform.Translate(transform.forward * (-speed) * Time.deltaTime);
        //float movex = 0;
        float x = Input.acceleration.x;
        Debug.Log("X = " + x);

        fpsCounter += (Time.deltaTime - fpsCounter) * .1f;
        //offSetValueText.GetComponent<Text>().text = "Value: " + fpsCounter.ToString();

        

        //if (Mathf.Abs(x) > offsetValue)
        //{
        //    movex = Mathf.Sign(x) * speed;
        //    transform.Translate(movex, 0, 0);
        //}

        if (x < -offsetValue)
        {
            right = false;
            left = true;
        }
        else if (x > offsetValue)
        {
            left = false;
            right = true;
        }
        else
        {
            left = false;
            right = false;
        }


        if (Input.GetKey(moveL)){
            left = true;
        } 

        if (Input.GetKeyUp(moveL))
        {
            left = false;
        }

        if (Input.GetKey(moveR))
        {
            right = true;
        }
        
        if(Input.GetKeyUp(moveR))
        {
            right = false;
        }


        // If i move left
        if (left && !isMovingLane && numLane > -2)
        {
            numLane--;
            anim.SetBool("isTurnLeft", true);
            //delCurrentLane(numLane);
            ChangeLane(numLane);
            StartCoroutine(SetFalseBool("isTurnLeft"));
            
        }

        // If i move right
        if (right && !isMovingLane && numLane < 2)
        {
            anim.SetBool("isTurnRight", true);
            numLane++;
            //delCurrentLane(numLane);
            ChangeLane(numLane);
            StartCoroutine(SetFalseBool("isTurnRight"));
            
        }
    }

    // Method called for switch lane that call a coroutine
    private void ChangeLane(int _numLane)
    {
        switch (_numLane)
        {
            case 0:
                StartCoroutine(SwitchLaneCO(x0));
                break;
            case 1:
                StartCoroutine(SwitchLaneCO(x1));
                break;
            case 2:
                StartCoroutine(SwitchLaneCO(x2));
                break;
            case -1:
                StartCoroutine(SwitchLaneCO(xless1));
                break;
            case -2:
                StartCoroutine(SwitchLaneCO(xless2));
                break;
        }
    }

    // Coroutine that set bool true, change the lane, and set bool false
    private IEnumerator SwitchLaneCO(float _x)
    {
        isMovingLane = true;
        while (this.transform.position.x != _x)
        {
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(_x, this.transform.position.y, this.transform.position.z), Time.deltaTime * velTurn);
        }
        isMovingLane = false;
    }

    // Coroutine for set false bool in animator with a little delay
    private IEnumerator SetFalseBool(string _bool)
    {
        yield return null;
        anim.SetBool(_bool, false);
    }

    public void IncreaseOffsetValue()
    {
        offsetValue+=0.1f;
    }

    public void DecreaseOffsetValue()
    {
        offsetValue-=0.1f;
    }

}