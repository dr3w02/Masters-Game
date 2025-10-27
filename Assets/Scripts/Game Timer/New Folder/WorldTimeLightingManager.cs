using System;
using UnityEngine;
using WorldTime;

namespace WorldTime
{

    public class LightingManager : MonoBehaviour
    {
        //References
        [SerializeField] private Light DirectionalLight;
        [SerializeField] private LightingPreset Preset;

        //Varibles
        [SerializeField, Range(0, 24)] private float TimeOfDay;

        [SerializeField]
        private WorldTime _worldTime;

        private void Awake()
        {
          
            _worldTime.WorldTimeChanged += WorldTimeSky;


        }
        private void OnDestroy()
        {
           _worldTime.WorldTimeChanged -= WorldTimeSky;
        }

        public void WorldTimeSky(object sender, TimeSpan newTime)
        {
            if (Application.isPlaying)
            {


                float hours = newTime.Hours + (newTime.Minutes / 60f);
                float timePercent = hours / 24f;



                UpdateLighting(timePercent);
                print($"SkyTimer{timePercent}");
                print(newTime);
              

            }
            else
            {
                UpdateLighting(TimeOfDay / 24f);
            }
        }

        //Checks if game is playing and then updates the lights based on the number its at(TimeOfDay);
        private void Update()
        {
            if (Preset == null)
                return;


           
        }

        //Checks if directional Light is assigned and changes colour and rotation
        private void UpdateLighting(float timePercent)
        {
            print("Lighting Updated");
            RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
            RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

            if (DirectionalLight != null)
            {
                DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
                DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
            }

        }


        //If directional Light is not set it will find the light object with the name of sun in the scene
        //If thats not set then it finds the first directional light ands takes that. 
        private void OnValidate()
        {
            if (DirectionalLight != null)
                return;

            if (RenderSettings.sun != null)
            {
                DirectionalLight = RenderSettings.sun;
            }
            else
            {
                Light[] lights = GameObject.FindObjectsOfType<Light>();

                foreach (Light light in lights)
                {
                    if (light.type == LightType.Directional)
                    {
                        DirectionalLight = light;
                        return;
                    }
                }
            }
        }

    }
}
