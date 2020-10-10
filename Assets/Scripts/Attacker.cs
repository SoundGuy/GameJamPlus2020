using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : Health
{

    [SerializeField] public  BeatAttack[] _attacks;
    [SerializeField] public  BeatAttack[] _Defenses;
    
    [SerializeField] private Attacker target;

    [SerializeField] public int currentAttack =0;
    
    public int ChoosecurrentAttack
    {
        get { return currentAttack; }
        set
        {
            if (currentAttack == value) return;

            currentAttack = value;
        }
    }
    
    [SerializeField] public int currentDefense =0;
    public void Beat()
    {

        if (!target)
            return;
        // TODO check if attack is less then number of attacks
        
        // TODO check if attacker is dead and return; 

        int NumDamagesATK = _attacks[currentAttack].Damages.Length;
        int currentBeatATK = BeatManager._instance.playedBeat % NumDamagesATK;
        
        
        int NumDamagesDEF = target._Defenses[target.currentDefense].Damages.Length;
        int currentBeatDEF = BeatManager._instance.playedBeat % NumDamagesDEF;
        
        switch (_attacks[currentAttack].Damages[currentBeatATK]._damageType)        
        {
            case BeatAttack.BeatDamageProperties.DamageType.FullDamage:
            {
                switch (target._Defenses[target.currentDefense].Damages[currentBeatDEF]._damageType)
                {
                    case BeatAttack.BeatDamageProperties.DamageType.FullDamage:
                    {
                        // TODO - Make better forumla that also relies on Defense. for example substruct or devide defense.
                        float damageTaken = _attacks[currentAttack].Damages[currentBeatATK].Strentgh; 
                        target.Hit(damageTaken);
                        break;
                    }
                }
                break;
            }

        }

        

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
