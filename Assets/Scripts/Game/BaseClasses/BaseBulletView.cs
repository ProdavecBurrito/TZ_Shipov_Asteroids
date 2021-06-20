using UnityEngine;

public class BaseBulletView : MonoBehaviour
{
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;

    private bool _isActive;

    public bool IsActive => _isActive;

    public void Awake()
    {
        _transform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeActiveState(false);
    }

    public void ChangeActiveState(bool isActive)
    {
        _transform.gameObject.SetActive(isActive);
        _isActive = isActive;
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }
}
