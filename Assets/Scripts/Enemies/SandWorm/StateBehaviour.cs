//this is a state template
//functions that each state is obligated to have. abstract means that the code has to implement it
using UnityEngine;

public abstract class StateBehaviour : MonoBehaviour
{
    public NewStateMachine AssociatedStateMachine { get; set; }
    public abstract bool InitializeState();
    public abstract void OnStateStart();
    public abstract void OnStateUpdate();
    public abstract void OnStateEnd();
    public abstract int StateTransitionCondition();
}
