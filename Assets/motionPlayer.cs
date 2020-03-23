using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class motionPlayer : MonoBehaviour
{
    public Transform Lever;
    public Transform Wheel;
    public Transform Joystick;
    public Transform[] Button;
    public TextMesh Button_UI;
    public Transform fakeHand;

    List<int> timeIdxList;
    List<float> timevalueList;

    List<int> buttonPressedIdxList;

    List<Vector3> levPosList;
    List<Vector3> levRotList;

    List<float> wheelRotYList;

    List<Vector3> joyPosList;
    List<Vector3> joyRotList;

    public List<Vector3> handPosList;
    public List<Vector3> handRotList;

    public List<Vector3> originButtonPosList;

    float buttonYPushedPos = 0;

    int currTimeSlotId=0;
    float prevTime = 0;




    // Start is called before the first frame update
    void Start()
    {
        levPosList = new List<Vector3>();
        levRotList = new List<Vector3>();
        wheelRotYList = new List<float>();
        joyPosList = new List<Vector3>();
        joyRotList = new List<Vector3>();
        buttonPressedIdxList = new List<int>();

        timeIdxList = new List<int>();
        timevalueList = new List<float>();

        handPosList = new List<Vector3>();
        handRotList = new List<Vector3>();

        originButtonPosList = new List<Vector3>();

        for (int i = 0; i < 6; i++)
        {
            originButtonPosList.Add(Button[i].position);
        }

        buttonYPushedPos = originButtonPosList[0].y * 0.8f;

        prevTime = Time.time;


        loadMotionFile("test.csv");
    }

    void resetButton()
    {
        for (int i = 0; i < Button.Length; i++)
        {
            Button[i].position = originButtonPosList[i];
        }
    }

    void loadMotionFile(string fileName)
    {
        string path = fileName;

        string[] textValue = File.ReadAllLines(path);

        if (textValue.Length > 0)
        {
            for (int i = 0; i < textValue.Length; i++)
            {
                if (i == 0)
                    continue;
                string[] textSplit = textValue[i].Split(',');

                timeIdxList.Add(int.Parse(textSplit[0]));
                timevalueList.Add(float.Parse(textSplit[1]));

    

                float levPosX = float.Parse(textSplit[2].TrimStart('('));
                float levPosY = float.Parse(textSplit[3].Trim());
                float levPosZ = float.Parse(textSplit[4].TrimEnd(')'));

                levPosList.Add(new Vector3(levPosX, levPosY, levPosZ));

                float levRotX = float.Parse(textSplit[5].TrimStart('('));
                float levRotY = float.Parse(textSplit[6].Trim());
                float levRotZ = float.Parse(textSplit[7].TrimEnd(')'));

                levRotList.Add(new Vector3(levRotX, levRotY, levRotZ));

                buttonPressedIdxList.Add(int.Parse(textSplit[8]));

                wheelRotYList.Add(float.Parse(textSplit[9]));

                float joyPosX = float.Parse(textSplit[10].TrimStart('('));
                float joyPosY = float.Parse(textSplit[11].Trim());
                float joyPosZ = float.Parse(textSplit[12].TrimEnd(')'));

                joyPosList.Add(new Vector3(joyPosX, joyPosY, joyPosZ));

                float joyRotX = float.Parse(textSplit[13].TrimStart('('));
                float joyRotY = float.Parse(textSplit[14].Trim());
                float joyRotZ = float.Parse(textSplit[15].TrimEnd(')'));

                joyRotList.Add(new Vector3(joyRotX, joyRotY, joyRotZ));


                float handPosX = float.Parse(textSplit[16].TrimStart('('));
                float handPosY = float.Parse(textSplit[17].Trim());
                float handPosZ = float.Parse(textSplit[18].TrimEnd(')'));

                handPosList.Add(new Vector3(handPosX, handPosY, handPosZ));

                float handRotX = float.Parse(textSplit[19].TrimStart('('));
                float handRotY = float.Parse(textSplit[20].Trim());
                float handRotZ = float.Parse(textSplit[21].TrimEnd(')'));

                handRotList.Add(new Vector3(handRotX, handRotY, handRotZ));
            }
        }
        else
        {
            Debug.Log("Fail to load file...");
        }

        // for Debugging


    }

    void setLeverTransform(Vector3 pos, Vector3 rot)
    {
        Lever.position = pos;
        Lever.rotation = Quaternion.Euler(rot);
    }
    void setButtonPressed(int buttonPressedIdx)
    {
        if(buttonPressedIdx >= 0)
        {
            Vector3 pressedButtonPos = new Vector3();
            resetButton();
            pressedButtonPos = new Vector3(originButtonPosList[buttonPressedIdx].x, buttonYPushedPos, originButtonPosList[buttonPressedIdx].z);

            Button[buttonPressedIdx].position = pressedButtonPos;

        }
        
    }
    void setJoystickTransform(Vector3 pos, Vector3 rot)
    {
        Joystick.position = pos;
        Joystick.rotation = Quaternion.Euler(rot);
    }

    void setWheelTransform(float rotY)
    {
        Vector3 prevRot = Wheel.rotation.eulerAngles;

        prevRot.y = rotY;

        Wheel.rotation = Quaternion.Euler(prevRot);
    }

    void setHandTransform(Vector3 pos, Vector3 rot)
    {
        fakeHand.position = pos;
        fakeHand.rotation = Quaternion.Euler(rot);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        setLeverTransform(levPosList[currTimeSlotId], levRotList[currTimeSlotId]);
        setButtonPressed(buttonPressedIdxList[currTimeSlotId]);
        setJoystickTransform(joyPosList[currTimeSlotId], joyRotList[currTimeSlotId]);
        setWheelTransform(wheelRotYList[currTimeSlotId]);
        setHandTransform(handPosList[currTimeSlotId], handRotList[currTimeSlotId]);

        currTimeSlotId += 1;
    }
}
