
using UnityEngine;

/// <summary>
/// Implementation of Singleton Pattern
/// </summary>
/// <typeparam name="T">The type we want to make a singleton</typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T self = null;

    /// <summary>
    /// 
    /// </summary>
    public static T Instance
    {
        get
        {
            // Instance requiered for the first time, we look for it
            if (self == null)
            {
                self = GameObject.FindObjectOfType(typeof(T)) as T;
            }
            return self;
        }
    }

    private void Awake()
    {
        if (self == null)
        {
            self = this as T;
        }
        else if (self != this)
        {
            Destroy(this);
            return;
        }
    }
}