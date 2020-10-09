using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class BeatSignalReciever : MonoBehaviour
{

    [SerializeField] float defIncreasePrec = 2f;
    [SerializeField] float defPrecOfBeat = 0.2f;
    
    [SerializeField] float safetyBuffer = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseSize(float IncreasePrec)
    {
        if (IncreasePrec == 0f)
        {
            IncreasePrec = defIncreasePrec;
        }

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
