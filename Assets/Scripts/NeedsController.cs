﻿using System;
using UnityEngine;

public class NeedsController : MonoBehaviour
{
    [Range(100,0)]
    public int happiness, energy;
    public int happinessTickRate, energyTickRate;
    public DateTime lastTimeHappy,
           lastTimeGainedEnergy;


    public object PetManager { get; private set; }

    private void Awake()
    {
         Initialize( 100, 100, 2, 1);
    }

    public void Initialize(int happiness, int energy,
    int happinessTickRate, int energyTickRate)
    {
        lastTimeHappy = DateTime.Now;
        lastTimeGainedEnergy = DateTime.Now;
        this.happiness = happiness;
        this.energy = energy;
        this.happinessTickRate = happinessTickRate;
        this.energyTickRate = energyTickRate;
        PetUIController.instance.UpdateImages( happiness, energy);
    }

    public void Initialize( int happiness, int energy,
       int happinessTickRate, int energyTickRate,
    DateTime lastTimeHappy, DateTime lastTimeGainedEnergy)
    {
       this.lastTimeHappy = lastTimeHappy;
        this.lastTimeGainedEnergy = lastTimeGainedEnergy;

        this.happiness = happiness
             - happinessTickRate
                * TickAmountSinceLastTimeToCurrentTime(lastTimeHappy, TimingManager.instance.hourLength);

        this.energy = energy
            - energyTickRate
            * TickAmountSinceLastTimeToCurrentTime(lastTimeGainedEnergy, TimingManager.instance.hourLength);

        this.happinessTickRate = happinessTickRate;
        this.energyTickRate = energyTickRate;
        if (this.happiness < 0) this.happiness = 0;
        if (this.energy < 0) this.energy = 0;
        PetUIController.instance.UpdateImages( this.happiness, this.energy);
    }

    private void Update()
    {
        if (TimingManager.instance.gameHourTimer < 0)
        {
            ChangeHappiness(-happinessTickRate);
            ChangeEnergy(-energyTickRate);
            PetUIController.instance.UpdateImages(happiness, energy);
        }
    }

    public void ChangeHappiness(int amount)
    {
        happiness += amount;
        if (amount > 0)
        {
            lastTimeHappy = DateTime.Now;
        }
        if (happiness < 0)
        {
            //PetManager.instance.Die();
        }
        else if (happiness > 100) happiness = 100;
    }

    public void ChangeEnergy(int amount)
    {
        energy += amount;
        if (amount > 0)
        {
            lastTimeGainedEnergy = DateTime.Now;
        }
        if (energy < 0)
        {
            //PetManager.instance.Die();
        }
        else if (energy > 100) energy = 100;
    }

    public int TickAmountSinceLastTimeToCurrentTime(DateTime lastTime, float tickRateInSeconds)
    {
        DateTime currentDateTime = DateTime.Now;
        int dayOfYearDifference = currentDateTime.DayOfYear - lastTime.DayOfYear;
        if (currentDateTime.Year > lastTime.Year
            || dayOfYearDifference >= 7) return 1500;
        int dayDifferenceSecondsAmount = dayOfYearDifference * 86400;
        if (dayOfYearDifference > 0) return Mathf.RoundToInt(dayDifferenceSecondsAmount / tickRateInSeconds);

        int hourDifferenceSecondsAmount = (currentDateTime.Hour - lastTime.Hour) * 3600;
        if (hourDifferenceSecondsAmount > 0) return Mathf.RoundToInt(hourDifferenceSecondsAmount / tickRateInSeconds);

        int minuteDifferenceSecondsAmount = (currentDateTime.Minute - lastTime.Minute) * 60;
        if (minuteDifferenceSecondsAmount > 0) return Mathf.RoundToInt(minuteDifferenceSecondsAmount / tickRateInSeconds);

        int secondDifferenceAmount = currentDateTime.Second - lastTime.Second;
        return Mathf.RoundToInt(secondDifferenceAmount / tickRateInSeconds);
    }
}
