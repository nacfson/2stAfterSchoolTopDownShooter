using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : PoolableMono{
    public ResourceDataSO ItemData => _itemData;
    [SerializeField ]
    private ResourceDataSO _itemData;

    private AudioSource _audioSource;
    private Collider2D _collider;

    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _itemData.useSound;
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PickUpResource(){
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine(){
        _collider.enabled = false;
        _spriteRenderer.enabled = false;
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length + 0.3f);
        PoolManager.Instance.Push(this);
    }

    //OnDisable 도 해줘야됨

    public override void Reset(){
        gameObject.layer = LayerMask.NameToLayer("Item");
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
    }
}
