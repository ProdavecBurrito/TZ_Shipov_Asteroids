using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private InputType _inputType;

    private InputController _inputController;
    private BattleUnitView _shipView;
    private BattleUnitView _ufoView;
    private UFOSpawnController _ufoSpawnController;

    private void Awake()
    {
        LoadBaseObjects();
        _inputController = new InputController(_inputType, _shipView);
        _ufoSpawnController = new UFOSpawnController(_ufoView);
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
        _shipView = ResourcesLoader.LoadAndInstantiateObject<BattleUnitView>("Prefabs/Ship");
        ResourcesLoader.LoadAndInstantiateObject<Border>("Prefabs/Border");
        _ufoView = ResourcesLoader.LoadAndInstantiateObject<BattleUnitView>("Prefabs/UFO");
    }

    private void OnDestroy()
    {
        UpdatingController.RemoveFromUpdate(_inputController);
    }
}
