
using UnityEngine;

public class BitsConsumption : MonoBehaviour
{
    private void Update()
    {
        GameObject g = Utility.FindNearestTaggedObject("Giblet", transform.position, 0.1f);
        if (g)
        {
            gameObject.GetComponent<Energy>().Give(GetComponent<Energy>().energyLevel, g.GetComponent<Energy>());
        }
    }
}
