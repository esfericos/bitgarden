using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

public class EnemyCastle : Entity
{
    public EnemyMovement enemy;
    public int enemyQty = 10;
    public float timeToStart = 0.1f;
    public float timeToRepeat = 0.2f;
    private int totalEnemies = 0;
    private Vector3 position;


    public void SpawnEnemies(Position pos)
    {
        totalEnemies = 0;
        position = new Vector3(pos.X + 1, pos.Y + 1);
        InvokeRepeating(nameof(RenderEnemy), timeToStart, timeToRepeat);
    }

    private void RenderEnemy()
    {
        totalEnemies++;
        Instantiate(enemy, position, Quaternion.identity);
        if (totalEnemies >= enemyQty) CancelInvoke();
    }

}
