using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }

    private PlayerInput inputHandler;

    public event Action OnInventoryPressed;
    public event Action OnOpenChestPressed;
    public event Action OnUseItem1Pressed;
    public event Action OnUseItem2Pressed;
    public event Action OnUseItem3Pressed;

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
        inputHandler.Gameplay.UseItem1.performed += OnUseItem1;
        inputHandler.Gameplay.UseItem2.performed += OnUseItem2;
        inputHandler.Gameplay.UseItem3.performed += OnUseItem3;
    }

    private void OnDisable()
    {
        inputHandler.GameMenu.Disable();
        inputHandler.GameMenu.Inventory.performed -= OnCharacterMenu;
        
        inputHandler.Gameplay.Disable();
        inputHandler.Gameplay.OpenChest.performed -= OnOpenChest;
        inputHandler.Gameplay.UseItem1.performed -= OnUseItem1;
        inputHandler.Gameplay.UseItem2.performed -= OnUseItem2;
        inputHandler.Gameplay.UseItem3.performed -= OnUseItem3;
    }

    private void OnCharacterMenu(InputAction.CallbackContext context)
    {
        OnInventoryPressed?.Invoke();
    }

    private void OnOpenChest(InputAction.CallbackContext context)
    {
        OnOpenChestPressed?.Invoke();
    }
    
    private void OnUseItem1(InputAction.CallbackContext context)
    {
        OnUseItem1Pressed?.Invoke();
    }

    private void OnUseItem2(InputAction.CallbackContext context)
    {
        OnUseItem2Pressed?.Invoke();
    }

    private void OnUseItem3(InputAction.CallbackContext context)
    {
        OnUseItem3Pressed?.Invoke();
    }
}
