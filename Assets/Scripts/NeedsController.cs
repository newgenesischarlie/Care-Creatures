using System;
using UnityEngine;

public class NeedsController : MonoBehaviour
{
    [Range(100,0)]
    public int happiness, energy;
    public int happinessTickRate, energyTickRate;
    public DateTime lastTimeHappy,
           lastTimeGainedEnergy;

    [Header("Decay Settings")]
    public float decayInterval = 10f;
    private float decayTimer;

    public object PetManager { get; private set; }

    private void Awake()
    {
         Initialize( 100, 100, 2, 1);
         decayTimer = decayInterval;
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
        decayTimer -= Time.deltaTime;
        
        if (decayTimer <= 0f)
        {
            ChangeHappiness(-happinessTickRate);
            ChangeEnergy(-energyTickRate);
            PetUIController.instance.UpdateImages(happiness, energy);
            
            decayTimer = decayInterval;
        }
        
        if (TimingManager.instance != null && TimingManager.instance.gameHourTimer < 0)
        {
            ChangeHappiness(-happinessTickRate);
            ChangeEnergy(-energyTickRate);
            PetUIController.instance.UpdateImages(happiness, energy);
        }
    }

    public void ChangeHappiness(int amount)
    {
        int previousHappiness = happiness;
        happiness += amount;
        
        if (amount > 0)
        {
            lastTimeHappy = DateTime.Now;
            Debug.Log($"Happiness increased by {amount}! Current happiness: {happiness}/100 (was {previousHappiness}/100)");
        }
        else if (amount < 0)
        {
            Debug.Log($"Happiness decreased by {Mathf.Abs(amount)}. Current happiness: {happiness}/100 (was {previousHappiness}/100)");
        }
        
        if (happiness < 0)
        {
            happiness = 0;
            Debug.LogWarning("Happiness reached 0!");
        }
        else if (happiness > 100)
        {
            happiness = 100;
        }
    }

    public void ChangeEnergy(int amount)
    {
        int previousEnergy = energy;
        energy += amount;
        
        if (amount > 0)
        {
            lastTimeGainedEnergy = DateTime.Now;
            Debug.Log($"Energy increased by {amount}! Current energy: {energy}/100 (was {previousEnergy}/100)");
        }
        else if (amount < 0)
        {
            Debug.Log($"Energy decreased by {Mathf.Abs(amount)}. Current energy: {energy}/100 (was {previousEnergy}/100)");
        }
        
        if (energy < 0)
        {
            energy = 0;
            Debug.LogWarning("Energy reached 0!");
        }
        else if (energy > 100)
        {
            energy = 100;
        }
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
