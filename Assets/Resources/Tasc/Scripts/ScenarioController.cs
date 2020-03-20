using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tasc;

public class ScenarioController : MonoBehaviour
{
    bool isTerminusExported = false;
    bool isActionExported = false;
    Scenario scenario = new Scenario("Crane manipulation", "A user is required to memorize a series of equipment operations.");

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
        Button correctButton = GameObject.Find("button_red").GetComponent<Button>();
        Lever lever = FindObjectsOfType<Lever>()[0];

        Task task1 = new Task("Task1", "");
        //task1.instruction = new Instruction("첫 번째 단계로 조이스틱을 조종하여 X 좌표를 43으로 Y 좌표를 -29로 맞춰주세요.");
        task1.instruction = new Instruction(task1.name);
        task1.instruction.SetContentWithContext("Stage 1", Information.Context.Title);
        task1.instruction.SetContentWithContext("As a first step, control the joystick.", Information.Context.Narration);
        task1.instruction.SetContentWithContext("As a first step, control the joystick to set the X coordinate to 43 and the Y coordinate to -29.", Information.Context.Description);
        task1.exit = new Condition(new VariableDistanceState(new VectorVariableState(joystick, "leverCoord", new Vector3(43, -29, 0)), 10.0f), Condition.RelationalOperator.SmallerOrEqual);
        scenario.Add(task1);

        Task task2 = new Task("Task2", "");
        task2.instruction = new Instruction(task2.name);
        task2.instruction.SetContentWithContext("Stage 2", Information.Context.Title);
        task2.instruction.SetContentWithContext("As a second step, turn the wheel clockwise one turn.", Information.Context.Narration);
        task2.instruction.SetContentWithContext("As a second step, turn the wheel clockwise one turn.", Information.Context.Description);
        task2.exit = new Condition(new BoolVariableState(wheel, "didCW", true), Condition.RelationalOperator.Equal);
        scenario.Add(task2);

        Task task3 = new Task("Task3", "");
        task3.instruction = new Instruction(task3.name);
        task3.instruction.SetContentWithContext("Stage 3", Information.Context.Title);
        task3.instruction.SetContentWithContext("As a third step, press the grab button to hold the crane hook.", Information.Context.Narration);
        task3.instruction.SetContentWithContext("As a third step, press the grab button to hold the crane hook.", Information.Context.Description);
        task3.exit = new Condition(new BoolVariableState(correctButton, "isPushed", true), Condition.RelationalOperator.Equal);
        scenario.Add(task3);

        Task task4 = new Task("Task4", "");
        task4.instruction = new Instruction(task4.name);
        task4.instruction.SetContentWithContext("Stage 4", Information.Context.Title);
        task4.instruction.SetContentWithContext("As a fourth step, pull the lever toward you and change the gear of the crane to 1.", Information.Context.Narration);
        task4.instruction.SetContentWithContext("As a fourth step, pull the lever toward you and change the gear of the crane to 1.", Information.Context.Description);
        task4.exit = new Condition(new IntVariableState(lever, "gearValue", 1), Condition.RelationalOperator.Equal);
        scenario.Add(task4);

        Task task5 = new Task("Task5", "");
        task5.instruction = new Instruction(task5.name);
        task5.instruction.SetContentWithContext("Stage 5", Information.Context.Title);
        task5.instruction.SetContentWithContext("As a fifth step, turn the wheel counterclockwise one turn.", Information.Context.Narration);
        task5.instruction.SetContentWithContext("As a fifth step, turn the wheel counterclockwise one turn.", Information.Context.Description);
        task5.exit = new Condition(new BoolVariableState(wheel, "didCCW", true), Condition.RelationalOperator.Equal);
        scenario.Add(task5);

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
