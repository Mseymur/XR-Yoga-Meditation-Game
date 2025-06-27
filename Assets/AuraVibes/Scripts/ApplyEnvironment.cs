using UnityEngine;
using UnityEngine.Playables;    // For PlayableDirector
using UnityEngine.Timeline;     // For TimelineAsset, AudioTrack
using System.Collections.Generic;

[System.Serializable]
public class EnvironmentAudioLink
{
    public string environmentName;
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
        if (EnvironmentData.selectedSkyboxMaterial != null)
        {
            // 1) Apply skybox
            RenderSettings.skybox = EnvironmentData.selectedSkyboxMaterial;
            DynamicGI.UpdateEnvironment();
            Debug.Log("Applied skybox: " + EnvironmentData.selectedSkyboxMaterial.name);

            // 2) Mute/unmute AudioSources bound to your Timeline’s AudioTracks
            SetupAudioTracks();

            // 3) Force the graph to pick up new volumes immediately
            director.Evaluate();
        }
        else
        {
            Debug.LogWarning("No environment data was passed. Using default skybox and audio.");
        }
    }

   private void SetupAudioTracks()
{
    // 1) Figure out which track name should stay audible
    string targetName = null;
    foreach (var link in environmentLinks)
    {
        if (link.skyboxMaterial == EnvironmentData.selectedSkyboxMaterial)
        {
            targetName = link.audioTrackName;
            break;
        }
    }

    if (string.IsNullOrEmpty(targetName))
        Debug.LogWarning("No matching audio track found; all audio will be muted.");
    else
        Debug.Log($"Keeping '{targetName}' audible, muting all others.");

    // 2) Grab the TimelineAsset
    var timeline = director.playableAsset as TimelineAsset;
    if (timeline == null)
    {
        Debug.LogError("PlayableDirector has no TimelineAsset!");
        return;
    }

    // 3) Walk every AudioTrack in the timeline
    foreach (var track in timeline.GetRootTracks())
    {
        if (!(track is AudioTrack audioTrack))
            continue;

        bool isTarget = audioTrack.name == targetName;

        // 4) Grab whatever AudioSource (or GameObject with one) was bound to this track
        var binding = director.GetGenericBinding(audioTrack);
        AudioSource src = null;
        if (binding is AudioSource directSrc)
        {
            src = directSrc;
        }
        else if (binding is GameObject go)
        {
            src = go.GetComponent<AudioSource>();
        }

        // 5) Mute or unmute the source
        if (src != null)
        {
            src.volume = isTarget ? 1f : 0f;
            Debug.Log($"{(isTarget ? "Unmuted" : "Muted")} '{audioTrack.name}'");
        }
        else
        {
            Debug.LogWarning($"No AudioSource bound to track '{audioTrack.name}'");
        }
    }

    // 6) Push changes into the PlayableGraph right away
    director.Evaluate();
}
}