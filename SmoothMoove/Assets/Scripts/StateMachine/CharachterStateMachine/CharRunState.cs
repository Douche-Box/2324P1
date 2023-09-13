using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharRunState : CharBaseState
{
    public CharRunState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Run State Enter");
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
        if (!Ctx.IsMove)
        {
            Debug.Log("Run > Idle");
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMove && !Ctx.IsRun)
        {
            Debug.Log("Run > Walk");
            SwitchState(Factory.Walk());
        }
    }
}
