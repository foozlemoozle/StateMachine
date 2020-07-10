/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// See upload date for date created.

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

        private void OnGUI()
        {
            if( !Application.isPlaying )
            {
                DisplayMessage( "Application needs to be running!", Color.yellow );
                return;
            }

            if( StateMachineAccessor.stateMachine == null )
            {
                DisplayMessage( "No state machines!", Color.yellow );
                return;
            }

            DisplayMessage( "Top", Color.white );

            GUIStyle cyan = new GUIStyle( EditorStyles.label );
            cyan.normal.textColor = Color.cyan;

            string[] states = StateMachineAccessor.stateMachine.GetStateNames();
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
