using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Border : MonoBehaviour
{
    private BoxCollider2D _boxCollider;
    private float _rightBorder;
    private float _leftBorder;
    private float _topBorder;
    private float _botRorder;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.enabled = true;
        CalculateBorders();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        RemoveCollision(collision);
    }

    private void CalculateBorders()
    {
        _rightBorder = _boxCollider.bounds.max.x;
        _leftBorder = _boxCollider.bounds.min.x;
        _topBorder = _boxCollider.bounds.max.y;
        _botRorder = _boxCollider.bounds.min.y;
    }

    private void RemoveCollision(Collider2D collider)
    {
        if (collider.transform.position.x >= _rightBorder )
        {
            collider.transform.position = new Vector2(_leftBorder, collider.transform.position.y);
        }
        else if (collider.transform.position.x <= _leftBorder)
        {
            collider.transform.position = new Vector2(_rightBorder, collider.transform.position.y);
        }
        else if (collider.transform.position.y >= _topBorder)
        {
            collider.transform.position = new Vector2(collider.transform.position.x, _botRorder);
        }
        else if (collider.transform.position.y <= _botRorder)
        {
            collider.transform.position = new Vector2(collider.transform.position.x, _topBorder);
        }
    }
}
