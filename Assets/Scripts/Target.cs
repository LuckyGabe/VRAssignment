using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float fHealth;
    public int iMaxHealth;
    public bool bIsDead;

    // Start is called before the first frame update
    void Start()
    {
        fHealth = iMaxHealth;

    }

    //Method that deacrease the health
    public void DealDamage(float damage)
    {
        if(bIsDead) return;
        if (fHealth - damage <= 0)
        {
            fHealth = 0;
            bIsDead = true;
        }
        else
        {
            fHealth -= damage;
        }
    }

}
