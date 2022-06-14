using System;

public class DelegateTransition : StateTransition {

    private Func<bool> _conditionFunc;

    public DelegateTransition(State stateToGo, Func<bool> conditionFunc) {
        nextState = stateToGo;
        _conditionFunc = conditionFunc;
    }

    public override bool Condition() => _conditionFunc.Invoke();

}
