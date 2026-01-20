using System;
using System.Collections;
using Scenes.Nirvana_Mechanics.Scripts;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public Socket[] sockets;
    bool puzzleCompleted = false;


    void Awake()
    {
        Instance = this;
    }

    public void checkSockets()
    {
        //early return because we do not need to check if the puzzle is completed
        if (puzzleCompleted) return;
        
        int counter = 0;
        foreach (Socket socket in sockets)
        {
            if (socket.isCorrect) counter++;
        }
        Debug.Log($"{name}: Correct plug count: {counter}");
        if (counter == sockets.Length) completePuzzle();
    }
    void completePuzzle()
    {
        puzzleCompleted = true;
        Debug.Log("Puzzle finished wooooooooo");

        StartCoroutine(FanOn());
    }

    IEnumerator FanOn()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Sparks on");
        
        
    }
}
