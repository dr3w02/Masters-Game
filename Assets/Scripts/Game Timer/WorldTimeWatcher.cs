using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

namespace WorldTime
{


    public class WorldTimeWatcher : MonoBehaviour
    {
        [SerializeField]
        private WorldTime _worldTime;


        //List to store all the scheduled events
        [SerializeField]
        private List<Schedule> _schedule;

        private void Start()
        {
            _worldTime.WorldTimeChanged += CheckSchedule;
        }

        private void OnDestory()
        {
            _worldTime.WorldTimeChanged -= CheckSchedule;
        }

        //Losts inside of the schedual list and checks if any of the listed events is set for the time of the day that is now
        //if there is then invoke the action listed in the unity event 
        private void CheckSchedule(object sender, TimeSpan newTime)
        {
            var schedule = _schedule.FirstOrDefault( s => s.Hour == newTime.Hours && s.Minute == newTime.Minutes);

            schedule?._action?.Invoke();
        }


        //Setting up a event system where when something happens and this hour or minute the event begins
        [Serializable]
        private class Schedule 
        {
            public int Hour;
            public int Minute;

            public UnityEvent _action;
        }



    }

}
