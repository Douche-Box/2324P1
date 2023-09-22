using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSlideState : CharBaseState
{
    public CharSlideState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Slide State Enter");
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
        if (!Ctx.IsMove)
        {
            // Debug.Log("Run > Idle");
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMove && !Ctx.IsSlide)
        {
            // Debug.Log("Run > Walk");
            SwitchState(Factory.Walk());
        }
    }
}
