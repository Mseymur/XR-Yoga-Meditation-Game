   using UnityEngine;
   using TMPro;

public class Countdown : MonoBehaviour
{
    public float countdownTime = 60f; // Set the countdown duration in seconds
    public TextMeshProUGUI timerText; // Use this for TextMesh Pro

    void Start()
    {
        // Optionally initialize the text
        UpdateTimerText(countdownTime);
    }

    void Update()
    {
        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime; // Decrease the time
            UpdateTimerText(countdownTime);
        }
        else
        {
            // Timer expired
            countdownTime = 0;
            UpdateTimerText(countdownTime);
            // You can trigger other actions here
        }
    }

    void UpdateTimerText(float time)
    {
    
        timerText.text = Mathf.Ceil(time).ToString();
    }
}

public class Cowntdown
{
    
}
