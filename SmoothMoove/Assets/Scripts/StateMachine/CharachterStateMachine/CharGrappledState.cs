using UnityEngine;
using UnityEngine.Video;

public class CharGrappledState : CharBaseState
{
    public CharGrappledState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        InitializeSubState();

        Ctx.Rb.useGravity = false;

        Ctx.DesiredMoveForce = Ctx.GrappleSpeed;
        Ctx.GrappleDirection = (Ctx.GrapplePoint - Ctx.transform.position).normalized;

        HandleGrapple();
    }

    public override void ExitState()
    {
        Ctx.IsGrappled = false;
        Ctx.Rb.useGravity = true;
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
        if (Ctx.IsGrounded)
        {
            SwitchState(Factory.Grounded());
        }
        else if (Ctx.IsSloped)
        {
            SwitchState(Factory.Sloped());
        }
        else if (!Ctx.IsGrounded && !Ctx.IsSloped)
        {
            SwitchState(Factory.Fall());
        }
    }

    private void HandleGrapple()
    {
        Ctx.Rb.velocity = new Vector3(Ctx.Rb.velocity.x, 0f, Ctx.Rb.velocity.z);
        Ctx.Rb.AddForce(Ctx.GrappleDirection * Ctx.GrappleSpeed * Ctx.GrappleSpeedIncreaseMultiplier, ForceMode.Impulse);
    }
}
