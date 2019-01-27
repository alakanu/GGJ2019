using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public float MaxDistance;
    public Vector3 Speed;
    public GameObject[] Clouds;
    Vector3[] initialCloudsPosition;

    private void Start()
    {
        initialCloudsPosition = new Vector3[Clouds.Length];
        for (int i = 0; i < Clouds.Length; i++)
        {
            initialCloudsPosition[i] = Clouds[i].transform.position;
        }
    }
    private void Update()
    {
        for (int i = 0; i < Clouds.Length; i++)
        {
            if( (Clouds[i].transform.position - initialCloudsPosition[i]).magnitude > MaxDistance)
            {
                Clouds[i].transform.position = initialCloudsPosition[i] - (Speed.normalized * MaxDistance * 0.95f);
            }

            Clouds[i].transform.position += Speed * Time.deltaTime;
        }
    }
}
