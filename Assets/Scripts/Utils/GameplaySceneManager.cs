using UnityEngine;

/// <summary>
/// Enables to exit the game/app
/// </summary>
public class GameplaySceneManager : MonoBehaviour
{
    #region Life-cycle callbacks
    private void Start()
    {
        Input.backButtonLeavesApp = true;
    }

    /// <summary>
    /// Exits when X button is tapped
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    #endregion
}
