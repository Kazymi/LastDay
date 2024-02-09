namespace StateMachine
{
    public class StateMachine : IStateMachine
    {
        public State CurrentState { get; private set; }
        public StateMachine SubMachine { get; private set; }

        public StateMachine(State state)
        {
            SetState(state);
        }

        public void Tick()
        {
            SubMachine?.Tick();
            var newIndex = IsTransitionsCondition();
            if (newIndex != -1)
            {
                SetState(CurrentState.Transitions[newIndex].StateTo);
            }
            else
            {
                CurrentState.Tick();
            }
        }

        public void FixedTick()
        {
            SubMachine?.FixedTick();
            CurrentState.FixedTick();
        }

        private int IsTransitionsCondition()
        {
            var currentTransitions = CurrentState.Transitions;
            for (var i = 0; i != currentTransitions.Count; i++)
            {
                var condition = currentTransitions[i].Condition;
                condition.Tick();
                if (condition.IsConditionSatisfied())
                {
                    return i;
                }
            }

            return -1;
        }

        public StateMachine CreateSubMachine(State startState)
        {
            var newStateMachine = new StateMachine(startState);
            SubMachine = newStateMachine;
            return SubMachine;
        }

        public void SetState(State state)
        {
            CurrentState?.OnStateExit();
            CurrentState?.DeInitializeTransitions();

            CurrentState = state;
            CurrentState.OnStateEnter();
            CurrentState.InitializeTransitions();
        }
    }
}