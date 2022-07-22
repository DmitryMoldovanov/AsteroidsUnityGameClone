using UnityEngine;

public class UIManager
{
    public void EnableUI(GameObject ui)
    {
        ui.SetActive(true);
    }

    public void DisableUI(GameObject ui)
    {
        ui.SetActive(false);
    }
}
