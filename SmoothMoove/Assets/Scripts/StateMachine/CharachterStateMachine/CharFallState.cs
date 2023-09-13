using UnityEngine;
public class CharFallState : CharBaseState
{
    public CharFallState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory)
    {
        IsRootState = true;
    }

    //make a wait time for when you can move after falling for a bit

    public override void EnterState()
    {
        InitializeSubState();
        Debug.Log("Fall State Enter");
    }

    public override void ExitState() { }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState() { }

    #endregion

    public override void InitializeSubState()
    {
        if (!Ctx.IsMove && !Ctx.IsRun)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMove && !Ctx.IsRun)
        {
            SetSubState(Factory.Walk());
        }
        else
        {
            SetSubState(Factory.Run());
        }
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsGrounded)
        {
            Debug.Log("Fall > Grounded");
            SwitchState(Factory.Grounded());
        }
    }
}