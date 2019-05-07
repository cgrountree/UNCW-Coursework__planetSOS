using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeText;

    public float seconds, minutes;

    // Start is called before the first frame update
    void Start()
    {
        timeText.text = "00:00";
    }

    // Update is called once per frame
    void Update()
    {
        minutes = (int)(Time.time / 60f);
        seconds = (int)(Time.time % 60f);
        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
