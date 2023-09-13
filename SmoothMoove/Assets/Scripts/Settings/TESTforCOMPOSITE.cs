using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
public class TESTforCOMPOSITE : MonoBehaviour
{
    [SerializeField]
    private Char_Controller char_Controller;

    [SerializeField]
    private InputActionReference inputActionReference; //this is on the SO

    [SerializeField]
    private bool excludeMouse = true;
    [SerializeField]
    private int selectedBindingBtn;
    [SerializeField]
    private InputBinding.DisplayStringOptions displayStringOptions;
    [Header("Binding Info - DO NOT EDIT")]
    [SerializeField]
    private InputBinding inputBinding;
    private int bindingIndex;

    private string actionName;

    [Header("UI Fields")]
    [SerializeField]
    private TMP_Text actionText;
    [SerializeField]
    private Button rebindButton;
    [SerializeField]
    private TMP_Text rebindText;
    [SerializeField]
    private Button resetButton;

    private void OnEnable()
    {
        if (char_Controller != null)
        {
            print(char_Controller);
            TESTINPUTforCOMPOSITE.LoadBindingOverride(actionName);
            GetBindingInfo();
            UpdateUI();
        }

        TESTINPUTforCOMPOSITE.rebindComplete += UpdateUI;
        TESTINPUTforCOMPOSITE.rebindCanceled += UpdateUI;
    }

    private void OnDisable()
    {
        TESTINPUTforCOMPOSITE.rebindComplete -= UpdateUI;
        TESTINPUTforCOMPOSITE.rebindCanceled -= UpdateUI;
    }

    private void OnValidate()
    {
        if (char_Controller == null)
            return;

        GetBindingInfo();
        UpdateUI();
    }

    private void GetBindingInfo()
    {
        if (inputActionReference.action != null)
            actionName = inputActionReference.action.name;

        if (inputActionReference.action.bindings.Count > selectedBindingBtn)
        {
            inputBinding = inputActionReference.action.bindings[selectedBindingBtn];
            bindingIndex = selectedBindingBtn;
        }
    }

    private void UpdateUI()
    {
        if (actionText != null)
            actionText.text = actionName;

        if (rebindText != null)
        {
            if (Application.isPlaying)
            {
                print(actionName);
                print(char_Controller);
                print(bindingIndex + " bindingIndex");
                rebindText.text = TESTINPUTforCOMPOSITE.GetBindingName(actionName, bindingIndex);
            }
            else
                rebindText.text = inputActionReference.action.GetBindingDisplayString(bindingIndex);
        }
    }

    public void DoRebind(int btnIndex)
    {
        Debug.Log("hai");
        btnIndex++;
        TESTINPUTforCOMPOSITE.StartRebind(actionName, btnIndex, rebindText, excludeMouse);
    }

    private void ResetBinding(int btnIndex)
    {
        TESTINPUTforCOMPOSITE.ResetBinding(actionName, btnIndex);
        UpdateUI();
    }
}