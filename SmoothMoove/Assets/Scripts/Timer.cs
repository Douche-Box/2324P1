using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] float _elapsedTime;
    [SerializeField] TMP_Text _text;
    //halloasdf
    bool cantime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            cantime = !cantime;
        }

        if (!cantime)
        {
            _elapsedTime += Time.deltaTime;
        }
        int minutes = (int)(_elapsedTime / 60f) % 60;
        int seconds = (int)(_elapsedTime % 60f);
        int milliseconds = (int)(_elapsedTime * 1000f) % 1000;
        _text.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
