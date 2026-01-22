using System;
using UnityEngine;

namespace Scenes.Nirvana_Mechanics.Scripts
{
    public class Socket : MonoBehaviour
    {
        //calling the data from Plug to get the order of the plugs
        public PlugOrder order;
        //plugs within this socket if any
        public Plug currentPlug;
        //bool to check if its correct or not which will be automatically set to false 
        public bool isCorrect = false;

        private void OnTriggerEnter(Collider other)
        {
            //storing plug component within variable
            Plug plug = other.GetComponent<Plug>();

            if (plug != null && currentPlug == null && !plug.isConnected)
            {
                ConnectedPlug(plug);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //storing plug component within variable
            Plug plug = other.GetComponent<Plug>();
            
            //condition to check that plug is not null and the 
            if (plug != null && plug == currentPlug)
            {
                RemovePlug();
            }
        }

        public void ConnectedPlug(Plug plug)
        {
            currentPlug = plug;
            
            //turning the variables to true
            plug.isConnected = true;
            plug.currentSocket = this;

            if (plug.plugorder == order)
            {
                isCorrect = true;
                correctPLug();
            }
            else
            {
                isCorrect = false;
                wrongPlug();
            }

            //rechecking puzzle state
            PuzzleManager.Instance.checkSockets();
        }

        public void RemovePlug()
        {
            if (currentPlug == null)
                return;

            //resetting the plug
            currentPlug.isConnected = false;
            currentPlug.currentSocket = null;

            currentPlug = null;
            isCorrect = false;

            //rechecking puzzle state
            PuzzleManager.Instance.checkSockets();
        }

        public void correctPLug()
        {
            Debug.Log($"{name}: Correct plug inserted");
        }

        public void wrongPlug()
        {
            Debug.Log($"{name}: Wrong plug");
        }
    }
}