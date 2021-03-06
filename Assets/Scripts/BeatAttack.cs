﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BeatAttack : ScriptableObject
{
        
    [System.Serializable]
    public class BeatDamageProperties
    {
        public enum DamageType
        {
            NoDamage,
            FullDamage,
            Loop,
            Move,
        }


        [SerializeField] public DamageType _damageType;
        [SerializeField] public float Strentgh;
        [SerializeField] public float Range;
        [SerializeField] public Sprite sprite;
        [SerializeField] public MovementDirection moveDirection;
        [SerializeField] public AudioClip [] sound;
        [SerializeField] public AudioClip [] HitSuccessSound;
        [SerializeField] public AudioClip [] HitFailSound;
    }

    [SerializeField] public BeatDamageProperties  [] Damages;        
    
}
