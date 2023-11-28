using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LshapeRoad : MonoBehaviour
{
    public StrightRoad roadPrefab; // Assign this prefab in the Inspector
    public float turnOffset; // This should be set to the width of the road

    void Start()
    {

        //using the strightroad class as not repeat code keep it simple and more maintianable  DRY (Don't Repeat Yourself) principle

        // Instantiate the first segment of the road
        StrightRoad firstSegment = Instantiate(roadPrefab, transform);
        firstSegment.name = "Road_Segment_1";

        // Instantiate the second segment of the road
        StrightRoad secondSegment = Instantiate(roadPrefab, transform);
        secondSegment.name = "Road_Segment_2";
        //// The second segment should start at the end of the first and then offset by the road's width
        //secondSegment.transform.localPosition = new Vector3(firstSegment.RoadSize.x, 0, firstSegment.RoadSize.z / 2 + turnOffset / 2);
        // Rotate the second segment by 90 degrees to form the L shape
        secondSegment.transform.localRotation = Quaternion.Euler(0, 90, 0);
    }
}
