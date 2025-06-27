using UnityEngine;
using UnityEngine.Playables;    // Required for the PlayableDirector
using TMPro;                   // Required for TextMeshPro
using UnityEngine.SceneManagement; // Optional: if you want to load a scene when the timer ends

public class MeditationTimeline : MonoBehaviour
{
    [Header("Connections")]
    [Tooltip("The timeline you want to play during the countdown.")]
    public PlayableDirector director;

    [Tooltip("The UI Text element that will display the countdown.")]
    public TextMeshProUGUI countdownText;

    [Header("Timer Settings")]
    [Tooltip("The total duration of the countdown in minutes.")]
    public float countdownMinutes = 5.0f;

    // Internal timer state
    private float timeRemainingInSeconds;
    private bool isTimerRunning = false;
    private OVRInput.Button menuButton = OVRInput.Button.One;   // 'A'

    void Start()
    {
        // 1. Set the total duration from our public variable
        timeRemainingInSeconds = countdownMinutes * 60f;
        isTimerRunning = true;

        // 2. Play the associated timeline, if one is assigned
        if (director != null)
        {
            director.Play();
        }

        // 3. Immediately update the UI to show the starting time (e.g., "05:00")
        UpdateCountdownUI();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemainingInSeconds > 0)
            {
                // Subtract the time that has passed since the last frame
                timeRemainingInSeconds -= Time.deltaTime;
                UpdateCountdownUI();
            }
            else
            {
                // Timer has reached zero
                timeRemainingInSeconds = 0;
                isTimerRunning = false;
                UpdateCountdownUI(); // Update one last time to show "00:00"
                OnTimerEnd();
            }
        }
                // menu
        if (OVRInput.GetDown(menuButton))
            SceneManager.LoadScene("Nature");
    }

    private void UpdateCountdownUI()
    {
        if (countdownText == null) return; // Do nothing if no text object is assigned

        // Calculate minutes and seconds from the remaining time
        int minutes = Mathf.FloorToInt(timeRemainingInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeRemainingInSeconds % 60f);

        // Format the string as "MM:SS" and update the text
        countdownText.text = $"{minutes:00}:{seconds:00}";
    }

    private void OnTimerEnd()
    {
        Debug.Log("Countdown finished!");

        // Stop the timeline
        if (director != null)
        {
            director.Stop();
        }

        // OPTIONAL: You could add code here to do something else, like:
        // - Show a "Session Complete" message
        // - Load another scene: SceneManager.LoadScene("ResultsScene");
    }
}