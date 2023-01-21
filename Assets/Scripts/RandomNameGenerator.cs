using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNameGenerator : MonoBehaviour
{
    public string[] WaterPrefixes = {"Aqua", "Hydro", "Fluvi", "Mari", "Ombro" };
    public string[] LandPrefixes = {"Pedo", "Litho", "Geo", "Terra", "Topo"};
    public string[] AirPrefixes = {"Aero", "Ptero", "Ornitho", "Anemo", "Atmo"};
    string[] vowels = {"a", "e", "i", "o", "u"};
    string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };

    /*void Start()
    {
        Debug.Log(NameGenerator(-1));
        Debug.Log(NameGenerator(0));
        Debug.Log(NameGenerator(1));
    }*/
    string NameGenerator(float Adaptation)
    {
        string _name = "";
        int num = Random.Range(0, 5);
        if (Adaptation < -0.3)
        {
            _name = WaterPrefixes[num];
        }else if(Adaptation <= 0.3){
            _name = LandPrefixes[num];
        }else{
            _name = AirPrefixes[num];
        }
        int nameLength = Random.Range(1, 4);
        for(int i=1; i<=nameLength; ++i)
        {
            int consonant = Random.Range(0, 21);
            int vowel = Random.Range(0, 5);
            string temp = (string)consonants[consonant] + (string)vowels[vowel];
            _name += temp;
        }
        return _name;
    }
}
