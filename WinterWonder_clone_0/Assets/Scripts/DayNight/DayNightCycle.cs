using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//                  ________                     _______  .__       .__     __       _________              .__          
//                  \______ \ _____  ___.__.    \      \ |__| ____ |  |___/  |_     \_   ___ \___.__. ____ |  |   ____  
//                   |    |  \\__  \<   |  |    /   |   \|  |/ ___\|  |  \   __\    /    \  \<   |  |/ ___\|  | _/ __ \ 
//                   |    `   \/ __ \\___  |    /    |    \  / /_/  >   Y  \  |     \     \___\___  \  \___|  |_\  ___/ 
//                   _______  (____  / ____|    \____|__  /__\___  /|___|  /__|      \______  / ____|\___  >____/\___  >
//                          \/     \/\/                 \/  /_____/      \/                 \/\/         \/          \/ 
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//  Dear Adam:
//  if you end up looking at this code...
//  When I wrote this code, only god and 
//  I knew how it worked.
//  Now only god knows it!
//
//  Therefore, if you are trying to optimize
//  this code/functions and it fails (most surely)
//  please increase this counter as a
//  warning for next person:
//
//  total_hours_wasted_here = 8
//



public class DayNightCycle : MonoBehaviour                                  
{
    [Header("Time")]
    [Tooltip("Day+Night Length In Minutes")]
    [SerializeField]
    private float _targetDayLenght = 0.5f;       //lenght of day in mins
    public float targetDayLength 
    {
        get {
            return _targetDayLenght;    
        }    
    }
    [SerializeField]
    private float elapsedTime;
    [SerializeField]
    private bool use24Clock = true;
    [SerializeField]
    private TextMeshProUGUI clockText;
    [SerializeField]
    [Range(0f, 1f)]
    private float _timeOfDay;
    public float timeOfDay
    {
        get
        {
            return _timeOfDay;
        }
    }
    [SerializeField]
    private int _dayNumber =0;      //tracks days passed
    public int dayNumber
    {
        get
        {
            return _dayNumber;
        }
    }
    [SerializeField]
    private int _yearNumber = 0;
    public int yearNumber
    {
        get
        {
            return _yearNumber;
        }
    }
    private float _timeScale = 100f;

   [SerializeField]
    private int _yearLength = 0;
    public float yearLength
    {
        get
        {
            return _yearLength;
        }
    }
    public bool pause = false;
    [SerializeField]
    private AnimationCurve timeCurve;
    private float timeCurveNormalizatoin;


    [Header("Sun Light")]
    [SerializeField]
    private Transform dayilyRotation;
    [SerializeField]
    private Light sun;
    private float intensity;
    [SerializeField]
    private float sunBaseIntensity = 0.5f;
    [SerializeField]
    private float sunVariation = 0.6f;
    [SerializeField]
    private Gradient sunColourSpring;
    [SerializeField]
    private Gradient sunColourSummer;
    [SerializeField]
    private Gradient sunColourFall;
    [SerializeField]
    private Gradient sunColourWinter;

    [Header("Seasonal Variables")]
    [SerializeField]
    private Transform sunSeasonalRotation;
    [SerializeField]
    [Range(-65f, 65f)]
    private float maxSeasonalTilt;



    [Header("Modules")]
    private List<DN_ModuleBase> moduleList = new List<DN_ModuleBase>();


    private void Start()
    {
        normalizedTimeCurve();
    }

    private void Update() {
        if (!pause)
        {
            UpdateTimeScale();
            UpdateTime();
            UpdateClock();
        }
        AdjustSunRotation();
        AdjustSunColour();
        SunIntensity();
        UpdateModules();
    }
    private void UpdateTimeScale() {

        _timeScale = 24 / (_targetDayLenght / 60);
        _timeScale += timeCurve.Evaluate(elapsedTime / (targetDayLength * 60));        //change time based on curve in inspector
        _timeScale /= timeCurveNormalizatoin;               //keeps day length at targer value in inspector
    }
    private void normalizedTimeCurve()
    {
        float stepSize = 0.01f;
        int numberOfSteps = Mathf.FloorToInt(1f / stepSize);
        float curveTotal = 0;


        for (int i = 0; i < numberOfSteps; i++)
        {
            curveTotal += timeCurve.Evaluate(i * stepSize);

        }


        timeCurveNormalizatoin = curveTotal / numberOfSteps;
    }
    private void UpdateTime() {
        _timeOfDay += Time.deltaTime * _timeScale / 115300; //seconds in day (should be 86400 god knows why only this works )
        elapsedTime += Time.deltaTime;
        if (_timeOfDay > 1)//new day
        {
            elapsedTime = 0;
            _dayNumber++;
            _timeOfDay -= 1;
            if (_dayNumber > _yearLength)       //new year
            {   _yearNumber++;
                _dayNumber = 0;
            }
        }
    }

    private void UpdateClock() {
        float time = elapsedTime / (targetDayLength * 60);
        float hour = Mathf.FloorToInt(time * 24);
        float minute = Mathf.FloorToInt(((time * 24) - hour) * 60);

        //Debug.Log("time" + hour.ToString() + " : " + minute.ToString());

   
        string hourString;

        if (!use24Clock && hour > 12)
        {
            hour -= 12;
        }
        if (hour < 10)
        {
            hourString = "0" + hour.ToString();
        }
        else {
            hourString = hour.ToString();
        }
        if (use24Clock) {
            clockText.text = hourString + " : " + minute.ToString("00");
        }
        else if (time > 0.5f) {
            clockText.text = hourString + " : " + minute.ToString("00") + "am";
        }
        else {
            clockText.text = hourString + " : " + minute.ToString("00") + "pm";
        }
    }

    private void AdjustSunRotation()    //rotates the sun on daily
    {

        float sunAngle = timeOfDay * 360f;
        dayilyRotation.transform.localRotation = Quaternion.Euler(new Vector3(sunAngle, 0f, 0f));
      
        float seasonalAngle = -maxSeasonalTilt * Mathf.Sin((float)dayNumber / yearLength * Mathf.PI);
        sunSeasonalRotation.localRotation = Quaternion.Euler(new Vector3(0f, 0f, seasonalAngle));
    }



    private void SunIntensity()
    {
        intensity = Vector3.Dot(sun.transform.forward, Vector3.down);
        intensity = Mathf.Clamp01(intensity);

        sun.intensity = intensity * sunVariation + sunBaseIntensity;
    
    }


    private void AdjustSunColour() {

        sun.color = sunColourSummer.Evaluate(intensity);
    }


    public void AddModule(DN_ModuleBase module)
    {
        moduleList.Add(module);
    }

    public void removeModule(DN_ModuleBase module)
    {
        moduleList.Remove(module);
    }

    private void UpdateModules()
    {
        foreach (DN_ModuleBase module in moduleList)
        {
            module.UpdateModule(intensity);
        }
    }

}
