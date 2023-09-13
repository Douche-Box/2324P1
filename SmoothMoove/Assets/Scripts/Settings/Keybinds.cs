using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Keybinds : MonoBehaviour
{
    [SerializeField] private InputActionReference[] inputActionReference;
    [SerializeField] private Char_Controller char_Controller;

    [SerializeField] private GameObject[] binding_OBJ;

    [SerializeField] private List<string> action = new List<string>();
    [SerializeField] private List<GameObject> input_OBJ = new List<GameObject>();
    [SerializeField] private List<TMP_Text> input_TXT = new List<TMP_Text>();
    [SerializeField] private List<GameObject> waitForInput_OBJ = new List<GameObject>();

    [SerializeField] private string[] inputs;
    [SerializeField] private string[] rebinds;

    [SerializeField] private List<GameObject> reset_OBJ = new List<GameObject>();

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    [SerializeField] private bool canSave;



    private string rebindsKey = "rebinds";

    private void Start()
    {
        GetUI();
    }
    private void GetUI()
    {
        for (int i = 0; i < binding_OBJ.Length; i++)
        {
            action.Add(binding_OBJ[i].transform.GetChild(0).GetComponent<TMP_Text>().text);
            input_OBJ.Add(binding_OBJ[i].transform.GetChild(2).gameObject);
            input_TXT.Add(binding_OBJ[i].transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>());
            waitForInput_OBJ.Add(binding_OBJ[i].transform.GetChild(1).gameObject);
            reset_OBJ.Add(binding_OBJ[i].transform.GetChild(3).gameObject);
            LoadBindings(i);
        }
    }
    public void LoadBindings(int indext)
    {

        rebinds[indext] = PlayerPrefs.GetString(rebindsKey + action[indext], rebinds[0]);
        if (string.IsNullOrEmpty(rebinds[indext]))
        {
        }
        else
        {
            char_Controller.PlayerInput.actions[action[indext]].LoadBindingOverridesFromJson(rebinds[indext]);
            LoadUI(indext);
        }
    }
    public void SaveBinding()
    {
        if (canSave)
        {
            for (int i = 0; i < binding_OBJ.Length; i++)
            {
                rebinds[i] = char_Controller.PlayerInput.actions[action[i]].SaveBindingOverridesAsJson();
                PlayerPrefs.SetString(rebindsKey + action[i], rebinds[i]);

                print("CAN SAVE");
            }
        }
        else
        {
            print("CANT SAVE");
        }
    }
    public void LoadUI(int index)
    {
        inputs[index] = InputControlPath.ToHumanReadableString(
                   inputActionReference[index].action.bindings[0].effectivePath,
                   InputControlPath.HumanReadableStringOptions.OmitDevice);

        input_TXT[index].text = inputs[index];

        input_OBJ[index].SetActive(true);
        waitForInput_OBJ[index].SetActive(false);

        CheckDoubles(index);
    }
    private void CheckDoubles(int index)
    {
        for (int i = 0; i < action.Count; i++)
        {
            if (index != i && inputs[index] == inputs[i])
            {
                print(index + " != " + i + " " + " && " + inputs[index] + " == " + inputs[i]);
                print("THE SAME");
                input_TXT[index].text = null;
                inputs[index] = null;
                ResetRebinding(index);
            }
        }
    }
    public void StartRebinding(int BTNIndex)
    {

        input_OBJ[BTNIndex].SetActive(false);
        waitForInput_OBJ[BTNIndex].SetActive(true);

        char_Controller.PlayerInput.SwitchCurrentActionMap("Menu");

        rebindingOperation = inputActionReference[BTNIndex].action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(BTNIndex))
            .Start();
    }
    private void RebindComplete(int button)
    {
        rebindingOperation.Dispose();

        LoadUI(button);

        // DO when not in menu anymore
        char_Controller.PlayerInput.SwitchCurrentActionMap("Game");
    }
    private void ResetRebinding(int BTNIndex)
    {
        inputActionReference[BTNIndex].action.RemoveAllBindingOverrides();

        inputs[BTNIndex] = InputControlPath.ToHumanReadableString(
           inputActionReference[BTNIndex].action.bindings[0].effectivePath,
           InputControlPath.HumanReadableStringOptions.OmitDevice);

        LoadUI(BTNIndex);
    }

}