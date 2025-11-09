using UnityEngine;

public class TileClashReference : MonoBehaviour {
    [Header("Game Data")]
    public TileSetGameData tileSetGameData;

    public PoolSettings poolSettings;
    public UIReference uiReference;

    [Header("Gameplay")]
    public SessionData sessionData;

    public GameObject backgroundPrefab;
    public Transform boardTransform;
}