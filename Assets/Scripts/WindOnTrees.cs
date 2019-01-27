using UnityEngine;

public class WindOnTrees : MonoBehaviour
{
    float offset;
    float frequency;
    AnimationCurve curve;
    Quaternion maxDeltaRotation;
    Quaternion baseRotation;
    Quaternion maxRotation;

    private void Start()
    {
        offset = Random.value / 10f;
        WindOnTreeManager manager = GameObject.FindObjectOfType<WindOnTreeManager>();
        manager.AddToManager(this, out frequency, out curve, out maxDeltaRotation);
        baseRotation = this.transform.rotation;
        maxRotation = baseRotation * maxDeltaRotation;
    }

    public void BlowTheWind(float value)
    {
        value += offset;
        if (offset > 1.0)
            value -= 1.0f;
        this.transform.rotation = Quaternion.Lerp(baseRotation, maxRotation, curve.Evaluate(value));
    }
}
