using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarinaAnchor : MonoBehaviour
{
    private int damage;

    public async UniTask AdditionalSkillAsync(int damage, float timeToRotateAnchor, float anchorRotationSpeed, float anchorRotationValue)
    {
        var anchor = transform.GetChild(0);

        var startTime = Time.time;

        while (true)
        {
            var elapsedTime = Time.time - startTime;

            if (elapsedTime >= timeToRotateAnchor)
                break;

            // ���� ���ư��� ���� �������� �̵�
            transform.Rotate(0, 0, -anchorRotationSpeed);
            anchor.Translate(Vector2.left * (anchorRotationValue * Time.deltaTime));

            await UniTask.NextFrame();
        }

        // TODO : ���� ����� �� ã�Ƽ� ������ ����
        var nearestEnemy = GameManager.Instance.GetNearestEnemy();

        transform.LookAt(nearestEnemy.transform);
        this.Move(nearestEnemy.transform, 0.2f);

        await UniTask.Delay(200);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Hit(damage);
        }
    }
}
