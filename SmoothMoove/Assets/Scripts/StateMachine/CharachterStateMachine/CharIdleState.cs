using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharIdleState : CharBaseState
{
    public CharIdleState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Idle State Enter");
    }

    public override void ExitState() { }


    #region MonoBehaveiours

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState() { }

    #endregion

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsMove && Ctx.IsRun)
        {
            Debug.Log("Idle > Run");
            SwitchState(Factory.Run());
        }
        else if (Ctx.IsMove)
        {
            Debug.Log("Idle > Walk");
            SwitchState(Factory.Walk());
        }
    }
}
