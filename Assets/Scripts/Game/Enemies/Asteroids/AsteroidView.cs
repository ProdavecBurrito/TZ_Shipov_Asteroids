using UnityEngine;

public class AsteroidView : BaseEnemyView
{
    private void Awake()
    {
        _unitTransform = GetComponent<Transform>();
        SetActivity(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionType = collision.GetComponent<BaseUnitView>();
        {
            if (collisionType is ShipView || collisionType is UFOView)
            {
                collisionType.GetDamage(false);
            }
        }
    }
}
