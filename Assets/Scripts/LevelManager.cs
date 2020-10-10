using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private int TotalEnemies;
    [SerializeField] private int AliveEnemies;

    [SerializeField] private bool AutoProgress = false;

    [SerializeField] private Button nextButton;
    
    
    public void AnnouceDeath(Health imDead = null)
    {
        AliveEnemies--;
        if (AliveEnemies == 0)
        {
            // enable next level button - or transition to next level
            nextButton.gameObject.SetActive(true);
            
            if (AutoProgress)
            {
                // MOVE to next level
            }           
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        TotalEnemies = 0;
        foreach (Attacker attacker in FindObjectsOfType<Attacker>())
        {
            if (attacker.tag == "Enemy")
            {
                TotalEnemies++;
            }
        }

        AliveEnemies = TotalEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
