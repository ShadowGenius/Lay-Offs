using UnityEngine;

public class ButtonActions : MonoBehaviour
{
    public void GameStart()
    {
        SceneSwitcher.Change("Interactables");
    }

    public void Menu()
    {
        SceneSwitcher.Change("MainMenu");
    }
}