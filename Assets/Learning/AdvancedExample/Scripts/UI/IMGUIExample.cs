using System;
using System.Threading.Tasks;
using UnityEngine;

public class IMGUIExample : MonoBehaviour {
    private PlayerReference playerReference;
    private bool button;

    private async void Start() {
        while (playerReference == null) {
            playerReference = FindFirstObjectByType<PlayerReference>();
            await Task.Yield();
        }
    }

    public void OnGUI() {
        // get the icon from the project view
        Texture icon = (Texture)Resources.Load("icon");
        GUI.Box (new Rect (10,120,120,50), "");
        GUI.Label (new Rect (10,135,100,20), new GUIContent("This is a label", icon));
        button = GUI.Button (new Rect (10,180,100,20), new GUIContent ("Click me", "This is the tooltip"));
        if (button) {
            playerReference.playerUIReference.healthBar.fillAmount-=0.1f;
        }
    }
}
