using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour {

    public float timeInDay = 1.0f;
    public Color Day;
    public Color Night;
    public AnimationCurve ColorInterpolationCurve;

    float timer;
    float percentageOfDay; //how far into the day are we changes light intensity



	// Use this for initialization
	void Start () {
        timer = 0.0f;

		
	}
	
	// Update is called once per frame
	void Update () {
        checkTime();
        UpdateLight();
    }

    void UpdateLight()
    {
        Light l = GetComponent<Light>();

        var interpValue = ColorInterpolationCurve.Evaluate(percentageOfDay);
        l.color = Color.Lerp(Day, Night, interpValue);
    }

    void checkTime()
    {
        timer += Time.deltaTime;
        percentageOfDay = timer / timeInDay;
        if(timer > timeInDay)
        {
            timer = 0.0f;
        }
    }

}
