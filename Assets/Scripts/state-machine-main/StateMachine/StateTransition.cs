using System;

namespace StateMachine
{
    public class StateTransition : IStateTransition
    {
        public State StateTo { get; private set; }
        public StateCondition Condition { get; private set; }
        public event Action transitionDeInitialized;

        public StateTransition(State state, StateCondition stateConditionCondition)
        {
            StateTo = state;
            Condition = stateConditionCondition;
        }
        
        public void InitializeCondition()
        {
            Condition.InitializeCondition();
        }

        public void DeInitializeCondition()
        {
            Condition.DeInitializeCondition();
            transitionDeInitialized?.Invoke();
        }
    }

    public interface IStateTransition
    {
        public void InitializeCondition();
        public void DeInitializeCondition();
        public State StateTo { get; }
        public StateCondition Condition { get; }
    }
}