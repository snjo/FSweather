using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class FSweatherSystem : MonoBehaviour
    {
        public Vessel activeVessel;
        public Vector3 windDirection = new Vector3(0f, 0f, 0f);
        public float windForce = 0f;
        public Vector2 buttonSize = new Vector2(55f, 25f);
        public Vector2 GUIsubElementPadding = new Vector2(6f, 6f);
        public float windIncrements = 0.1f;
        public Rect GUIoriginalRect = new Rect(126f, 2f, 500f, 300f);
        public List<FirespitterWeather.FSstorm> storms = new List<FirespitterWeather.FSstorm>();
        public bool showMenu = false;
        public Transform compass;
        Vector3 forceDirection;

        public void adjustWind(float amount)
        {
            windForce += amount;
        }

        public void adjustWindDirection(Vector3 amount)
        {
            windDirection += amount;
        }

        public void Start()
        {
            Debug.Log("FSweatherSystem: Start");
            compass = new GameObject().transform;
        }

        public void FixedUpdate()
        {            
            if (!HighLogic.LoadedSceneIsFlight)
                return;            

            if (windForce != 0f)
            {                
                Vessel vessel = FlightGlobals.ActiveVessel;
                if (vessel != null)
                {                    
                    Vector3 up = (vessel.rigidbody.position - vessel.mainBody.position).normalized;
                    compass.position = vessel.mainBody.transform.position;
                    compass.LookAt(vessel.transform);
                    //compass x:ew y:ns z:alt

                    forceDirection = ((compass.right * windDirection.x) + (compass.up * windDirection.y) + (compass.forward * windDirection.z)).normalized;
                    
                    if (vessel.parts.Count > 0)
                    {
                        foreach (Part p in vessel.parts)
                        {
                            if (p.rigidbody != null)
                            {                                
                                p.rigidbody.AddForce(forceDirection * windForce);// * p.maximum_drag);                            
                            }
                        }
                        //Part testPart = vessel.parts[0];
                        //testPart.rigidbody.AddForce(forceDirection * windForce);                    
                    }
                    else
                    {
                        //Debug.Log("FSweatherSystem: activeVessel parts count is < 0");
                    }
                }
                else
                {
                    //Debug.Log("FSweatherSystem: activeVessel is null");
                }
            }
        }

        public void OnGUI()
        {
            Rect currentRect = new Rect(GUIoriginalRect.x, GUIoriginalRect.y, buttonSize.x, buttonSize.y);
            
            if (GUI.Button(new Rect(currentRect.x, currentRect.y, buttonSize.y, buttonSize.y), "W"))
            {
                showMenu = !showMenu;
            }
            currentRect.y += buttonSize.y + GUIsubElementPadding.y;

            if (showMenu)
            {
                // windForce adjustment
                if (GUI.Button(currentRect, "wind +"))
                {
                    adjustWind(+windIncrements);
                }
                currentRect.x += buttonSize.x + GUIsubElementPadding.x;
                if (GUI.Button(currentRect, "wind -"))
                {
                    adjustWind(-windIncrements);
                }
                currentRect.x = GUIoriginalRect.x;
                currentRect.y += buttonSize.y + GUIsubElementPadding.y;

                // windDirection adjustment
                if (GUI.Button(currentRect, "West x+"))
                {
                    adjustWindDirection(new Vector3(+0.1f, 0f, 0f));
                }
                currentRect.x += buttonSize.x + GUIsubElementPadding.x;
                if (GUI.Button(currentRect, "East x-"))
                {
                    adjustWindDirection(new Vector3(-0.1f, 0f, 0f));
                }
                currentRect.x += buttonSize.x + GUIsubElementPadding.x;
                if (GUI.Button(currentRect, "North y+"))
                {
                    adjustWindDirection(new Vector3(0f, +0.1f, 0f));
                }
                currentRect.x += buttonSize.x + GUIsubElementPadding.x;
                if (GUI.Button(currentRect, "South y-"))
                {
                    adjustWindDirection(new Vector3(0f, -0.1f, 0f));
                }
                currentRect.x += buttonSize.x + GUIsubElementPadding.x;
                if (GUI.Button(currentRect, "Up z+"))
                {
                    adjustWindDirection(new Vector3(0f, 0f, +0.1f));
                }
                currentRect.x += buttonSize.x + GUIsubElementPadding.x;
                if (GUI.Button(currentRect, "Down z-"))
                {
                    adjustWindDirection(new Vector3(0f, 0f, -0.1f));
                }
                                
                currentRect.x = GUIoriginalRect.x;
                currentRect.y += buttonSize.y + GUIsubElementPadding.y;
                GUI.Label(new Rect(currentRect.x, currentRect.y, GUIoriginalRect.width, buttonSize.y), "Wind: " + windDirection + " / " + windForce + " : " + forceDirection);
            }
        }
    }