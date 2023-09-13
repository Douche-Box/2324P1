using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Char_Controller : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput = null;
    //public Keybinds keybinds;
    public PlayerInput PlayerInput => playerInput;

    [Header("Inputs")]
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private bool jumpInput;
    [SerializeField] private bool blockInput;
    [SerializeField] private bool pushInput;


    private void OnEnable()
    {
        playerInput.actions.FindAction("Move").performed += w => moveInput = w.ReadValue<Vector2>();
        playerInput.actions.FindAction("Move").canceled += s => moveInput = new Vector2(0, 0);

        playerInput.actions.FindAction("Jump").performed += a => jumpInput = true;
        playerInput.actions.FindAction("Jump").canceled += a => jumpInput = false;

        playerInput.actions.FindAction("Block").performed += b => blockInput = true;
        playerInput.actions.FindAction("Block").canceled += b => blockInput = false;

        playerInput.actions.FindAction("Push").performed += c => pushInput = true;
        playerInput.actions.FindAction("Push").canceled += c => pushInput = false;
    }

    private void Update()
    {
        if (jumpInput)
        {
            print("Jump");
        }
        if (blockInput)
        {
            print("Block");
        }
        if (pushInput)
        {
            print("Push");
        }
    }
}