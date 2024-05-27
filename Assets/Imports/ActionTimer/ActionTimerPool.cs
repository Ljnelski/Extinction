using System;
using System.Collections.Generic;
using UnityEngine;

namespace TimerUtility
{

    /*  Filename:           TimerPool.cs
    *  Author:             Liam Nelski (301064116)
    *  Last Update:        2024 Febuary 13th
    *  Description:        Script that can contain n number of Action Timers that can be placed on objects
    *  Revision History:   2022 November 12th (Liam Nelski): Inital Script.
    *                      2022 November 13th (Liam Nelski): Added support for a Callback on tick
    *                      2022 November 14th (Liam Nelski): Added support for Pausing, Updating and Canceling Timers + a dictionary to search for named Timers
    *                      2024 Febuary 13th (Liam Nelski): Added To TimerUtility Namespace and Updated to work with Action Timer V 1.0.2
    *                      
    *           
    *           
    * Version: 1.0.0
    */
    public class ActionTimerPool : MonoBehaviour
    {
        Queue<ActionTimer> _pooledTimers;
        List<ActionTimer> _activeTimers;
        Dictionary<string, ActionTimer> _activeNamedTimers;

        private void Awake()
        {
            _pooledTimers = new Queue<ActionTimer>();
            _activeTimers = new List<ActionTimer>();
            _activeNamedTimers = new Dictionary<string, ActionTimer>();
        }

        private void Update()
        {
            if (_activeTimers.Count > 0)
            {
                float deltaTime = Time.deltaTime;

                for (int i = 0; i < _activeTimers.Count; i++)
                {
                    _activeTimers[i].Tick(deltaTime);
                }
            }
        }

        public ActionTimer CreateNamedTimer(string name, float duration, Action onComplete, Action<float> onTick)
        {
            ActionTimer newTimer;
            if (_activeNamedTimers.TryGetValue(name, out newTimer))
            {
                Debug.LogError("TimerPool ERROR: Named Timer: " + name + " already exists in the dictionary");
                return null;
            }

            if (_pooledTimers.Count > 0)
            {
                newTimer = _pooledTimers.Dequeue();

                newTimer.SetCompleteCallback(onComplete);
                newTimer.SetTickCallback(onTick);
                newTimer.Start(duration);
            }
            else
            {
                newTimer = new ActionTimer(true, ReturnTimerToPool);
                newTimer.SetCompleteCallback(onComplete);
                newTimer.SetTickCallback(onTick);
                newTimer.Start(duration);
            }

            _activeNamedTimers.Add(name, newTimer);
            return newTimer;
        }

        public ActionTimer CreateNamedTimer(string name, float duration, Action onComplete)
        {
            return CreateNamedTimer(name, duration, onComplete, null);
        }

        public ActionTimer CreateTimer(float duration, Action onComplete, Action<float> onTick)
        {
            ActionTimer newTimer;
            if (_pooledTimers.Count > 0)
            {
                newTimer = _pooledTimers.Dequeue();
                newTimer.SetCompleteCallback(onComplete);
                newTimer.SetTickCallback(onTick);
                newTimer.Start(duration);
            }
            else
            {
                newTimer = new ActionTimer(true, ReturnTimerToPool);
                newTimer.SetCompleteCallback(onComplete);
                newTimer.SetTickCallback(onTick);
                newTimer.Start(duration);
            }

            _activeTimers.Add(newTimer);
            return newTimer;
        }

        public ActionTimer CreateTimer(float duration, Action callback)
        {
            return CreateTimer(duration, callback, null);
        }

        public void UpdateTimerTime(string name, float value)
        {
            if (_activeNamedTimers.TryGetValue(name, out ActionTimer timer))
            {
                timer.Tick(value);
            }
        }

        public void SetTimerTime(string name, float value)
        {
            if (_activeNamedTimers.TryGetValue(name, out ActionTimer timer))
            {
                timer.Tick(value);
            }
        }

        private void ReturnTimerToPool(ActionTimer newTimer)
        {
            _activeTimers.Remove(newTimer);
            _pooledTimers.Enqueue(newTimer);
        }
    }
}