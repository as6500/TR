using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : Singleton<SceneManagement>
{
    public static Dictionary<GameObject, string> scenes = new(); //Scenes
    public static Dictionary<int, GameObject> uniqueIdentifiers = new(); //All objects that stays between scenes

    protected override void Awake()
    {
        base.Awake(); //calls the awake function of the parent class || if not called, the awake function on the singleton not called meaning does not do the process
        SceneManager.sceneLoaded += WithScene; //when you load a scene it runs this function || sceneLoaded is an event and += adds a listener to that event
    }
    
    public void WithScene(Scene scene, LoadSceneMode mode)
    {
        foreach (KeyValuePair<GameObject, string> keyValuePair in scenes) //KeyValuePair - key = gameObject, value = scene (dictionary has both)
        {
            if (keyValuePair.Value == scene.name)
                keyValuePair.Key.SetActive(true); //shows the gameobject on the scene if the scene is the same as the gameobject truly is
            else
                keyValuePair.Key.SetActive(false); //hides the gameobject if it doesn't belong to that scene
        }
    }

    public void AddObjectToScene(GameObject obj, String sceneName)
    {
        int instanceId = obj.GetInstanceID(); // get the id that the unity creates on the object
        	
        if (!uniqueIdentifiers.ContainsKey(instanceId)) //if it doesn't have the name of this gameObject
        {
            uniqueIdentifiers.Add(instanceId, obj); //adds the name
            scenes.Add(obj, sceneName);
        }
		
        GameObject otherObject = GameObject.Find(obj.name);
		
        if (otherObject != null && uniqueIdentifiers[instanceId] != otherObject)
            Destroy(otherObject);

        if (sceneName != SceneManager.GetActiveScene().name) //if the scene is not the correct one, it deactivates them
            obj.SetActive(false);
        
        DontDestroyOnLoad(obj);
    }

    public void RemoveObjectFromScene(GameObject obj) //remove from the dictionaries
    {
        uniqueIdentifiers.Remove(obj.GetInstanceID());
        scenes.Remove(obj);
    }

    public void AddObjectToSceneForWeapon(GameObject obj, String sceneName)
    {
        int instanceId = obj.GetInstanceID(); // get the id that the unity creates on the object

        if (!uniqueIdentifiers.ContainsKey(instanceId)) //if it doesn't have the name of this gameObject
        {
            uniqueIdentifiers.Add(instanceId, obj); //adds the name
            scenes.Add(obj, sceneName);
        }

        GameObject otherObject = GameObject.Find(obj.name);

        if (otherObject != null && uniqueIdentifiers[instanceId] != otherObject)
            Destroy(otherObject);

        if (sceneName != "BunkerInside") //if the scene is not the correct one, it deactivates them
            obj.SetActive(true);

        DontDestroyOnLoad(obj);
    }
}
