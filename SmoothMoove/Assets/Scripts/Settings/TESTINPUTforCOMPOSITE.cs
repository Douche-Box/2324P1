using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TESTINPUTforCOMPOSITE : MonoBehaviour
{
    public static Char_Controller char_Controller;

    public static NewControls inputActions;

    public static event Action rebindComplete;
    public static event Action rebindCanceled;
    public static event Action<InputAction, int> rebindStarted;


    private void OnEnable()
    {
        if (char_Controller == null)
        {
            print("null");
            char_Controller = FindObjectOfType<Char_Controller>();
            print(char_Controller);
        }
    }
    private void Awake()
    {
        if (char_Controller == null)
        {
            print(char_Controller);
            char_Controller = FindObjectOfType<Char_Controller>();
            print(char_Controller);
        }
        if (inputActions == null)
            inputActions = new();
    }

    public static void StartRebind(string actionName, int bindingIndex, TMP_Text statusText, bool excludeMouse)
    {
        Debug.Log("hoi");
        print(char_Controller);
        InputAction action = char_Controller.PlayerInput.actions.FindAction(actionName);
        if (action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.Log("Couldn't find action or binding");
            return;
        }
        Debug.Log("hei");
        if (action.bindings[bindingIndex].isComposite)
        {
            Debug.Log("hui");
            var firstPartIndex = bindingIndex + 1;

            //print(bindingIndex);
            //print(firstPartIndex);

            //print(action.bindings[bindingIndex].isComposite);
            //print(action.bindings[firstPartIndex].isPartOfComposite);

            if (firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isPartOfComposite)
            {
                DoRebind(action, bindingIndex, statusText, true, excludeMouse);
                Debug.Log("hii");
            }
        }
        else
            DoRebind(action, bindingIndex, statusText, false, excludeMouse);
    }

    private static void DoRebind(InputAction actionToRebind, int bindingIndex, TMP_Text statusText, bool allCompositeParts, bool excludeMouse)
    {
        print("boo");
        if (actionToRebind == null || bindingIndex < 0)
            return;
        statusText.text = $"Press a {actionToRebind.expectedControlType}";

        actionToRebind.Disable();

        var rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex);

        rebind.OnComplete(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            if (allCompositeParts)
            {
                var nextBindingIndex = bindingIndex + 1;
                if (nextBindingIndex < actionToRebind.bindings.Count && actionToRebind.bindings[nextBindingIndex].isComposite)
                    DoRebind(actionToRebind, nextBindingIndex, statusText, allCompositeParts, excludeMouse);
            }

            SaveBindingOverride(actionToRebind);
            rebindComplete?.Invoke();
        });

        rebind.OnCancel(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            rebindCanceled?.Invoke();
        });

        rebind.WithCancelingThrough("<Keyboard>/escape");

        if (excludeMouse)
            rebind.WithControlsExcluding("Mouse");

        rebindStarted?.Invoke(actionToRebind, bindingIndex);
        rebind.Start(); //actually starts the rebinding process
    }

    public static string GetBindingName(string actionName, int bindingIndex)
    {
        if (char_Controller == null)
            char_Controller = FindObjectOfType<Char_Controller>();

        print(char_Controller);
        InputAction action = char_Controller.PlayerInput.actions.FindAction(actionName);
        return action.GetBindingDisplayString(bindingIndex);
    }

    private static void SaveBindingOverride(InputAction action)
    {
        for (int i = 0; i < action.bindings.Count; i++)
        {
            PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);
        }
    }

    public static void LoadBindingOverride(string actionName)
    {
        if (inputActions == null)
            inputActions = new NewControls();

        InputAction action = char_Controller.PlayerInput.actions.FindAction(actionName);

        for (int i = 0; i < action.bindings.Count; i++)
        {
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
                action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));
        }
    }

    public static void ResetBinding(string actionName, int bindingIndex)
    {
        InputAction action = char_Controller.PlayerInput.actions.FindAction(actionName);

        if (action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.Log("Could not find action or binding");
            return;
        }

        if (action.bindings[bindingIndex].isComposite)
        {
            for (int i = bindingIndex; i < action.bindings.Count && action.bindings[i].isComposite; i++)
                action.RemoveBindingOverride(i);
        }
        else
            action.RemoveBindingOverride(bindingIndex);

        SaveBindingOverride(action);
    }

}