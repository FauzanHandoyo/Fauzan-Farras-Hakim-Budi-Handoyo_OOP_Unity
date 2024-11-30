using UnityEngine;

public class PointsManager : MonoBehaviour
{
    private int currentPoints = 0;

    // Add points to the current total
    public void AddPoints(int pointsToAdd)
    {
        currentPoints += pointsToAdd;
        Debug.Log($"Added {pointsToAdd} points. Total points: {currentPoints}");
    }

    // Get the current points
    public int GetCurrentPoints()
    {
        return currentPoints;
    }

    // Optionally reset points, if needed (e.g., when restarting the game or starting a new wave)
    public void ResetPoints()
    {
        currentPoints = 0;
    }
}
