using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;
using TMPro; // Using TextMeshPro is recommended
using System.Collections.Generic;

public class TimelineRewindController : MonoBehaviour
{
    [Tooltip("The PlayableDirector you want to control. Will be found automatically if not assigned.")]
    public PlayableDirector director;

    [Header("UI Countdown")]
    [Tooltip("The UI Text component that will show the remaining time.")]
    public TextMeshProUGUI countdownText;

    private List<double> markerTimes = new List<double>();
    private double timelineDuration;

    // Controller buttons
    private OVRInput.Button rewindButton = OVRInput.Button.Two; // 'B'
    private OVRInput.Button menuButton = OVRInput.Button.One;   // 'A'

    private const double JUMP_THRESHOLD = 0.2;

    // Awake is now only for finding components
    void Awake()
    {
        if (director == null) director = GetComponent<PlayableDirector>();
    }

    // Start is for initialization that depends on other objects being ready
    void Start()
    {
        FindMarkerTimes();

        // --- THE FIX IS HERE ---
        // We get the duration in Start(), when the asset is guaranteed to be loaded.
        if (director.playableAsset != null)
        {
            timelineDuration = director.playableAsset.duration;
            // This debug log is crucial for testing!
            Debug.Log($"Timeline duration successfully registered as: {timelineDuration} seconds.");
        }
        else
        {
            Debug.LogError("PlayableDirector has no PlayableAsset assigned! Cannot get duration.");
        }

        director.Play();
    }

    void Update()
    {
        // rewind
        if (OVRInput.GetDown(rewindButton))
            PerformRewind();

        // menu
        if (OVRInput.GetDown(menuButton))
            SceneManager.LoadScene("Nature");

        // countdown
        UpdateCountdownUI();
    }

    private void UpdateCountdownUI()
    {
        if (countdownText == null) return;
        if (timelineDuration <= 0) return; // Don't run if duration is invalid

        double remaining = timelineDuration - director.time;
        if (remaining < 0) remaining = 0;

        // format as MM:SS
        int minutes = Mathf.FloorToInt((float)remaining / 60f);
        int seconds = Mathf.FloorToInt((float)remaining % 60f);

        countdownText.text = $"{minutes:00}:{seconds:00}";
    }

    void FindMarkerTimes()
    {
        var timelineAsset = director.playableAsset as TimelineAsset;
        if (timelineAsset == null) return;

        foreach (var track in timelineAsset.GetRootTracks())
            if (track is MarkerTrack markerTrack)
                foreach (var marker in markerTrack.GetMarkers())
                    if (marker is SignalEmitter)
                        markerTimes.Add(marker.time);

        markerTimes.Sort();
        Debug.Log($"Registered {markerTimes.Count} marker times.");
    }

    private void PerformRewind()
    {
        if (markerTimes.Count == 0) return;

        double currentTime = director.time;
        int idx = markerTimes.FindLastIndex(t => t <= currentTime);
        if (idx < 0) return;

        double segmentStart = markerTimes[idx];
        int targetIdx = (currentTime - segmentStart <= JUMP_THRESHOLD)
                        ? Mathf.Max(0, idx - 1)
                        : idx;

        director.time = markerTimes[targetIdx];
        if (director.state != PlayState.Playing)
            director.Play();
    }
}