using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instnace
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;
                if(instance == null)
                {
                    var instanceContainer = new GameObject("Manager");
                    instance = instanceContainer.AddComponent(typeof(T)) as T;
                }
            }
            return instance;
        }
    }
}
