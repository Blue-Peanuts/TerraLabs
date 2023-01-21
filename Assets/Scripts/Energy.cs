using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public int energyLevel = 70;
    public int maxLevel = 100;

    public void Drain(int drainAmt, Energy target)
    {
        int targetDrainAmt = drainAmt;
        int thisDrainGain = drainAmt;
        int realDrain = drainAmt;

        // Check how much to drain/gain
        if (target.energyLevel - drainAmt <= 0)
        {
            targetDrainAmt = target.energyLevel;
        }

         if (this.energyLevel + drainAmt >= this.maxLevel)
        {
            thisDrainGain = this.maxLevel - this.energyLevel;
        }

        // Find minimum drain amount
        realDrain = Mathf.Min(targetDrainAmt, thisDrainGain);

        // Set new energy levels
        target.energyLevel -= realDrain;
        this.energyLevel += realDrain;

        //Destroy if energy is 0
        if (target.energyLevel == 0)
        {
            Destroy(target.gameObject);
        }
    }

    public void Give(int drainAmt, Energy target)
    {
        target.Drain(drainAmt, this);
    }
}

