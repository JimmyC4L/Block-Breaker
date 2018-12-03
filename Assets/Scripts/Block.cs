using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Block : MonoBehaviour
{
    // config params
    [FormerlySerializedAs("breakSound")] [SerializeField]
    private AudioClip _breakSound;

    [FormerlySerializedAs("sparklesVFX")] [SerializeField]
    private GameObject _sparklesVfx;

    [FormerlySerializedAs("maxHitPoints")] [SerializeField]
    private int _maxHitCount;

    [FormerlySerializedAs("blockSprites")] [SerializeField]
    private Sprite[] _blockSprites;

    // cached reference
    private LevelController _levelController;

    // state variables
    [FormerlySerializedAs("hitCount")] [SerializeField]
    private int _hitCount;// serialized for debugging 

    private void Start()
    {
        IncreaseBreakableBlocksCount();
    }

    private void IncreaseBreakableBlocksCount()
    {
        _levelController = FindObjectOfType<LevelController>();
        if (CompareTag("Breakable"))
        {
            _levelController.IncreaseBlocksCount();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        HandleHit();
    }

    private void HandleHit()
    {
        if (!CompareTag("Breakable")) return;
        _hitCount++;
        _maxHitCount = _blockSprites.Length + 1;
        if (_hitCount >= _maxHitCount)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextBlockSprite();
        }
    }

    private void ShowNextBlockSprite()
    {
        var spriteIndex = _hitCount - 1;
        if (_blockSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = _blockSprites[spriteIndex];
        }
        else
        {
            Debug.Log("Block sprite is missing from array!");
        }
    }

    private void DestroyBlock()
    {
        if (Camera.main != null) AudioSource.PlayClipAtPoint(_breakSound, Camera.main.transform.position);
        TriggerSparklesVfx();
        Destroy(gameObject);
        FindObjectOfType<GameSession>().AddToScore();
        _levelController.BlockDestroyed();
    }

    private void TriggerSparklesVfx()
    {
        var sparkles = Instantiate(_sparklesVfx, transform.position, transform.rotation);
        Destroy(sparkles, 2);
    }
}