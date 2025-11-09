using System.Collections.Generic;
using System.Linq;
using ToolBox.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement {
    private readonly PlayerReference playerPrefab;
    private readonly InputHandler inputHandler;
    private readonly PlayerSpawnPoint spawnPoint;

    private PlayerReference playerReference;
    private EntitySettings playerSettings;
    private PlayerMovementSettings movementSettings;

    private CurrentSavedData currentSavedData;
    private CharacterController characterController;
    private ICharacterState currentState;
    private bool isRotating;
    private Vector3 velocity;

    public Vector3 Velocity {
        get => velocity;
        set => velocity = value;
    }

    public PlayerMovement(PlayerReference playerPrefab, InputHandler inputHandler, PlayerSpawnPoint spawnPoint) {
        this.playerPrefab = playerPrefab;
        this.inputHandler = inputHandler;
        this.spawnPoint = spawnPoint;
    }

    public void Initialize() {
        playerReference  = Object.Instantiate(playerPrefab);
        movementSettings = playerReference.playerMovementSettings;
        playerSettings = playerReference.playerSettings;
        characterController = playerReference.GetComponent<CharacterController>();

        inputHandler.RegisterPlayerInput();

        LoadPlayerSavedData();
        characterController.transform.position = new Vector3(
            currentSavedData.currentPlayerData.position[0],
            currentSavedData.currentPlayerData.position[1],
            currentSavedData.currentPlayerData.position[2]);

        playerReference.transform.position = spawnPoint.transform.position;
        playerReference.transform.rotation = spawnPoint.transform.rotation;

        SetState(new IdleState(this, inputHandler, movementSettings));
        velocity = Vector3.zero;
    }
    private void LoadPlayerSavedData() {
        currentSavedData = SimpleSaveLoad.Instance.LoadData("saveData01");
        if (string.IsNullOrEmpty(currentSavedData.currentPlayerData.entitySettings.name)) {
            Debug.Log("No player name found. Creating new player with default settings.");
            SetDefaultData();
            SimpleSaveLoad.Instance.SaveData("saveData01");
        }
        else {
            Debug.Log($"Player with name - {currentSavedData.currentPlayerData.entitySettings.name} - initialized");
            LoadCurrentData();
        }
    }

    private void SetDefaultData() {
        var entitySettings = new CurrentPlayerSettings {
            name = playerSettings.entityName,
            health = playerSettings.health,
            mana = playerSettings.mana,
            strength = playerSettings.strength,
            dexterity = playerSettings.dexterity,
            intelligence = playerSettings.intelligence,
            vitality = playerSettings.vitality,
            luck = playerSettings.luck
        };

        currentSavedData.currentPlayerData.entitySettings.startInventory = new List<IItem>(playerSettings.currentInventory.inventory);

        currentSavedData.currentPlayerData.entitySettings = entitySettings;
        currentSavedData.currentPlayerData.position = new List<float> { 0f, 1f, 0f };

        currentSavedData.levelData = new LevelData {
            timeAndDate = "2024-11-26"
        };

        //using Save System for Unity package with Odin Serializer
        DataSerializer.Save("player_data", currentSavedData.currentPlayerData);
        DataSerializer.Save("level_data", currentSavedData.levelData);
    }

    private void LoadCurrentData() {
        EntitySettings entitySettings = DataSerializer.Load<EntitySettings>("player_data");
        LevelData levelData = DataSerializer.Load<LevelData>("level_data");

        CurrentPlayerData currentPlayerData = currentSavedData.currentPlayerData;
        playerSettings.entityName = currentPlayerData.entitySettings.name;
        playerSettings.health = currentPlayerData.entitySettings.health;
        playerSettings.mana = currentPlayerData.entitySettings.mana;
        playerSettings.strength = currentPlayerData.entitySettings.strength;
        playerSettings.dexterity = currentPlayerData.entitySettings.dexterity;
        playerSettings.intelligence = currentPlayerData.entitySettings.intelligence;
        playerSettings.vitality = currentPlayerData.entitySettings.vitality;
        playerSettings.luck = currentPlayerData.entitySettings.luck;

        foreach(Item item in currentPlayerData.entitySettings.startInventory.Cast<Item>()) {
            playerSettings.currentInventory.inventory.Add(item);
        }
    }

    public void Tick() {
        currentState.UpdateState();
        RotateCharacter();

        if(!IsGrounded()) {
            velocity.y -= movementSettings.gravity * Time.deltaTime;
        }
        else if(velocity.y <= 0) {
            velocity.y = -2f;
        }
        characterController.Move(velocity * Time.deltaTime);
    }

    public void MoveCharacter(float speed) {
        Vector3 moveInput = new Vector3(inputHandler.MovementData.x, 0f, inputHandler.MovementData.y).normalized;
        Vector3 moveDirection = characterController.transform.TransformDirection(moveInput) * speed;
        velocity.x = moveDirection.x;
        velocity.z = moveDirection.z;
    }

    private void RotateCharacter() {
        if(Mouse.current.rightButton.wasPressedThisFrame) {
            isRotating = !isRotating;

            Cursor.lockState = isRotating ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isRotating;
        }

        if(!isRotating) {
            return;
        }

        Vector3 rotation = new Vector3(0f, inputHandler.LookData.x * movementSettings.rotationSpeed, 0f);
        characterController.transform.Rotate(rotation * Time.deltaTime);
    }

    public bool IsGrounded() {
        return characterController.isGrounded;
    }

    public void SetState(ICharacterState newState) {
        currentState?.ExitState();
        newState.EnterState();
        Debug.Log($"{currentState} => {newState}");
        currentState = newState;
    }

    private void OnDestroy() {
        SetSaveData();
        SimpleSaveLoad.Instance.SaveData("saveData01");
        inputHandler.Dispose();
    }
    private void SetSaveData() {
        currentSavedData.currentPlayerData.position = new List<float> { characterController.transform.position.x, characterController.transform.position.y, characterController.transform.position.z };
        currentSavedData.currentPlayerData.entitySettings.name = playerSettings.entityName;
        currentSavedData.currentPlayerData.entitySettings.health = playerSettings.health;
        currentSavedData.currentPlayerData.entitySettings.mana = playerSettings.mana;
        currentSavedData.currentPlayerData.entitySettings.strength = playerSettings.strength;
        currentSavedData.currentPlayerData.entitySettings.dexterity = playerSettings.dexterity;
        currentSavedData.currentPlayerData.entitySettings.intelligence = playerSettings.intelligence;
        currentSavedData.currentPlayerData.entitySettings.vitality = playerSettings.vitality;
        currentSavedData.currentPlayerData.entitySettings.luck = playerSettings.luck;
        currentSavedData.currentPlayerData.entitySettings.startInventory = new List<IItem>(playerSettings.currentInventory.inventory);
        currentSavedData.levelData.timeAndDate = System.DateTime.Now.ToString("yyyy-MM-dd");
    }
}
