using UnityEngine;

public abstract class PuzzleInteractable : MonoBehaviour
{
    public string PuzzleID; // unique ID for the puzzle

    public abstract string GetState();
    public abstract void LoadState(string state);
}