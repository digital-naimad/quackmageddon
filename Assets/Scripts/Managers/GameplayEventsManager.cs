using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quackmageddon
{
    /// <summary>
    /// Implementation of Observer Pattern also using implementation of Singleton Pattern
    /// </summary>
    public class GameplayEventsManager : MonoSingleton<GameplayEventsManager>
    {
        private Dictionary<string, List<Action<short>>> listenersDictionary;

        #region Life-cycle callback

        private void Awake()
        {
            listenersDictionary = new Dictionary<string, List<Action<short>>>();
        }

        #endregion

        /// <summary>
        /// Registers listener for an event of type given in parameter
        /// </summary>
        /// <param name="eventType">One of constant strings defined in GameplayEventType class</param>
        /// <param name="callbackFunction"></param>
        public void RegisterListener(string eventType, Action<short> callbackFunction)
        {
            if (!listenersDictionary.ContainsKey(eventType))
            {
                listenersDictionary.Add(eventType, new List<Action<short>>());
            }

            listenersDictionary[eventType].Add(callbackFunction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType">One of constant strings defined in GameplayEventType class</param>
        /// <param name="callbackFunction"></param>
        public void UnregisterListener(string eventType, Action<short> callbackFunction)
        {
            if (!listenersDictionary.ContainsKey(eventType))
            {
                Debug.Log("listenersDictionary: Cannot unregister callback for eventType: " + eventType);

                listenersDictionary[eventType].Remove(callbackFunction);
                return;
            }

            if (!listenersDictionary[eventType].Contains(callbackFunction))
            {
                Debug.Log("actionsList: Cannot unregister callback for eventType: " + eventType);
                return;
            }

            listenersDictionary[eventType].Remove(callbackFunction);
        }

        /// <summary>
        /// Callbacks all of registered listeners about occurrence of an event given in the first parameter
        /// </summary>
        /// <param name="eventType">One of a constant strings defined in GameplayEventType class</param>
        /// <param name="value">optional value of type short. Using by some eventTypes</param>
        public void DispatchEvent(string eventType, short value = 0)
        {
            if (!listenersDictionary.ContainsKey(eventType))
            {
                Debug.Log("There is no any listeners registered for eventType: " + eventType);
            }

            foreach (Action<short> listener in listenersDictionary[eventType])
            {
                listener(value);
            }
        }
    }
}
