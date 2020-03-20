using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tasc;

public class T2 : MonoBehaviour
{
    bool isTerminusExported = false;
    bool isActionExported = false;
    Scenario scenario = new Scenario("T2", "A user is required to memorize a series of operations.");

    public List<Interface> interfaces;
    public Actor actor;

    // Use this for initialization
    void Start()
    {
        InitializeScenario();
    }

    void InitializeScenario()
    {
        MakeTestScenario();
    }

    void MakeTestScenario()
    {
        ////////////// actor null exception 

        //*
        Task introduction = new Task("Intro", "");
        introduction.instruction = new Instruction(introduction.name);
        introduction.instruction.SetContentWithContext("Tutorial", Information.Context.Title);
        introduction.instruction.SetContentWithContext("Hi. Your training is just started. Please follow the instruction.", Information.Context.Narration);
        introduction.instruction.SetContentWithContext("Hi. Your training is just started. Please follow the instruction.", Information.Context.Description);
        introduction.exit = new Condition(new TimeState(0, 0, 5), Condition.RelationalOperator.LargerOrEqual);
        //introduction.exit = new Condition(new InputUpState(actor, (int)KeyCode.C), Condition.Operator.Equal);
        scenario.Add(introduction);


        //안녕하세요. 지금부터 장비 훈련을 시작하겠습니다. 노란 머리의 엔지니어의 시범을 본 후에 따라해주세요.
        //첫 번째 단계로 조이스틱을 조종하여 X 좌표를 43으로 Y 좌표를 -29로 맞춰주세요.
        //두 번째 단계로 휠을 시계 방향으로 한 바퀴 돌려 크레인 훅을 내려보세요.
        //세 번째 단계로 그랩 버튼을 눌러 크레인 훅이 물체를 잡도록 하세요.
        //네 번째 단계로 레버를 몸 쪽으로 잡아 당겨 크레인의 기어를 1로 바꿔 주세요.
        //다섯 번째 단계로 휠을 반시계방향으로 한 바퀴 돌려 크레인 훅을 올라가도록 하세요.
        //훈련이 종료되었습니다. 수고하셨습니다.

        Joystick joystick = FindObjectsOfType<Joystick>()[0];
        Wheel wheel = FindObjectsOfType<Wheel>()[0];
        Button buttonPurple = GameObject.Find("button_purple").GetComponent<Button>();
        Button buttonGreen = GameObject.Find("button_green").GetComponent<Button>();
        Button buttonRed = GameObject.Find("button_red").GetComponent<Button>();
        Button buttonOrange = GameObject.Find("button_orange").GetComponent<Button>();
        Button buttonYellow = GameObject.Find("button_yellow").GetComponent<Button>();
        Button buttonBlue = GameObject.Find("button_blue").GetComponent<Button>();
        Lever lever = FindObjectsOfType<Lever>()[0];

        Task button_blue = new Task("button_blue", "");
        button_blue.instruction = new Instruction(button_blue.name);
        button_blue.instruction.SetContentWithContext("Stage 1", Information.Context.Title);
        button_blue.instruction.SetContentWithContext("", Information.Context.Narration);
        button_blue.instruction.SetContentWithContext("", Information.Context.Description);
        button_blue.exit = new Condition(new BoolVariableState(buttonBlue, "isPushed", true), Condition.RelationalOperator.Equal);
        scenario.Add(button_blue);

        Task wheel_ccw = new Task("wheel_ccw", "");
        wheel_ccw.instruction = new Instruction(wheel_ccw.name);
        wheel_ccw.instruction.SetContentWithContext("Stage 2", Information.Context.Title);
        wheel_ccw.instruction.SetContentWithContext("As a fifth step, turn the wheel counterclockwise one turn.", Information.Context.Narration);
        wheel_ccw.instruction.SetContentWithContext("As a fifth step, turn the wheel counterclockwise one turn.", Information.Context.Description);
        wheel_ccw.exit = new Condition(new BoolVariableState(wheel, "didCCW", true), Condition.RelationalOperator.Equal);
        scenario.Add(wheel_ccw);

        Task joystick_s = new Task("joystick_s", "");
        joystick_s.instruction = new Instruction(joystick_s.name);
        joystick_s.instruction.SetContentWithContext("Stage 3", Information.Context.Title);
        joystick_s.instruction.SetContentWithContext("", Information.Context.Narration);
        joystick_s.instruction.SetContentWithContext("", Information.Context.Description);
        joystick_s.exit = new Condition(new VariableDistanceState(new VectorVariableState(joystick, "leverCoord", new Vector3(0, -20, 0)), 10.0f), Condition.RelationalOperator.SmallerOrEqual);
        scenario.Add(joystick_s);

        Task wheel_cw = new Task("wheel_cw", "");
        wheel_cw.instruction = new Instruction(wheel_cw.name);
        wheel_cw.instruction.SetContentWithContext("Stage 4", Information.Context.Title);
        wheel_cw.instruction.SetContentWithContext("turn the wheel clockwise one turn.", Information.Context.Narration);
        wheel_cw.instruction.SetContentWithContext("turn the wheel clockwise one turn.", Information.Context.Description);
        wheel_cw.exit = new Condition(new BoolVariableState(wheel, "didCW", true), Condition.RelationalOperator.Equal);
        scenario.Add(wheel_cw);

        Task lever_3 = new Task("lever_3", "");
        lever_3.instruction = new Instruction(lever_3.name);
        lever_3.instruction.SetContentWithContext("Stage 5", Information.Context.Title);
        lever_3.instruction.SetContentWithContext("", Information.Context.Narration);
        lever_3.instruction.SetContentWithContext("", Information.Context.Description);
        lever_3.exit = new Condition(new IntVariableState(lever, "gearValue", 3), Condition.RelationalOperator.Equal);
        scenario.Add(lever_3);

        Task button_red = new Task("button_red", "");
        button_red.instruction = new Instruction(button_red.name);
        button_red.instruction.SetContentWithContext("Stage 6", Information.Context.Title);
        button_red.instruction.SetContentWithContext("", Information.Context.Narration);
        button_red.instruction.SetContentWithContext("", Information.Context.Description);
        button_red.exit = new Condition(new BoolVariableState(buttonRed, "isPushed", true), Condition.RelationalOperator.Equal);
        scenario.Add(button_red);

        Task lever_2 = new Task("lever_2", "");
        lever_2.instruction = new Instruction(lever_2.name);
        lever_2.instruction.SetContentWithContext("Stage 7", Information.Context.Title);
        lever_2.instruction.SetContentWithContext("", Information.Context.Narration);
        lever_2.instruction.SetContentWithContext("", Information.Context.Description);
        lever_2.exit = new Condition(new IntVariableState(lever, "gearValue", 2), Condition.RelationalOperator.Equal);
        scenario.Add(lever_2);

        Task joystick_se = new Task("joystick_se", "");
        joystick_se.instruction = new Instruction(joystick_se.name);
        joystick_se.instruction.SetContentWithContext("Stage 8", Information.Context.Title);
        joystick_se.instruction.SetContentWithContext("", Information.Context.Narration);
        joystick_se.instruction.SetContentWithContext("", Information.Context.Description);
        joystick_se.exit = new Condition(new VariableDistanceState(new VectorVariableState(joystick, "leverCoord", new Vector3(20, -20, 0)), 10.0f), Condition.RelationalOperator.SmallerOrEqual);
        scenario.Add(joystick_se);

        Task ending = new Task("Finish", "");
        ending.instruction = new Instruction(ending.name);
        ending.instruction.SetContentWithContext("Finish", Information.Context.Title);
        ending.instruction.SetContentWithContext("Well done! Your training is successfully terminated.", Information.Context.Narration);
        ending.instruction.SetContentWithContext("Well done! Your training is successfully terminated.", Information.Context.Description);
        ending.exit = new Condition(new InputUpState(actor, (int)KeyCode.C), Condition.RelationalOperator.Equal);
        scenario.Add(ending);

        scenario.MakeProcedure();

        scenario.Activate();
    }

    // Update is called once per frame
    void Update()
    {
        if (scenario != null)
            scenario.Proceed(interfaces);
    }
}
