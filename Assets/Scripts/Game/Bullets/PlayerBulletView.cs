using UnityEngine;

public class PlayerBulletView : BaseBulletView
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionType = collision.GetComponent<BaseEnemyView>();
        {
            if (collisionType)
            {
                collisionType.GetDamage(true);
                if (collisionType is CrackingAsteroidsView asteroidsView)
                {
                    asteroidsView.Crack();
                }
                ChangeActiveState(false);
            }
        }
    }
}
