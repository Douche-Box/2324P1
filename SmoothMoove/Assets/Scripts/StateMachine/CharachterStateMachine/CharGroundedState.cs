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
        // Debug.Log("Grounded State Enter");
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

    public override void InitializeSubState()
    {
        if (!Ctx.IsMove && !Ctx.IsSlide)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMove && !Ctx.IsSlide)
        {
            SetSubState(Factory.Walk());
        }
        else if (Ctx.IsMove && Ctx.IsSlide)
        {
            SetSubState(Factory.Slide());
        }
    }

    public override void CheckSwitchStates()
    {
        // Require Jump Press
        if (!Ctx.IsGrounded && !Ctx.IsJump)
        {
            // Debug.Log("Grounded > Fall");
            SwitchState(Factory.Fall());
        }
        if (Ctx.IsJump)
        {
            // Debug.Log("Grounded > Jump");
            SwitchState(Factory.Jump());
        }
        if (Ctx.IsSloped)
        {
            // Debug.Log("Grounded > Sloped");
            SwitchState(Factory.Sloped());
        }
    }
}
