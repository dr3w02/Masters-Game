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
        private TimeController _timeController;


        //List to store all the scheduled events
        [SerializeField]
        private List<Schedule> _schedule;



        private void Start()
        {
            _timeController.WorldTimeChanged += CheckSchedule;
        }

        private void OnDestory()
        {
            _timeController.WorldTimeChanged -= CheckSchedule;
        }

        //Losts inside of the schedual list and checks if any of the listed events is set for the time of the day that is now
        //if there is then invoke the action listed in the unity event 
        private void CheckSchedule(object sender, TimeSpan newTime)
        {
            foreach (var schedule in _schedule)
            {
                if (!schedule.hasTriggered && newTime.Hours >= schedule.Hour && newTime.Minutes >= schedule.Minute)
                {
                    schedule._action?.Invoke();
                    schedule.hasTriggered = true;
                }
            }
        }


        //Setting up a event system where when something happens and this hour or minute the event begins
        [Serializable]
        private class Schedule 
        {
            public int Hour;
            public int Minute;

            public UnityEvent _action;

            [SerializeField]
            public bool hasTriggered = false;
        }



    }

}
