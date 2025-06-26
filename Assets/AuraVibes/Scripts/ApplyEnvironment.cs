using UnityEngine;
using UnityEngine.Playables;    // Required for controlling the Timeline
using UnityEngine.Timeline;      // Required for accessing Timeline tracks
using System.Collections.Generic;

// We create a special data container to link a skybox to an audio track name.
// [System.Serializable] makes it show up in the Inspector.
[System.Serializable]
public class EnvironmentAudioLink
{
    public string environmentName; // Just for organization in the Inspector
    public Material skyboxMaterial;
    public string audioTrackName;
}

public class ApplyEnvironment : MonoBehaviour
{
    [Tooltip("Drag your PlayableDirector GameObject here.")]
    public PlayableDirector director;

    [Tooltip("Link each skybox material to its matching audio track name from the Timeline.")]
    public List<EnvironmentAudioLink> environmentLinks;


    void Awake()
    {
        // --- 1. SET THE SKYBOX (Existing Logic) ---
        if (EnvironmentData.selectedSkyboxMaterial != null)
        {
            RenderSettings.skybox = EnvironmentData.selectedSkyboxMaterial;
            DynamicGI.UpdateEnvironment();
            Debug.Log("Applied skybox: " + EnvironmentData.selectedSkyboxMaterial.name);

            // --- 2. SET THE AUDIO (New Logic) ---
            SetupAudioTracks();
        }
        else
        {
            Debug.LogWarning("No environment data was passed to this scene. Using default skybox and audio.");
        }
    }

    private void SetupAudioTracks()
    {
        // Find the correct audio track name for the selected skybox
        string targetAudioTrackName = "";
        foreach (var link in environmentLinks)
        {
            if (link.skyboxMaterial == EnvironmentData.selectedSkyboxMaterial)
            {
                targetAudioTrackName = link.audioTrackName;
                break;
            }
        }

        if (string.IsNullOrEmpty(targetAudioTrackName))
        {
            Debug.LogWarning("Could not find a matching audio track for the selected skybox. All audio will be muted.");
        }
        else
        {
            Debug.Log($"Target audio track to unmute: '{targetAudioTrackName}'");
        }

        // Now, loop through all tracks in the timeline
        var timelineAsset = director.playableAsset as TimelineAsset;
        foreach (var track in timelineAsset.GetRootTracks())
        {
            // Check if the track is an AudioTrack
            if (track is AudioTrack audioTrack)
            {
                // Is this our target track?
                if (audioTrack.name == targetAudioTrackName)
                {
                    // Unmute it!
                    audioTrack.muted = false;
                    Debug.Log($"SUCCESS: Unmuted track '{audioTrack.name}'.");
                }
                else
                {
                    // Otherwise, mute all other audio tracks.
                    audioTrack.muted = true;
                    Debug.Log($"Muted track '{audioTrack.name}'.");
                }
            }
        }
    }
}