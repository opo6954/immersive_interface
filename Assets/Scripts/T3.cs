using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tasc;

public class T3 : MonoBehaviour
{
    Scenario scenario = new Scenario("T3", "A user is required to memorize a series of operations.");

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

        Task wheel_ccw = new Task("wheel_ccw", "");
        wheel_ccw.instruction = new Instruction(wheel_ccw.name);
        wheel_ccw.instruction.SetContentWithContext("Stage 1", Information.Context.Title);
        wheel_ccw.instruction.SetContentWithContext("As a fifth step, turn the wheel counterclockwise one turn.", Information.Context.Narration);
        wheel_ccw.instruction.SetContentWithContext("As a fifth step, turn the wheel counterclockwise one turn.", Information.Context.Description);
        wheel_ccw.exit = new Condition(new BoolVariableState(wheel, "didCCW", true), Condition.RelationalOperator.Equal);
        scenario.Add(wheel_ccw);

        Task lever_3 = new Task("lever_3", "");
        lever_3.instruction = new Instruction(lever_3.name);
        lever_3.instruction.SetContentWithContext("Stage 2", Information.Context.Title);
        lever_3.instruction.SetContentWithContext("", Information.Context.Narration);
        lever_3.instruction.SetContentWithContext("", Information.Context.Description);
        lever_3.exit = new Condition(new IntVariableState(lever, "gearValue", 3), Condition.RelationalOperator.Equal);
        scenario.Add(lever_3);

        Task button_yellow = new Task("button_yellow", "");
        button_yellow.instruction = new Instruction(button_yellow.name);
        button_yellow.instruction.SetContentWithContext("Stage 3", Information.Context.Title);
        button_yellow.instruction.SetContentWithContext("", Information.Context.Narration);
        button_yellow.instruction.SetContentWithContext("", Information.Context.Description);
        button_yellow.exit = new Condition(new BoolVariableState(buttonYellow, "isPushed", true), Condition.RelationalOperator.Equal);
        scenario.Add(button_yellow);

        Task joystick_sw = new Task("joystick_sw", "");
        joystick_sw.instruction = new Instruction(joystick_sw.name);
        joystick_sw.instruction.SetContentWithContext("Stage 4", Information.Context.Title);
        joystick_sw.instruction.SetContentWithContext("", Information.Context.Narration);
        joystick_sw.instruction.SetContentWithContext("", Information.Context.Description);
        joystick_sw.exit = new Condition(new VariableDistanceState(new VectorVariableState(joystick, "leverCoord", new Vector3(-20, -20, 0)), 10.0f), Condition.RelationalOperator.SmallerOrEqual);
        scenario.Add(joystick_sw);

        Task lever_1 = new Task("lever_1", "");
        lever_1.instruction = new Instruction(lever_1.name);
        lever_1.instruction.SetContentWithContext("Stage 5", Information.Context.Title);
        lever_1.instruction.SetContentWithContext("", Information.Context.Narration);
        lever_1.instruction.SetContentWithContext("", Information.Context.Description);
        lever_1.exit = new Condition(new IntVariableState(lever, "gearValue", 1), Condition.RelationalOperator.Equal);
        scenario.Add(lever_1);

        Task wheel_cw = new Task("wheel_cw", "");
        wheel_cw.instruction = new Instruction(wheel_cw.name);
        wheel_cw.instruction.SetContentWithContext("Stage 6", Information.Context.Title);
        wheel_cw.instruction.SetContentWithContext("turn the wheel clockwise one turn.", Information.Context.Narration);
        wheel_cw.instruction.SetContentWithContext("turn the wheel clockwise one turn.", Information.Context.Description);
        wheel_cw.exit = new Condition(new BoolVariableState(wheel, "didCW", true), Condition.RelationalOperator.Equal);
        scenario.Add(wheel_cw);

        Task joystick_w = new Task("joystick_w", "");
        joystick_w.instruction = new Instruction(joystick_w.name);
        joystick_w.instruction.SetContentWithContext("Stage 7", Information.Context.Title);
        joystick_w.instruction.SetContentWithContext("", Information.Context.Narration);
        joystick_w.instruction.SetContentWithContext("", Information.Context.Description);
        joystick_w.exit = new Condition(new VariableDistanceState(new VectorVariableState(joystick, "leverCoord", new Vector3(-20, 0, 0)), 10.0f), Condition.RelationalOperator.SmallerOrEqual);
        scenario.Add(joystick_w);

        Task button_orange = new Task("button_orange", "");
        button_orange.instruction = new Instruction(button_orange.name);
        button_orange.instruction.SetContentWithContext("Stage 8", Information.Context.Title);
        button_orange.instruction.SetContentWithContext("", Information.Context.Narration);
        button_orange.instruction.SetContentWithContext("", Information.Context.Description);
        button_orange.exit = new Condition(new BoolVariableState(buttonOrange, "isPushed", true), Condition.RelationalOperator.Equal);
        scenario.Add(button_orange);

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
