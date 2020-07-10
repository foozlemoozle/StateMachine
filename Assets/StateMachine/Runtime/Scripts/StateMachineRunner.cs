/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// See upload date for date created.

/**
 * Created by Kirk George 03/30/2020
 * Runs a state machine
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace com.keg.statemachine
{
#if UNITY_EDITOR
    public static class StateMachineAccessor
	{
        public static IStateMachine stateMachine;
	}
#endif

    public class StateMachineRunner : MonoBehaviour
    {
        private StateMachine _rootStateMachine;

        public IStateMachine stateMachine => _rootStateMachine;

        // Start is called before the first frame update
        void Awake()
        {
            _rootStateMachine = new StateMachine();
        }

        private void OnEnable()
        {
            _rootStateMachine.Setup();
#if UNITY_EDITOR
            StateMachineAccessor.stateMachine = _rootStateMachine;
#endif
        }

        // Update is called once per frame
        void Update()
        {
            _rootStateMachine.Update();
            Heartbeat.Tick();
        }

        private void OnDisable()
        {
            _rootStateMachine.Teardown();
        }
    }
}
