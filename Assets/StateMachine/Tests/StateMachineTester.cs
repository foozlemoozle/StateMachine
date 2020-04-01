/**
 * Created by Kirk George 03/31/2020
 * Tests state machine
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StateMachineTester : StateMachineRunner
{
    private int _stateCount = 0;
    private Stack<TestState> _states;

    public void CreateState()
    {
        if( _states == null )
        {
            _states = new Stack<TestState>();
        }

        TestState state = new TestState( _stateCount.ToString() );
        stateMachine.Push( state );
        _states.Push( state );
        _stateCount++;
    }

    public void RemoveState()
    {
        if( _states == null )
        {
            return;
        }

        stateMachine.Pop( _states.Pop() );
    }
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor( typeof( StateMachineTester ) )]
public class StateMachineTesterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if( Application.isPlaying )
        {
            StateMachineTester smt = target as StateMachineTester;
            if( GUILayout.Button( "Create" ) )
            {
                smt.CreateState();
            }
            if( GUILayout.Button( "Remove" ) )
            {
                smt.RemoveState();
            }
        }

        base.OnInspectorGUI();
    }
}
#endif
