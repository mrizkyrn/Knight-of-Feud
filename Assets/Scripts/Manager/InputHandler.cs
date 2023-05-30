using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }

    private PlayerInput inputHandler;

    public event Action OnInventoryPressed;
    public event Action OnOpenChestPressed;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        inputHandler = new PlayerInput();
    }

    private void OnEnable()
    {
        inputHandler.GameMenu.Enable();
        inputHandler.GameMenu.Inventory.performed += OnCharacterMenu;

        inputHandler.Gameplay.Enable();
        inputHandler.Gameplay.OpenChest.performed += OnOpenChest;
    }

    private void OnDisable()
    {
        inputHandler.GameMenu.Disable();
        inputHandler.GameMenu.Inventory.performed -= OnCharacterMenu;
        
        inputHandler.Gameplay.Disable();
        inputHandler.Gameplay.OpenChest.performed -= OnOpenChest;
    }

    private void OnCharacterMenu(InputAction.CallbackContext context)
    {
        OnInventoryPressed?.Invoke();
    }

    private void OnOpenChest(InputAction.CallbackContext context)
    {
        OnOpenChestPressed?.Invoke();
    }
}
