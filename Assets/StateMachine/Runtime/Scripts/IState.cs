/**
 * Created by Kirk George 03/30/2020
 * Interface that defines a state on a state machine.
 **/

public interface IState
{
    IStateMachine owner { get; set; }
    bool pendingPop { get; set; }

    // first step of state push logic
    void PreTransitionIn();
    // second step of state push logic -- any transition data available is sent in, and this should handle transitions if it makes sense.
    StateTransition TransitionIn( StateTransition transition );
    // third step of state push logic
    void OnEnterState();
    // occurs once a state becomes the top of the state machine stack.
    // happens immediately after OnEnterState, or after a top state is popped.
    void OnBecomeActive();
    // happens once every frame after OnBecomeActive ONLY IF THE STATE IS ACTIVE
    void ActiveUpdate();
    // happens once every frame while the state is ANYWHERE on the stack
    void UniversalUpdate();
    // happens once every heartbeat after OnBecomeActive ONLY IF THE STATE IS ACTIVE
    void ActiveHeartbeat();
    // happens once every heartbeat while the state is ANYWHERE on the stack
    void UniversalHeartbeat();
    // this happens when a state becomes inactive
    // 1. a state is pushed on top of this state
    // 2. this state is popped (before OnExitState)
    void OnBecomeInactive();
    // first step of state pop logic
    void OnExitState();
    // second step of state pop logic -- any transition data available is sent in.
    // returns any newly generated transition data
    StateTransition TransitionOut( StateTransition transition );
    // final step of state pop logic
    void OnStateComplete();
}
