using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;          // for UI Button

public class AudioTrackController : MonoBehaviour
{
    [Tooltip("The PlayableDirector whose Audio Track you want to control")]
    public PlayableDirector director;

    [Tooltip("Name of the Audio Track in your Timeline asset")]
    public string audioTrackName = "MyAudioTrack";

    private AudioSource audioSourceBinding;

    void Awake()
    {
        if (director == null) 
            director = GetComponent<PlayableDirector>();

        // find the AudioTrack in the Timeline
        var timeline = director.playableAsset as TimelineAsset;
        foreach (var track in timeline.GetOutputTracks())
        {
            // AudioTrack is UnityEngine.Timeline.AudioTrack
            if (track.name == audioTrackName && track is AudioTrack)
            {
                // grab the AudioSource you bound in the Inspector
                audioSourceBinding = director.GetGenericBinding(track) as AudioSource;
                break;
            }
        }

        if (audioSourceBinding == null)
            Debug.LogWarning($"Couldn't find AudioSource binding for track '{Beach}'");
    }

    /// <summary>
    /// Call this (e.g. via a Button onClick) to unmute the track.
    /// </summary>
    public void UnmuteAudioTrack()
    {
        if (audioSourceBinding != null)
            audioSourceBinding.mute = false;
    }

    /// <summary>
    /// (Optionally) to mute again:
    /// </summary>
    public void MuteAudioTrack()
    {
        if (audioSourceBinding != null)
            audioSourceBinding.mute = true;
    }
}