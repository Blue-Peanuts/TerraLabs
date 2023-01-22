using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNameGenerator : MonoBehaviour
{
    static string[] WaterPrefixes = {"Aqua", "Hydro", "Fluvi", "Mari", "Ombro" };
    static  string[] LandPrefixes = {"Pedo", "Litho", "Geo", "Terra", "Topo"};
    static  string[] AirPrefixes = {"Aero", "Ptero", "Ornitho", "Anemo", "Atmo"};
    static string[] vowels = {"a", "e", "i", "o", "u"};
    static string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };

    /*void Start()
    {
        Debug.Log(NameGenerator(-1));
        Debug.Log(NameGenerator(0));
        Debug.Log(NameGenerator(1));
    }*/
    public static string NameGenerator(float adaptation)
    {
        string name = "";
        int num = Random.Range(0, 5);
        if (adaptation < -0.3)
        {
            name = WaterPrefixes[num];
        }else if(adaptation <= 0.3){
            name = LandPrefixes[num];
        }else{
            name = AirPrefixes[num];
        }
        int nameLength = Random.Range(1, 4);
        for(int i=1; i<=nameLength; ++i)
        {
            int consonant = Random.Range(0, 21);
            int vowel = Random.Range(0, 5);
            string temp = (string)consonants[consonant] + (string)vowels[vowel];
            name += temp;
        }
        return name;
    }
}
