using System;
using System.Collections.Generic;

namespace CodeHub.StateMachine
{
    public abstract class AbstractStateMachine : IStateMachine
    {
        protected Dictionary<Type, IExitableState> States;
        public IExitableState Current { get; protected set; }

        protected AbstractStateMachine()
        {
            States = new Dictionary<Type, IExitableState>();

            //register
            //States[typeof(StartState)] = new StartState(this, gameConfig, levelMap, swipeService, movementService);
        }

        public void EnterState<TState>() where TState : IExitableState
        {
            Current?.Exit();
            var state = States[typeof(TState)];
            (state as IState).Enter();
            Current = state;
        }
    }
}