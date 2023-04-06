using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fun : MonoBehaviour
{
    [SerializeField] Image secondsFill;
    [SerializeField] Image minutesFill;
    [SerializeField] Image hoursFill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var time = System.DateTime.Now;
        int seconds = time.Second;
        secondsFill.fillAmount = (seconds / 60.0f);
        int min = time.Minute;
        minutesFill.fillAmount = (min / 60.0f);
        int hour = time.Hour;
        hoursFill.fillAmount = (hour / 60.0f);
    }
}
