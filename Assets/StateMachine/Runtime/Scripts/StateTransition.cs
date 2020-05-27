/**
 * Created by Kirk George 03/30/2020
 * Data object for transitions
 **/

namespace com.keg.statemachine
{
    public abstract class StateTransition
    {
        public ITransitionHandler owner;

        public abstract void Start();

        // instantly end the transition
        public virtual void KillTransition()
        {
            owner.RemoveTransition();
        }

        // start the transition end process
        public virtual void EndTransition()
        {
            owner.RemoveTransition();
        }
    }
}
