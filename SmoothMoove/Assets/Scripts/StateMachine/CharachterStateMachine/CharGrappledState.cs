using UnityEngine;

public class CharGrappledState : CharBaseState
{
    public CharGrappledState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        Ctx.DesiredMoveForce = Ctx.GrappleSpeed;

        if (Ctx.GrappleJoint == null && Ctx.IsShoot)
        {
            Ctx.MakeGrappleJoint();
        }
    }

    public override void ExitState()
    {
        Ctx.DestroyGrapplePoint();
    }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void LateUpdateState() { }

    public override void FixedUpdateState() { }

    #endregion

    public override void InitializeSubState()
    {
        if (!Ctx.IsMove)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMove)
        {
            SetSubState(Factory.Walk());
        }
    }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsShoot)
        {
            SwitchState(Factory.Fall());
        }
    }
}
