/**
 * Created by Kirk George 03/31/2020
 * Editor window to view the state machine
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace com.keg.statemachine
{
    public class StateMachineViewer : EditorWindow
    {
        [MenuItem( "StateMachine/StateMachine Viewer" )]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<StateMachineViewer>();
        }

        private StateMachineRunner _runner;

        private void OnGUI()
        {
            if( !Application.isPlaying )
            {
                DisplayMessage( "Application needs to be running!", Color.yellow );
                return;
            }

            if( _runner == null )
            {
                StateMachineRunner[] searchResults = Resources.FindObjectsOfTypeAll<StateMachineRunner>();
                if( searchResults.Length == 1 )
                {
                    _runner = searchResults[ 0 ];
                }
                else if( searchResults.Length > 1 )
                {
                    DisplayMessage(
                        string.Format( "FOUND {0} INSTANCES OF StateMachineRunner.  THERE SHOULD ONLY BE ONE.", searchResults.Length ),
                        Color.red
                        );

                    return;
                }
                else
                {
                    DisplayMessage( "No state machines!", Color.yellow );
                    return;
                }
            }

            DisplayMessage( "Top", Color.white );

            GUIStyle cyan = new GUIStyle( EditorStyles.label );
            cyan.normal.textColor = Color.cyan;

            string[] states = _runner.stateMachine.GetStateNames();
            int count = states.Length;
            for( int i = 0; i < count; ++i )
            {
                if( GUILayout.Button( states[ i ], cyan ) )
                {
                    var results = AssetDatabase.FindAssets( states[ i ] + ".cs" );
                    if( results.Length > 0 )
                    {
                        MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>( results[ 0 ] );
                        if( script != null )
                        {
                            AssetDatabase.OpenAsset( script.GetInstanceID() );
                        }
                    }
                }
            }

            DisplayMessage( "Bottom", Color.white );
        }

        private void DisplayMessage( string msg, Color color )
        {
            GUIStyle lblClr = new GUIStyle( EditorStyles.label );
            lblClr.normal.textColor = color;

            EditorGUILayout.LabelField( msg, lblClr );
        }
    }
}
