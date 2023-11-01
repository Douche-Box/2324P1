using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] float _elapsedTime;
    [SerializeField] TMP_Text _text;
    //hallo

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        int minutes = (int)(Time.timeSinceLevelLoad / 60f) % 60;
        int seconds = (int)(Time.timeSinceLevelLoad % 60f);
        int milliseconds = (int)(Time.timeSinceLevelLoad * 1000f) % 1000;
        _text.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
