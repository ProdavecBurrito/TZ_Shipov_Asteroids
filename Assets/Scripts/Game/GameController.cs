using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private ScoreUI _scoreUI;
    [SerializeField] private HealthUI _healthUI;

    private GameMenu _gameMenu;
    private GameOverMenu _gameOverMenu;

    private ShipController _shipController;
    private UFOSpawnController _ufoSpawnController;
    private AsteroidSpawnController _asteroidSpawnController;

    private void Start()
    {
        LoadBaseObjects();
        _healthUI = GetComponentInChildren<HealthUI>();
        _scoreUI = GetComponentInChildren<ScoreUI>();

        _shipController = new ShipController(_gameMenu, _healthUI);
        _ufoSpawnController = new UFOSpawnController(_scoreUI);
        _asteroidSpawnController = new AsteroidSpawnController(_scoreUI);

        _shipController.ShipModel.OnDie += GameOver;

        UpdatingController.AddToUpdate(_ufoSpawnController);
        UpdatingController.AddToUpdate(_shipController);
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
        _gameMenu = ResourcesLoader.LoadAndInstantiateObject<GameMenu>("Prefabs/GameMenu");
        _gameOverMenu = ResourcesLoader.LoadAndInstantiateObject<GameOverMenu>("Prefabs/GameOverMenu");
    }

    private void GameOver()
    {
        _gameOverMenu.gameObject.SetActive(true);
    }


    private void OnDestroy()
    {
        UpdatingController.RemoveAllFromUpdate();
    }
}
