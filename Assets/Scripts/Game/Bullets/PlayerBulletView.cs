using UnityEngine;

public class PlayerBulletView : BaseBulletView
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionType = collision.GetComponent<BaseEnemyView>();
        {
            if (collisionType)
            {
                collisionType.GetDamage();
                if (collisionType is CrackingAsteroidsView asteroidsView)
                {
                    asteroidsView.Crack();
                }
                if (collisionType is IScoreKeeper scoreKeeper)
                {
                    scoreKeeper.GiveScore();
                }
                ChangeActiveState(false);
            }
        }
    }
}
