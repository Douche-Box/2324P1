using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharGrappledState : CharBaseState
{
    public CharGrappledState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Ctx.DesiredMoveForce = Ctx.GrappleSpeed;
    }

    public override void ExitState()
    {
        Debug.Log("exit grapple");
    }

    #region MonoBehaveiours

    public override void UpdateState()
    {

    }

    public override void LateUpdateState() { }

    public override void FixedUpdateState()
    {
        CheckSwitchStates();

    }

    #endregion

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {

    }
}
