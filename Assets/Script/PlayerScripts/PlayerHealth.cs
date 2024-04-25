using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{   
    public int AbilityTier;
    public Slider HealthBar;
    public int maxhealth = 100;
    public int health;

    public void Damage(int amount)
    {
        health -= amount;
    }

    public int GetQuartile(float value, float maxValue)
    {
        float percentage = value / maxValue;

        if (percentage <= 0.25f)
        {
            return 1;
        }
        else if (percentage <= 0.50f)
        {
            return 2;
        }
        else if (percentage <= 0.75f)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.maxValue = maxhealth;
        health = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.value = health;
        if (health <= 0)
        {
            health = 0;
            Debug.Log("U ded bruh");
        }
        else if (health > maxhealth)
        {
            health = maxhealth;
        }

        //Ability tier
        AbilityTier = GetQuartile(health, maxhealth);
    }
}
