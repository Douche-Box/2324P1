using UnityEngine;
using UnityEngine.Video;

public class CharGrappledState : CharBaseState
{
    public CharGrappledState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory)
    {
        IsRootState = true;
    }
    Vector3 hookshotdir;
    public override void EnterState()
    {
        InitializeSubState();
        hookshotdir = (Ctx.GrappleHit.point - Ctx.transform.position).normalized;

        Ctx.DesiredMoveForce = Ctx.GrappleSpeed;
    }

    public override void ExitState()
    {

    }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        CheckSwitchStates();
        HandleGrapple();
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

    private void HandleGrapple()
    {
        // Ctx.Rb.velocity = new Vector3(Ctx.Rb.velocity.x, 0f, Ctx.Rb.velocity.z); 

        float GrappleSpeed = Vector3.Distance(Ctx.transform.position, Ctx.GrappleHit.point);
        float GrappleSpeedMultiplier = 5f;

        Ctx.Rb.AddForce(hookshotdir * GrappleSpeed * GrappleSpeedMultiplier * Time.deltaTime, ForceMode.Impulse);

        float reachedPoint = 1f;

        if (Vector3.Distance(Ctx.transform.position, Ctx.GrappleHit.point) < reachedPoint)
        {
            Debug.Log("AH");
        }
    }
}
