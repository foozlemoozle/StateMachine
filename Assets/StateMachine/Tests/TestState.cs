/**
 * Created by Kirk George 03/31/2020
 * Test state
 **/

using Debug = UnityEngine.Debug;
using com.keg.statemachine;

namespace com.keg.statemachine.tests
{
    public class TestState : State
    {
        public string name;

        public TestState( string name )
        {
            this.name = name;
        }

        public override void PreTransitionIn()
        {
            base.PreTransitionIn();
            Debug.LogFormat( "PreTransitionIn {0}", name );
        }

        public override StateTransition TransitionIn( StateTransition transition )
        {
            Debug.LogFormat( "TransitionIn {0}", name );
            return base.TransitionIn( transition );
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            Debug.LogFormat( "OnEnterState {0}", name );
        }

        public override void OnBecomeActive()
        {
            base.OnBecomeActive();
            Debug.LogFormat( "OnBecomeActive {0}", name );
        }

        public override void ActiveUpdate()
        {
            base.ActiveUpdate();
            Debug.LogFormat( "ActiveUpdate {0}", name );
        }

        public override void ActiveHeartbeat()
        {
            base.ActiveHeartbeat();
            Debug.LogFormat( "ActiveHeartbeat {0}", name );
        }

        public override void UniversalUpdate()
        {
            base.UniversalUpdate();
            Debug.LogFormat( "UniversalUpdate {0}", name );
        }

        public override void UniversalHeartbeat()
        {
            base.UniversalHeartbeat();
            Debug.LogFormat( "UniversalHeartbeat {0}", name );
        }

        public override void OnBecomeInactive()
        {
            base.OnBecomeInactive();
            Debug.LogFormat( "OnBecomeInactive {0}", name );
        }

        public override StateTransition TransitionOut( StateTransition transition )
        {
            Debug.LogFormat( "TransitionOut {0}", name );
            return base.TransitionOut( transition );
        }

        public override void OnExitState()
        {
            base.OnExitState();
            Debug.LogFormat( "OnExitState {0}", name );
        }

        public override void OnStateComplete()
        {
            base.OnStateComplete();
            Debug.LogFormat( "OnStateComplete {0}", name );
        }
    }
}
