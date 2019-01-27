using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindOnTreeManager : MonoBehaviour
{
    public float SwingTime = 2.0f;
    public AnimationCurve animationCurve;
    public Vector3 MaxRotation = new Vector3(5f,0f,0f);

    Quaternion MaxRotationQuaternion;

    System.Diagnostics.Stopwatch _stopwatch = new System.Diagnostics.Stopwatch();

    private void Start()
    {
    }

    List<WindOnTrees> managedList = new List<WindOnTrees>();

    public void AddToManager(WindOnTrees managed, out float frequency, out AnimationCurve curve, out Quaternion maxRotation)
    {
        managedList.Add(managed);
        curve = animationCurve;
        frequency = SwingTime;
        MaxRotationQuaternion = Quaternion.Euler(MaxRotation);
        maxRotation = MaxRotationQuaternion;
    }

    private void Update()
    {
        _stopwatch.Reset();
        _stopwatch.Start();
        for (int i = _lastValidIndex; i < managedList.Count; i++)
        {
            if(_stopwatch.ElapsedMilliseconds > 2.0f)
            {
                _lastValidIndex = i;
                _stopwatch.Stop();
                return;
            }
            managedList[i].BlowTheWind((Time.realtimeSinceStartup % SwingTime) / SwingTime);
        }
        _lastValidIndex = 0;
        _stopwatch.Stop();
    }

    int _lastValidIndex = 0;
}

