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
