/**
 * Created by Kirk George 03/30/2020
 * A base class implementation of IState.
 * It is not requied to use this class as a base to utilize a state machine
 */

public class State : IState
{
    public IStateMachine owner { get; set; }
    public bool pendingPop { get; set; }

    public virtual void PreTransitionIn()
    {
    }

    public virtual StateTransition TransitionIn( StateTransition transition )
    {
        if( transition != null )
        {
            transition.EndTransition();
        }
        return null;
    }

    public virtual void OnEnterState()
    {
    }

    public virtual void OnBecomeActive()
    {
    }

    public virtual void ActiveUpdate()
    {
    }

    public virtual void UniversalUpdate()
    {
    }

    public virtual void ActiveHeartbeat()
    {
    }

    public virtual void UniversalHeartbeat()
    {
    }

    public virtual void OnBecomeInactive()
    {
    }

    public virtual void OnExitState()
    {
    }

    public virtual StateTransition TransitionOut( StateTransition transition )
    {
        if( transition != null )
        {
            transition.EndTransition();
        }
        return null;
    }

    public virtual void OnStateComplete()
    {
    }
}
