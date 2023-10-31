using UnityEngine;

public class CharVaultState : CharBaseState
{
    public CharVaultState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("Vault ENTER");
        Ctx.IsVaulted = true;
        Vector3 offset = new Vector3(Ctx.VaultObj.transform.position.x, Ctx.VaultObj.GetComponent<Renderer>().bounds.max.y + 1f, Ctx.VaultObj.transform.position.z);
        Debug.Log(Ctx.VaultObj.GetComponent<Renderer>().bounds.max);
        Debug.Log(Ctx.VaultObj.GetComponent<Renderer>().bounds.min);
        Ctx.transform.position = offset;
    }

    public override void ExitState()
    {
        Ctx.IsVaulted = false;
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
        if (Ctx.IsGrounded)
        {
            Debug.Log("VAULT > GROUNDED");
            SwitchState(Factory.Grounded());
        }
    }
}
