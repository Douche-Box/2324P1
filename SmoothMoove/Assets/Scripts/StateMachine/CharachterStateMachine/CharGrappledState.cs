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
        Ctx.FinishedGrapple = false;

        Ctx.IsGrappling = true;

        Ctx.GrappleHooks--;

        Ctx.DesiredMoveForce = Ctx.GrappleSpeed;

        Ctx.IsForced = true;

        Ctx.ExtraForce = Ctx.GrappleSpeed;

        Ctx.GrappleDirection = (Ctx.GrapplePoint - Ctx.transform.position).normalized;

        Ctx.PlayerAnimator.SetTrigger("Grapple");

        Ctx.GrappleDelay = Ctx.MaxGrappleDelay;
    }

    public override void ExitState()
    {
        Ctx.IsGrappling = false;
    }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        CheckSwitchStates();

        Ctx.GrappleDelay -= Time.deltaTime;

        if (Ctx.GrappleDelay <= 0)
        {
            HandleGrappleMovement();
            Ctx.GrappleDelay = Ctx.MaxGrappleDelay;
        }
    }

    public override void LateUpdateState() { }

    public override void FixedUpdateState() { }

    #endregion

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsGrounded && Ctx.FinishedGrapple)
        {
            SwitchState(Factory.Grounded());
        }
        else if (Ctx.IsSloped && Ctx.FinishedGrapple)
        {
            SwitchState(Factory.Sloped());
        }
        else if (!Ctx.IsGrounded && !Ctx.IsSloped && Ctx.FinishedGrapple)
        {
            SwitchState(Factory.Fall());
        }
    }

    private void HandleGrappleMovement()
    {
        Ctx.Rb.velocity = new Vector3(Ctx.Rb.velocity.x, 0, Ctx.Rb.velocity.z);

        Ctx.Rb.AddForce(Ctx.GrappleDirection * Ctx.GrappleSpeed, ForceMode.Impulse);

        Ctx.FinishedGrapple = true;
    }
}
