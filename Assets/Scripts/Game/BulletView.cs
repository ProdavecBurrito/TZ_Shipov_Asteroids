using UnityEngine;

public class BulletView : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is IDamageble damageble)
        {
            damageble.GetDamage();
        }
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
