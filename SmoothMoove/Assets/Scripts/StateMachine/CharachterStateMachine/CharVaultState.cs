using UnityEngine;

public class CharVaultState : CharBaseState
{
    public CharVaultState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        // Debug.Log("Vault ENTER");
        Ctx.IsVaulted = true;


        float yOffset = Ctx.VaultObj.GetComponent<Renderer>().bounds.max.y + 1f;
        float xOffset = Mathf.Abs(Ctx.transform.forward.x) > Mathf.Abs(Ctx.transform.forward.z) ? (Ctx.VaultObj.transform.position.x - Ctx.transform.position.x) : 0f;
        float zOffset = Mathf.Abs(Ctx.transform.forward.z) > Mathf.Abs(Ctx.transform.forward.x) ? (Ctx.VaultObj.transform.position.z - Ctx.transform.position.z) : 0f;

        Vector3 newPosition = new Vector3(Ctx.transform.position.x + xOffset, yOffset, Ctx.transform.position.z + zOffset);
        Ctx.transform.position = newPosition;
        Debug.Log("vaultanimationfor");
        Ctx.PlayerAnimator.SetBool("Vault", true);
        Debug.Log("vaultanimationaft");
    }

    public override void ExitState()
    {
        Ctx.IsVaulted = false;
        Ctx.PlayerAnimator.SetBool("Vault", false);
    }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
    }

    public override void LateUpdateState() { }

    #endregion

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsGrounded && Input.GetKeyDown(KeyCode.P))
        {
            SwitchState(Factory.Grounded());
        }
    }
}
