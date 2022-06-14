using System;

public abstract class State{

    //public string stateName { get; protected set; }
    public Action onTransition;
    public Action onUpdate;
    public void OnTransition() {
        onTransition?.Invoke();
    }
    public void StateUpdate() {
        onUpdate?.Invoke();
    }

}