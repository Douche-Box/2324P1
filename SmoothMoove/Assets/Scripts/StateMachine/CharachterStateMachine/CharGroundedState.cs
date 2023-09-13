using UnityEngine;

public class CharGroundedState : CharBaseState
{
    public CharGroundedState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        Ctx.IsJumpTime = Ctx.MaxJumpTime;
        Debug.Log("Grounded State Enter");
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
        else if (Ctx.IsMove && Ctx.IsRun)
        {
            SetSubState(Factory.Run());
        }
    }

    public override void CheckSwitchStates()
    {
        // Require Jump Press
        if (!Ctx.IsGrounded && !Ctx.IsJump && Ctx.IsJumpTime == 0)
        {
            Debug.Log("Grounded > Fall");
            SwitchState(Factory.Fall());
        }
        if (Ctx.IsJump)
        {
            Debug.Log("Grounded > Jump");
            SwitchState(Factory.Jump());
        }
    }
}
