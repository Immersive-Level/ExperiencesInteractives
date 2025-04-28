using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public MiniGameLoader loader;

    public void OnClickPlayPong()
    {
        loader.LoadMiniGame("Pong");
    }
    public void OnClickPlayTriqui()
    {
        loader.LoadMiniGame("Triqui");
    }
    public void OnClickPlayMachine()
    {
        loader.LoadMiniGame("SlotMachine");
    }
}
