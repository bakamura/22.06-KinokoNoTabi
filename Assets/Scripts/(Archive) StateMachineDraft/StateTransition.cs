public abstract class StateTransition {

    public State nextState {  get; protected set; }
    public abstract bool Condition();

}
