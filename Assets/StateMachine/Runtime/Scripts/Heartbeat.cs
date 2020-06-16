/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// See upload date for date created.

/**
 * Created by Kirk George 03/31/2020
 * Manages heartbeat.  Singleton class.
 * Only ONE object should call Update.
 **/

using System.Collections.Generic;

namespace com.keg.statemachine
{
    public class Heartbeat
    {
        public delegate void Action();

        //frame rate is used to decide how many staggered heartbeats.
        //this value should be LESS THAN your target frame rate.
        //Heartbeat length is equal to this number of Update calls.
        private static readonly int _TICKS_PER_BEAT = 30;
        private static Heartbeat _instance;

        private int _curFrame = 0;
        private int _eventIndex = 0;

        private Action[] _events;
        private Dictionary<Action, int> _callbackToTickMap;

        private Heartbeat()
        {
            _events = new Action[ _TICKS_PER_BEAT ];
            _callbackToTickMap = new Dictionary<Action, int>();
        }

        private static void InstantiateIfNeeded()
        {
            if( _instance == null )
            {
                _instance = new Heartbeat();
            }
        }

        public static void Tick()
        {
            InstantiateIfNeeded();

            if( _instance._curFrame < _instance._events.Length && _instance._events[ _instance._curFrame ] != null )
            {
                _instance._events[ _instance._curFrame ]();
                ++_instance._curFrame;
            }
            else if( _instance._curFrame >= _TICKS_PER_BEAT )
            {
                _instance._curFrame = 0;
            }
            else
            {
                ++_instance._curFrame;
            }
        }

        public static void Register( Action callback )
        {
            InstantiateIfNeeded();

            if( !_instance._callbackToTickMap.ContainsKey( callback ) )
            {
                if( _instance._eventIndex >= _TICKS_PER_BEAT )
                {
                    _instance._eventIndex = 0;
                }

                _instance._callbackToTickMap.Add( callback, _instance._eventIndex );

                if( _instance._events[ _instance._eventIndex ] == null )
                {
                    _instance._events[ _instance._eventIndex ] = _instance.NoOp;
                }
                _instance._events[ _instance._eventIndex ] += callback;

                ++_instance._eventIndex;
            }
        }

        public static void Deregister( Action callback )
        {
            InstantiateIfNeeded();

            if( _instance._callbackToTickMap.ContainsKey( callback ) )
            {
                int index = _instance._callbackToTickMap[ callback ];
                _instance._callbackToTickMap.Remove( callback );
                _instance._events[ index ] -= callback;
            }
        }

        private void NoOp() { }
    }
}
