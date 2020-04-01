/**
 * Created by Kirk George 03/30/2020
 * Runs a state machine
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
