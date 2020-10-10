using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class BeatSignalReciever : MonoBehaviour
{

    [SerializeField] public  float defIncreasePrec = 2f;
    [SerializeField] public  float defPrecOfBeat = 0.2f;
    
    [SerializeField] public float safetyBuffer = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Beat()
    {
        IncreaseSize();
        // TODO: Make this a subscription. 
        foreach (Attacker attacker in FindObjectsOfType<Attacker>())
        {
            attacker.Beat();            
        }
    }

    public void IncreaseSize()
    {
        
        float IncreasePrec = defIncreasePrec;
        float PrecOfBeat = defPrecOfBeat;
        
        
        Vector3 origScale = gameObject.transform.localScale;
        Vector3 toScale = origScale * IncreasePrec;
        float UpLength = BeatManager.BeatLength * PrecOfBeat * (1f- safetyBuffer);
        float DownLength = BeatManager.BeatLength * (1f -PrecOfBeat) * (1f - safetyBuffer);
        LeanTween.scale(gameObject, toScale, UpLength).setOnComplete(() =>
            {
                LeanTween.scale(gameObject, origScale, DownLength);
            });
    }
}
