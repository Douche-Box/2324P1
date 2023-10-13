using UnityEngine;

public class CharWalkState : CharBaseState
{
    public CharWalkState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("WALK ENTER");
        Ctx.DesiredMoveForce = Ctx.MoveSpeed;

        Ctx.PlayerAnimator.SetFloat("Running", Ctx.MovementSpeed);
    }

    public override void ExitState() { }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void LateUpdateState() { }

    public override void FixedUpdateState()
    {
        WalkMovement();
    }

    #endregion

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMove)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsSlide && Ctx.IsMove && !Ctx.IsWalled && Ctx.MoveForce >= Ctx.MoveSpeed)
        {
            Debug.Log("walk > slide");
            SwitchState(Factory.Slide());
        }
    }

    private void WalkMovement()
    {

        Ctx.Rb.AddForce(Ctx.Movement * Ctx.MoveForce * 10f * Ctx.MoveMultiplier, ForceMode.Force);
    }

}