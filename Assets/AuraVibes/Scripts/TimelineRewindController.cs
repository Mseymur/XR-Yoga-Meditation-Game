using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement; // <-- 1. ADD THIS LINE AT THE TOP

public class TimelineRewindController : MonoBehaviour
{
    [Tooltip("The PlayableDirector you want to control. Will be found automatically if not assigned.")]
    public PlayableDirector director;

    private List<double> markerTimes = new List<double>();
    
    // Define our controller buttons
    private OVRInput.Button rewindButton = OVRInput.Button.Two; // 'B' button
    private OVRInput.Button menuButton = OVRInput.Button.One;  // 'A' button

    private const double JUMP_THRESHOLD = 0.2;

    void Awake()
    {
        if (director == null)
        {
            director = GetComponent<PlayableDirector>();
        }
        FindMarkerTimes();
    }
    void Start()
{
    director.Play();
}
    void FindMarkerTimes()
    {
        var timelineAsset = director.playableAsset as TimelineAsset;
        if (timelineAsset == null) return;

        foreach (var track in timelineAsset.GetRootTracks())
        {
            if (track is MarkerTrack markerTrack)
            {
                foreach (var marker in markerTrack.GetMarkers())
                {
                    if (marker is SignalEmitter)
                    {
                        markerTimes.Add(marker.time);
                    }
                }
            }
        }
        markerTimes = markerTimes.OrderBy(t => t).ToList();
        Debug.Log($"Finished. A total of {markerTimes.Count} marker times were registered.");
    }

    void Update()
    {
        // Check for the 'B' button to rewind the timeline
        if (OVRInput.GetDown(rewindButton))
        {
            Debug.Log("Rewind button ('B') pressed on controller!");
            PerformRewind();
        }

        // --- 2. ADD THIS NEW 'IF' BLOCK ---
        // Check for the 'A' button to return to the menu
        if (OVRInput.GetDown(menuButton))
        {
            Debug.Log("Menu button ('A') pressed! Returning to Level Scene.");
            // Make sure the scene name in the quotes is spelled exactly right
            SceneManager.LoadScene("Level Scene");
        }
    }

    private void PerformRewind()
    {
        if (markerTimes.Count == 0) return;

        double currentTime = director.time;
        int currentMarkerIndex = -1;

        for (int i = markerTimes.Count - 1; i >= 0; i--)
        {
            if (currentTime >= markerTimes[i])
            {
                currentMarkerIndex = i;
                break;
            }
        }
        
        if (currentMarkerIndex == -1) return;

        double currentSectionStartTime = markerTimes[currentMarkerIndex];
        int targetMarkerIndex = currentMarkerIndex;

        if (currentTime - currentSectionStartTime <= JUMP_THRESHOLD)
        {
            targetMarkerIndex = Mathf.Max(0, currentMarkerIndex - 1);
        }

        double targetTime = markerTimes[targetMarkerIndex];
        Debug.Log($"Jumping timeline to: {targetTime}s");
        director.time = targetTime;
        
        if (director.state != PlayState.Playing)
        {
            director.Play();
        }
    }
}