using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float CurrentTime;
    public bool CountingDown;
    public TextMeshProUGUI TimerText;
    void Start()
    {
        
    }
    void Update()
    {
        CurrentTime = CountingDown ? CurrentTime -= Time.deltaTime : CurrentTime += Time.deltaTime;
        TimerText.text = CurrentTime.ToString("0.00");
    }
}
