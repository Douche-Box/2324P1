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
        Debug.Log("exit grapple");
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

    }

    public override void CheckSwitchStates()
    {

    }
}
