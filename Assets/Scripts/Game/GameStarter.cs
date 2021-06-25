using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private ScoreUI _scoreUI;
    [SerializeField] private HealthUI _healthUI;

    private GameMenu _gameMenu;
    private GameOverMenu _gameOverMenu;

    private PlayerShip _playerShip;
    private UFOSpawnController _ufoSpawnController;
    private AsteroidSpawnController _asteroidSpawnController;

    private void Start()
    {
        LoadBaseObjects();
        _healthUI = GetComponentInChildren<HealthUI>();
        _scoreUI = GetComponentInChildren<ScoreUI>();

        _playerShip.InjectUI(_gameMenu, _gameOverMenu, _healthUI);

        _ufoSpawnController = new UFOSpawnController(_scoreUI);
        _asteroidSpawnController = new AsteroidSpawnController(_scoreUI);

        _playerShip.OnDie += GameOver;

        UpdatingController.AddToFixedUpdate(_playerShip);
        UpdatingController.AddToUpdate(_ufoSpawnController);
        UpdatingController.AddToUpdate(_playerShip);
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
        _playerShip = ResourcesLoader.LoadAndInstantiateObject<PlayerShip>("Prefabs/Ship");
    }

    private void GameOver()
    {
        _gameOverMenu.gameObject.SetActive(true);
    }


    private void OnDestroy()
    {
        UpdatingController.RemoveAllFromUpdate();
        UpdatingController.RemoveAllFromFixedUpdate();
        _playerShip.OnDie -= GameOver;
    }
}
