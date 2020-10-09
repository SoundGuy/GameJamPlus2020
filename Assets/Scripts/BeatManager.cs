using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class BeatManager : MonoBehaviour
{
    [SerializeField] private int BMP;
    
    [SerializeField] private UnityEvent BeatAction;

    
    [SerializeField] public static float BeatLength;
    [SerializeField] private float FirstBeat;
    [SerializeField] private float LastBeat;
    [SerializeField] private int playedBeat;
    [SerializeField]  bool beatEnabled = false;
        
    // Start is called before the first frame update
    void Start()
    {
        BeatLength = 60f / BMP;
     }

    public  void StartBeat()
    {
        beatEnabled = true;
        FirstBeat = Time.time;
        playedBeat = 0;
        BeatLength = 60f / BMP;        
        GetComponent<PlayableDirector>().Play();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!beatEnabled)
        {
            return;
            
        }
        
        
        if ((Time.time - FirstBeat ) / (BeatLength) > playedBeat)
        {
            playedBeat++;
            LastBeat = Time.time;
            BeatAction.Invoke();
        }
    }
}
