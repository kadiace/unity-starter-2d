using UnityEngine;

public class GameSceneContext : MonoBehaviour
{
    [SerializeField] private InputMode _inputMode = InputMode.Player;

    private void Start()
    {
        Managers.Input.SetInputMode(_inputMode);
        Debug.Log("[GameSceneContext] Game scene initialized.");
    }
}
