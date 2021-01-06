using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductorScript : MonoBehaviour
{

    public float bpm = 120;
    public float secPerBeat;
    public float songPosition;
    private float songPositionInBeatsExact;
    private int songPositionInBeats;

    public float lastReportedBeat = 0f;

    public float dspSongTime;
    public float firstBeatOffset;
    public int bruh;
    public AudioSource musicSource;
    public AudioSource metronome_audioSrc;
    public float secPerRealBeat;

    public bool onBeat;

    public float songPosBeat;

    private int timesQuarterBeat;

    //private bool onBeat;

    //DELETE THESE COMMENTS

    public delegate void Beat();
    public static event Beat BeatEvent;

    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource.GetComponent<AudioSource>();
        //Metronome
        //metronome_audioSrc.GetComponent<AudioSource>();
        //Calculate the number of seconds in each beat
        secPerRealBeat = 60f / bpm;
        secPerBeat = 15f / bpm;
        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;
        //Start the music
        //musicSource.time = 10f;
        //musicSource.Play();
    }

    void Update()
    {
        //determine how many seconds since the song started
        //songPosition = (float)(AudioSettings.dspTime - dspSongTime);
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        //determine how many beats since the song started
        songPositionInBeatsExact = songPosition / secPerBeat;
        songPositionInBeats = (int)songPositionInBeatsExact;
        ReportBeat();
        //GameTimeline();

        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log(songPosBeat);
        }

        songPosBeat = (float)songPositionInBeats / 4;

        if (onBeat)
        {
            if (songPosBeat == 1)
            {
                musicSource.Play();
            }
        }
    }

    void ReportBeat()
    {
        if (lastReportedBeat < songPositionInBeats)
        {
            onBeat = true;
            times += 1;
            QuarterBeat();
            lastReportedBeat = songPositionInBeats;
        }
        else
        {
            onBeat = false;
        }
    }

    private int times;
    public void QuarterBeat()
    {
        if (times == 4)
        {
            times = 0;
            FullBeat();
        }
        //Debug.Log("beat");
        //songPosBeat += 0.25f; //DONT USE THIS IT COULD GO OUTTA SYNC
    }

    public void FullBeat()
    {
        BeatEvent();
        //Debug.Log("beat");
        metronome_audioSrc.Play();
    }
}
