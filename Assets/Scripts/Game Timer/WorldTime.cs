using System;
using System.Collections;
using UnityEngine;

namespace WorldTime
{
    public class WorldTime : MonoBehaviour
    {
        public event EventHandler<TimeSpan> WorldTimeChanged;

        [SerializeField]
        private float _dayLength = 300; // in seconds



        [SerializeField]
        private TimeSpan _currentTime;

       

        private float _minuteLength => _dayLength / WorldTimeConstants.MinutesInDay;


        //Starts Coroutine Move this if we want the day cycle timer to start later on 
        private void Start()
        {
            StartCoroutine(AddMinute());
        }

        
        private IEnumerator AddMinute()
        {
            _currentTime += TimeSpan.FromMinutes(1);
            WorldTimeChanged?.Invoke(this,_currentTime);
            yield return new WaitForSeconds(_minuteLength);
            
            StartCoroutine(AddMinute());
        }
        
    }
}


