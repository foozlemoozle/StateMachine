/// Created by: Kirk George
/// Copyright: Kirk George
/// Website: https://github.com/foozlemoozle?tab=repositories
/// See upload date for date created.

/**
 * Created by Kirk George 03/30/2020
 * Interface for a class that manages transitions.
 **/

namespace com.keg.statemachine
{
    public interface ITransitionHandler
    {
        StateTransition transition { get; }
        bool TrySetTransition( StateTransition transition );
        void RemoveTransition();
    }
}
