using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private ScoreUI _scoreUI;
    [SerializeField] private HealthUI _healthUI;

    private GameMenu _gameMenuController;
    private ShipController _inputController;
    private UFOSpawnController _ufoSpawnController;
    private AsteroidSpawnController _asteroidSpawnController;

    private void Start()
    {
        LoadBaseObjects();
        _healthUI = GetComponentInChildren<HealthUI>();
        _scoreUI = GetComponentInChildren<ScoreUI>();

        _inputController = new ShipController(_gameMenuController, _healthUI);
        _ufoSpawnController = new UFOSpawnController(_scoreUI);
        _asteroidSpawnController = new AsteroidSpawnController(_scoreUI);

        UpdatingController.AddToUpdate(_ufoSpawnController);
        UpdatingController.AddToUpdate(_inputController);
        UpdatingController.AddToUpdate(_asteroidSpawnController);
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
        ResourcesLoader.LoadAndInstantiateObject<Border>("Prefabs/Border");
        _gameMenuController = ResourcesLoader.LoadAndInstantiateObject<GameMenu>("Prefabs/GameMenu");
    }

    private void OnDestroy()
    {
        UpdatingController.RemoveAllFromUpdate();
    }
}
