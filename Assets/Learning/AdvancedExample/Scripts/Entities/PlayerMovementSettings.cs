using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSettings", menuName = "Examples/Character Settings")]
public class PlayerMovementSettings : ScriptableObject {
    [Header("Movement")]
    [Range(0, 20)]
    public float walkingSpeed = 3f;
    public float runSpeed = 10f;
    public float rotationSpeed = 20f;
    public float jumpVelocity = 2f;
    public float gravity = -9.8f;

    [Header("Trail Settings")]
    public TrailSettings trailSettings;
}

[Serializable]
public class TrailSettings {
    public Color color;
    public float width;
    public float length;
}
