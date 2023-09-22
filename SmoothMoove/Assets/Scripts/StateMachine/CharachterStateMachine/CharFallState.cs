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
        // Debug.Log("Fall State Enter");
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
        else
        {
            SetSubState(Factory.Slide());
        }
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsGrounded)
        {
            SwitchState(Factory.Grounded());
        }
        if (Ctx.IsSloped)
        {
            SwitchState(Factory.Sloped());
        }
    }
}