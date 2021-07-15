using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public virtual void changeText(float speed)
    {
        float s = speed * 3.6f;
        //text.text = Mathf.Clamp(Mathf.Round(s), 0f, 10000f ) + "KM/H";    

        if (speed > 0)
            text.text = Mathf.Round(s) + "KM/H";               
        else
        {
            text.text = (Mathf.Round(s) * -1) + "KM/H";            
        }
        
        
    }
}
