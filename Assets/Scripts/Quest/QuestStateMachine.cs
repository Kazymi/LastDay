using System;
using System.Collections;
using StateMachine;
using StateMachine.Conditions;
using UnityEngine;
using StateMachine = StateMachine.StateMachine;

public class QuestStateMachine : MonoBehaviour
{
    [SerializeField] private QuestUIAnimationConfiguration questUIAnimationConfiguration;
    [SerializeField] private QuestUI questUI;

    private Quest nextQuest;
    private global::StateMachine.StateMachine StateMachine;

    private QuestStateIdleState questStateIdle;
    private QuestEnableState questEnableState;
    private QuestDisableState questDisableState;
    private QuestActiveState questActiveState;
    
    private void Awake()
    {
        InitializeStateMachine();
    }

    public void StartNewQuest(Quest quest)
    {
        nextQuest = quest;
    }

    private void Update()
    {
        StateMachine.Tick();
    }

    private void InitializeStateMachine()
    {
        questStateIdle = new QuestStateIdleState();
        questEnableState = new QuestEnableState(questUI, questUIAnimationConfiguration);
        questDisableState = new QuestDisableState(questUI);
        questActiveState = new QuestActiveState(questUI);

        questStateIdle.AddTransition(new StateTransition(questEnableState, new FuncCondition(() =>
        {
            if (nextQuest == null)
            {
                return false;
            }

            questEnableState.SetQuest(nextQuest);
            questDisableState.SetQuest(nextQuest);
            questActiveState.SetQuest(nextQuest);
            nextQuest = null;
            return true;
        })));

        questEnableState.AddTransition(new StateTransition(questActiveState,
            new FuncCondition(() => questEnableState.IsReadyToLeave)));

        questActiveState.AddTransition(new StateTransition(questDisableState,
            new FuncCondition(() => questActiveState.IsReadyToLeave)));

        questDisableState.AddTransition(new StateTransition(questStateIdle,
            new FuncCondition(() => questDisableState.IsReadyToLeave)));
        StateMachine = new global::StateMachine.StateMachine(questStateIdle);
    }
}