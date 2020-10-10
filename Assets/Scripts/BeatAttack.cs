using System.Collections;
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
        [SerializeField] public Sprite sprite;
        [SerializeField] public MovementDirection moveDirection;
        [SerializeField] public AudioClip sound;
    }

    [SerializeField] public BeatDamageProperties  [] Damages;        
    
}
