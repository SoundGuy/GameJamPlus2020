using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class BeatManager : MonoBehaviour
{

    public static BeatManager _instance;
    [SerializeField] private int BMP;
    
    [SerializeField] private UnityEvent BeatAction;

    [SerializeField] private float startDelay = 5f;
    
    [SerializeField] public static float BeatLength;
    [SerializeField] private float LevelLoadTime;
    [SerializeField] private float FirstBeat;
    [SerializeField] private float LastBeat;
    [SerializeField] public int playedBeat;
    [SerializeField]  bool beatEnabled = false;
    [SerializeField] private Button startButton;
        
    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        BeatLength = 60f / BMP;
        LevelLoadTime = Time.time;
    }

    public  void StartBeat()
    {
        beatEnabled = true;
        FirstBeat = Time.time;
        playedBeat = 0;
        BeatLength = 60f / BMP;        
        //GetComponent<PlayableDirector>().Play();
        foreach (AudioSource audioSource  in GetComponents<AudioSource>())
        {
            audioSource.Play();    
        }
        
        if (startButton)
        {
            startButton.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {                        
        if (!beatEnabled)
        {
            if (Time.time - LevelLoadTime > startDelay)
            {
                StartBeat();
            } else {

                return;
            }

        }
        
        
        if ((Time.time - FirstBeat ) / (BeatLength) > playedBeat)
        {
            playedBeat++;
            LastBeat = Time.time;
            BeatAction.Invoke();
        }
    }
}
