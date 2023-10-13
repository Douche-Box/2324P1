using UnityEngine;

public class CharSlideState : CharBaseState
{
    public CharSlideState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory) { }

    public override void EnterState()
    {
        Ctx.DesiredMoveForce = Ctx.SlideSpeed;
        Ctx.MoveForce = Ctx.SlideSpeed;

        Ctx.PlayerObj.localScale = new Vector3(Ctx.PlayerObj.localScale.x + 1, Ctx.PlayerObj.localScale.y, Ctx.PlayerObj.localScale.z + 1);

        Ctx.Rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }

    public override void ExitState()
    {
        Ctx.PlayerObj.localScale = new Vector3(Ctx.PlayerObj.localScale.x - 1, Ctx.PlayerObj.localScale.y, Ctx.PlayerObj.localScale.z - 1);
    }

    #region MonoBehaveiours

    public override void UpdateState()
    {
    }

    public override void FixedUpdateState()
    {
        SlidingMovement();
        CheckSwitchStates();
    }

    public override void LateUpdateState() { }

    #endregion

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMove)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMove && !Ctx.IsSlide)
        {
            Debug.Log("slide > walk 1");
            SwitchState(Factory.Walk());
        }
        else if (Ctx.IsMove && Ctx.IsSlide && Ctx.IsWalled)
        {
            Debug.Log("slide > walk 2");

            SwitchState(Factory.Walk());
        }
        else if (Ctx.MoveForce <= Ctx.LowestSlideSpeed)
        {
            Debug.Log("slide > walk 3");
            SwitchState(Factory.Walk());
        }
    }

    private void SlidingMovement()
    {
        if (!Ctx.IsSloped || Ctx.Rb.velocity.y > 0.1f)
        {
            Ctx.DesiredMoveForce = Ctx.LowestSlideSpeed;
        }

        Ctx.Rb.AddForce(Ctx.Movement * Ctx.MoveForce * 10f * Ctx.MoveMultiplier, ForceMode.Force);
    }
}
