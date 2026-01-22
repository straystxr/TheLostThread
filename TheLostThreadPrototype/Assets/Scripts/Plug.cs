using System.Net.Sockets;
using Scenes.Nirvana_Mechanics.Scripts;
using UnityEngine;
using Socket = System.Net.Sockets.Socket;

public enum PlugOrder
{
    first,
    second,
    third,
    fourth
}

public class Plug : MonoBehaviour
{
    //script is used to store data only
    public PlugOrder plugorder;
    
    //bool is set to false immediately as the plugs will not be connected initially and will be used in other scripts
    public bool isConnected = false;
    public Scenes.Nirvana_Mechanics.Scripts.Socket currentSocket;
}
