using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameMenuController _gameMenuController;
    private InputController _inputController;
    private BattleUnitView _shipView;
    private BattleUnitView _ufoView;
    private UFOSpawnController _ufoSpawnController;

    private void Start()
    {
        LoadBaseObjects();
        _inputController = new InputController(_shipView, _gameMenuController);
        _ufoSpawnController = new UFOSpawnController(_ufoView, (ShipView)_shipView);

        UpdatingController.AddToUpdate(_ufoSpawnController);
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
        _gameMenuController = ResourcesLoader.LoadAndInstantiateObject<GameMenuController>("Prefabs/GameMenu");
    }

    private void OnDestroy()
    {
        UpdatingController.RemoveAllFromUpdate();
    }
}
