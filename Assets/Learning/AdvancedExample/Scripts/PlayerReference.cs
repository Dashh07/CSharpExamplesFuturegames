using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerReference : MonoBehaviour {
    public PlayerMovementSettings playerMovementSettings;
    public EntitySettings playerSettings;
    public PlayerUIReference playerUIReference;
}
