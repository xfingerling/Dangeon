using System.Collections;
using UnityEngine;

public class ActorAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] _runningSprites;
    [SerializeField] private Sprite[] _idleSprites;

    private SpriteRenderer _spriteRender;
    private int frame = 0;
    private void Start()
    {
        _spriteRender = GetComponent<SpriteRenderer>();

        StartCoroutine(IdleAnimationRoutine());
    }

    IEnumerator IdleAnimationRoutine()
    {
        while (true)
        {
            if (frame >= _idleSprites.Length)
                frame = 0;

            int spriteIndex = frame;
            Debug.Log(spriteIndex);
            _spriteRender.sprite = _idleSprites[spriteIndex];
            frame++;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
