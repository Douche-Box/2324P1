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
        Ctx.GrappleHooks--;
        Ctx.DesiredMoveForce = Ctx.GrappleSpeed;
        Ctx.GrappleDirection = (Ctx.GrapplePoint - Ctx.transform.position).normalized;

        HandleGrapple();
    }

    public override void ExitState()
    {
        Ctx.IsGrappled = false;
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

        Ctx.GrappleSpeed = 13;
        float grapplespeedextra = Ctx.GrappleSpeed = 13 + Vector3.Distance(Ctx.transform.position, Ctx.GrapplePoint);

        Vector3 newDirection = new Vector3(Ctx.GrappleDirection.x * grapplespeedextra * Ctx.GrappleSpeedIncreaseMultiplier, Ctx.GrappleDirection.y * Ctx.GrappleSpeed, Ctx.GrappleDirection.z * grapplespeedextra * Ctx.GrappleSpeedIncreaseMultiplier);

        Ctx.Rb.AddForce(newDirection, ForceMode.Impulse);
    }
}
