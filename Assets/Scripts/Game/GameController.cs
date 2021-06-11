using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private InputType _inputType;

    private BaseInput _baseInput;
    private InputController _inputController;
    private KeyboardInput _keyboard;
    private KeyboardPlusMouseInput _keyboardPlusMouse;
    private ShipView _shipView;

    private void Awake()
    {
        LoadBaseObjects();
        _inputController = new InputController(_inputType, _shipView);

        UpdatingController.AddToUpdate(_inputController);
    }

    private void Update()
    {
        UpdatingController.UpdateAll();
    }

    private void FixedUpdate()
    {
        UpdatingController.FixedUpdateAll();
    }

    private void LoadBaseObjects()
    {
        ResourcesLoader.LoadAndInstantiateObject<GameObject>("Prefabs/Background");
        _shipView = ResourcesLoader.LoadAndInstantiateObject<ShipView>("Prefabs/Ship");
    }
}
