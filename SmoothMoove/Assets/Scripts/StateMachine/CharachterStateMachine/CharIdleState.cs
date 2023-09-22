using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharIdleState : CharBaseState
{
    public CharIdleState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory) { }

    public override void EnterState()
    {
        // Debug.Log("Idle State Enter");
    }

    public override void ExitState() { }


    #region MonoBehaveiours

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {
        CheckSwitchStates();
    }

    public override void LateUpdateState()
    {
    }
    #endregion

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsMove && Ctx.IsSlide)
        {
            SwitchState(Factory.Slide());
        }
        else if (Ctx.IsMove)
        {
            SwitchState(Factory.Walk());
        }
    }
}