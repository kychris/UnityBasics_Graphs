using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    [SerializeField]
    FunctionLibrary.FunctionName function = default;

    Transform[] points;

    void Awake()
    {
        // Step between each point
        float step = 2f / resolution;

        // Transform variables
        Vector3 position = new Vector3(0, 0, 0);
        var scale = Vector3.one * step;
        points = new Transform[resolution * resolution];

        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            {
                // Reset x, Increment z when reach row end
                if (x == resolution)
                {
                    x = 0;
                    z += 1;
                }

                // Create point in scene
                Transform point = Instantiate(pointPrefab);

                // Define point coordinates
                position.x = (x + 0.5f) * step - 1f;
                position.z = (z + 0.5f) * step - 1f;

                // Assign location and scale
                point.localPosition = position;
                point.localScale = scale;
                point.SetParent(transform, false);
                points[i] = point;
            }
        }
    }

    void Update()
    {
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
        float time = Time.time;

        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = f(position.x, position.z, time);
            point.localPosition = position;
        }
    }
}