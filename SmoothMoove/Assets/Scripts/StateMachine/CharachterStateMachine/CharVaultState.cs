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
        // Ctx.Rb.useGravity = false;
        Vector3 offset = new Vector3(Ctx.transform.position.x, Ctx.VaultObj.GetComponent<Renderer>().bounds.max.y + 1f, Ctx.transform.position.z);
        // Debug.Log(Ctx.VaultObj.GetComponent<Renderer>().bounds.max);
        // Debug.Log(Ctx.VaultObj.GetComponent<Renderer>().bounds.min);
        Ctx.transform.position = offset;
        // Ctx.transform.position = Vector3.Slerp(Ctx.transform.position, offset, 8f * Time.deltaTime);
        // Ctx.Rb.useGravity = true;
    }

    public override void ExitState()
    {
        Ctx.IsVaulted = false;
    }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        // Vector3 offset = new Vector3(Ctx.transform.position.x, Ctx.VaultObj.GetComponent<Renderer>().bounds.max.y + 1f, Ctx.transform.position.z);
        // Ctx.transform.position = offset;
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
            SwitchState(Factory.Grounded());
        }
    }
}
