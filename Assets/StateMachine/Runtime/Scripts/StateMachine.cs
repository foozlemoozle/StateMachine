/**
 * Created by Kirk George 03/30/2020
 * State Machine
 **/

using System.Collections.Generic;

public interface IStateMachine
{
    void Push( IState state );
    void Pop( IState state );
    void Flush();

    string[] GetStateNames();
}

public class StateMachine : IStateMachine, ITransitionHandler
{
    private enum SMActionType
    {
        Push,
        Pop
    }

    private struct SMAction
    {
        public IState state;
        public SMActionType action;
    }

    private Stack<SMAction> _pendingActions;//LIFO queue
    private bool _lockPush = false;
    private List<IState> _states;
    private StateTransition _transition;

    public StateTransition transition => _transition;

    public StateMachine()
    {
        _pendingActions = new Stack<SMAction>();
        _lockPush = false;
        _states = new List<IState>();
    }

    public void Setup()
    {
        Heartbeat.Register( Beat );
    }

    public void Teardown()
    {
        Heartbeat.Deregister( Beat );
        Flush();
    }

    public void Push( IState state )
    {
        if( _lockPush )
        {
            return;
        }

        _pendingActions.Push( new SMAction() { state = state, action = SMActionType.Push } );

        HandlePendingAction();
    }

    private void HandlePendingAction()
    {
        if( _pendingActions.Count > 0 )
        {
            SMAction action = _pendingActions.Pop();
            if( action.action == SMActionType.Push )
            {
                PushInternal( action.state );
            }
            else
            {
                PopInternal( action.state );
            }
        }
    }

    private void PushInternal( IState state )
    {
        // make top state inactive
        IState top;
        if( TryGetTopState( out top ) )
        {
            top.OnBecomeInactive();
        }

        // setup the state
        state.owner = this;
        _states.Add( state );

        // execute state push steps
        state.PreTransitionIn();
        _transition = state.TransitionIn( _transition );
        state.OnEnterState();
        state.OnBecomeActive();

        //handle next action in our queue
        HandlePendingAction();
    }

    public void Update()
    {
        IState top;
        if( TryGetTopState( out top ) )
        {
            top.ActiveUpdate();

            int count = _states.Count;
            for( int i = 0; i < count; ++i )
            {
                _states[ i ].UniversalUpdate();
            }
        }
    }

    private void Beat()
    {
        IState top;
        if( TryGetTopState( out top ) )
        {
            top.ActiveHeartbeat();

            int count = _states.Count;
            for( int i = 0; i < count; ++i )
            {
                _states[ i ].UniversalHeartbeat();
            }
        }
    }

    public void Pop( IState state )
    {
        _pendingActions.Push( new SMAction() { state = state, action = SMActionType.Pop } );

        HandlePendingAction();
    }

    private void PopInternal( IState state )
    {
        //mark given state as pending pop
        state.pendingPop = true;

        IState top;
        if( TryGetTopState( out top ) )
        {
            if( top == state )
            {
                //remove from stack
                _states.Remove( top );

                // execute pop methods
                state.OnBecomeInactive();
                state.OnExitState();
                _transition = state.TransitionOut( _transition );
                state.OnStateComplete();

                //clean up values
                state.owner = null;
                state.pendingPop = false;

                // make the top state become active
                HandleOnBecomeActive();
            }
        }

        //continue handling pending actions
        HandlePendingAction();
    }

    public void Flush()
    {
        // mark all states as pending pop
        int count = _states.Count;
        for( int i = 0; i < count; ++i )
        {
            _states[ i ].pendingPop = true;
        }

        //mark push as locked
        _lockPush = true;

        //pop the top state
        IState top;
        if( TryGetTopState( out top ) )
        {
            Pop( top );
        }
    }

    private void HandleOnBecomeActive()
    {
        IState top;
        if( TryGetTopState( out top ) )
        {
            if( top.pendingPop )
            {
                Pop( top );
            }
            else
            {
                top.OnBecomeActive();
            }
        }
        // if there is no state to make active, then make sure pushing is not locked
        else
        {
            _lockPush = false;
        }
    }

    public bool TrySetTransition( StateTransition transition )
    {
        if( _transition == null )
        {
            _transition = transition;
            _transition.owner = this;
            return true;
        }

        return false;
    }

    public void RemoveTransition()
    {
        _transition.owner = null;
        _transition = null;
    }

    private bool TryGetTopState( out IState state )
    {
        state = null;
        int count = _states.Count;
        if( count > 0 )
        {
            state = _states[ count - 1 ];
        }

        return state != null;
    }

    public string[] GetStateNames()
    {
        int count = _states.Count;
        string[] names = new string[ count ];

        for( int i = 0; i < count; ++i )
        {
            names[ i ] = _states[ i ].GetType().Name;
        }

        return names;
    }
}
