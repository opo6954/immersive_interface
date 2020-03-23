using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class motionRecorder : MonoBehaviour
{
    public Transform Lever;
    public Transform Wheel;
    public Transform Joystick;
    public Transform Hand;
    public TextMesh Button_UI;

    public Transform[] buttons;


    public int timeIdx = 0;

    public List<int> timeIdxList;
    public List<float> timevalueList;

    public List<int> buttonPressedIdxList;

    public List<Vector3> levPosList;
    public List<Vector3> levRotList;

    public List<float> wheelRotYList;

    public List<Vector3> joyPosList;
    public List<Vector3> joyRotList;

    public List<Vector3> handPosList;
    public List<Vector3> handRotList;

    public List<Vector3> originButtonPosList;

    float buttonYPushedPos = 0;

    // Start is called before the first frame update
    void Start()
    {

        levPosList = new List<Vector3>();
        levRotList = new List<Vector3>();
        wheelRotYList = new List<float>();
        joyPosList = new List<Vector3>();
        joyRotList = new List<Vector3>();
        buttonPressedIdxList = new List<int>();

        handPosList = new List<Vector3>();
        handRotList = new List<Vector3>();


        timeIdxList = new List<int>();
        timevalueList = new List<float>();

        originButtonPosList = new List<Vector3>();

        for(int i=0; i<6; i++)
        {
            originButtonPosList.Add(buttons[i].position);
        }

        buttonYPushedPos = originButtonPosList[0].y * 0.8f;


    }

    void saveLogging()
    {
        string fileName = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

        fileName = "presentorMotion_" + fileName + ".csv";

        FileStream f = new FileStream(fileName, FileMode.Append, FileAccess.Write);

        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);

        writer.WriteLine("Time slot, time value, levPos, levRot, buttonIdx, wheelRoty, joyPos, joyRot, handPos, handRot");

        for (int i=0; i<timeIdxList.Count; i++)
        {
            string singleLine = timeIdxList[i].ToString() + "," + timevalueList[i].ToString() + "," + levPosList[i].ToString("F4") + "," + levRotList[i].ToString("F4") + "," + buttonPressedIdxList[i].ToString() + "," + wheelRotYList[i].ToString("F4") + "," + joyPosList[i].ToString("F4") + "," + joyRotList[i].ToString("F4") + "," + handPosList[i].ToString("F4") + "," + handRotList[i].ToString("F4");


            writer.WriteLine(singleLine);
        }

        writer.Close();

        Debug.Log("Save Presentor's motion Done...");
    }

    void resetButton()
    {
        for(int i=0; i<buttons.Length; i++)
        {
            buttons[i].position = originButtonPosList[i];
        }
    }

    void loggingOneLine(int timeIdx, float timevalue)
    {
        Vector3 levPos = Lever.position;
        Vector3 levRot = Lever.rotation.eulerAngles;

        Vector3 wheelRot = Wheel.rotation.eulerAngles;

        Vector3 joyPos = Joystick.position;
        Vector3 joyRot = Joystick.rotation.eulerAngles;

        int buttonPressedIdx = -1;

        string button_trimmed = "";

        if (Button_UI.text.Length > 0)
             button_trimmed = Button_UI.text.Split(':')[1].Trim();

        Vector3 pressedButtonPos = new Vector3();

        switch (button_trimmed)
        {
            case "button_purple":
                buttonPressedIdx = 0;
                resetButton();

                pressedButtonPos = new Vector3(originButtonPosList[buttonPressedIdx].x, buttonYPushedPos, originButtonPosList[buttonPressedIdx].z);
                
                buttons[buttonPressedIdx].position = pressedButtonPos;
                
                break;
            case "button_green":
                buttonPressedIdx = 1;
                resetButton();
                pressedButtonPos = new Vector3(originButtonPosList[buttonPressedIdx].x, buttonYPushedPos, originButtonPosList[buttonPressedIdx].z);
                buttons[buttonPressedIdx].position = pressedButtonPos;

                break;
            case "button_red":
                buttonPressedIdx = 2;
                resetButton();
                pressedButtonPos = new Vector3(originButtonPosList[buttonPressedIdx].x, buttonYPushedPos, originButtonPosList[buttonPressedIdx].z);
                buttons[buttonPressedIdx].position = pressedButtonPos;
                break;
            case "button_orange":
                buttonPressedIdx = 3;
                resetButton();
                pressedButtonPos = new Vector3(originButtonPosList[buttonPressedIdx].x, buttonYPushedPos, originButtonPosList[buttonPressedIdx].z);
                buttons[buttonPressedIdx].position = pressedButtonPos;
                break;
            case "button_yellow":
                buttonPressedIdx = 4;
                resetButton();
                pressedButtonPos = new Vector3(originButtonPosList[buttonPressedIdx].x, buttonYPushedPos, originButtonPosList[buttonPressedIdx].z);
                buttons[buttonPressedIdx].position = pressedButtonPos;
                break;
            case "button_blue":
                buttonPressedIdx = 5;
                resetButton();
                pressedButtonPos = new Vector3(originButtonPosList[buttonPressedIdx].x, buttonYPushedPos, originButtonPosList[buttonPressedIdx].z);
                buttons[buttonPressedIdx].position = pressedButtonPos;
                break;
            default:
                buttonPressedIdx = -1;
                break;
        }


        levPosList.Add(levPos);
        levRotList.Add(levRot);
        buttonPressedIdxList.Add(buttonPressedIdx);
        wheelRotYList.Add(wheelRot.y);
        joyPosList.Add(joyPos);
        joyRotList.Add(joyRot);

        handPosList.Add(Hand.position);
        handRotList.Add(Hand.rotation.eulerAngles);

        timeIdxList.Add(timeIdx);
        timevalueList.Add(timevalue);
    }

    private void OnDestroy()
    {
        if(timeIdxList.Count > 0)
            saveLogging();
    }

    private void FixedUpdate()
    {
        loggingOneLine(timeIdx, Time.time);

        timeIdx += 1;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
