using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    [SerializeField] TimeManager _timeManager;
    [SerializeField] DeathManager _deathManager;

    [SerializeField] private PlayerInput playerInput = null;
    public PlayerInput PlayerInput => playerInput;

    [SerializeField] bool _isReset;

    [SerializeField] bool _isNext;
    [SerializeField] float _nextTimer;
    [SerializeField] float _maxNextTimer;
    [SerializeField] bool _canNext;

    [SerializeField] CharStateMachine _player;

    [SerializeField] int checkPointInt;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _player = FindObjectOfType<CharStateMachine>();

        playerInput.actions.FindAction("Reset").started += OnReset;
        playerInput.actions.FindAction("Reset").performed += OnReset;
        playerInput.actions.FindAction("Reset").canceled += OnReset;

        playerInput.actions.FindAction("Next").started += OnNext;
        playerInput.actions.FindAction("Next").performed += OnNext;
        playerInput.actions.FindAction("Next").canceled += OnNext;
    }

    private void Update()
    {
        if (_player.IsMove)
        {
            _timeManager.CanTime = true;
        }

        if (_isReset)
        {
            _deathManager.DoDeath();
        }



        if (_isNext && _canNext)
        {
            _nextTimer = _maxNextTimer;
            _canNext = false;
            if (checkPointInt >= _deathManager._checkPointsList.Count - 1)
            {
                checkPointInt = 0;
            }
            else
            {
                checkPointInt++;
            }
            _player.transform.position = _deathManager._checkPointsList[checkPointInt].position;
            _player.transform.forward = _deathManager._checkPointsList[checkPointInt].forward;

        }

        if (_nextTimer <= 0)
        {
            _canNext = true;
            _nextTimer = 0;
        }
        if (!_canNext)
        {
            _nextTimer -= Time.deltaTime;
        }
    }

    void OnReset(InputAction.CallbackContext context)
    {
        _isReset = context.ReadValueAsButton();
    }

    void OnNext(InputAction.CallbackContext context)
    {
        _isNext = context.ReadValueAsButton();
    }



    public void LoadLevel(string scene)
    {
        _timeManager.CanTime = false;
        Debug.Log(_deathManager.DeathCount);
        SceneManager.LoadScene(scene);
    }
}