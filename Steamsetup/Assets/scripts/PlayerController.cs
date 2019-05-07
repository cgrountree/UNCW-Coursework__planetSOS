using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private AudioSource theAudio;

    public Text timeText;
    public float seconds, minutes;
    public Text countText;
    public Text winText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        winText.text = "";
        timeText.text = "00:00";
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Ups"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            theAudio = GetComponent<AudioSource>();
            theAudio.Play();
        }
    }

    void SetCountText()
    {
        countText.text = "Orbs: " + count.ToString();
        if (count >= 10)
        {
            winText.text = "You Escaped!";
        }
        
    }

    void Update()
    {
        if (!(winText.text == "You Escaped!"))
        {
            minutes = (int)(Time.time / 60f);
            seconds = (int)(Time.time % 60f);
            timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }
}