using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProjectUtils
{
    private static List<GameObject> TrackedDontDestroyOnLoadObjects = new List<GameObject>();
    
    public static void DontDestroyOnLoadTracked(GameObject objectToTrack)
    {
        UnityEngine.Object.DontDestroyOnLoad(objectToTrack);
        TrackedDontDestroyOnLoadObjects.Add(objectToTrack);
    }

    public static List<GameObject> GetAllTrackedDontDestroyOnLoadObjects()
    {
        return TrackedDontDestroyOnLoadObjects;
    }
    
    public static void DestroyOnLoadTracked(GameObject objectToTrack)
    {
        TrackedDontDestroyOnLoadObjects.Remove(objectToTrack);
        UnityEngine.Object.Destroy(objectToTrack);
    }
}