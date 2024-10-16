using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    //publics
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    public float speed = 1f;

    public string TagToCheckEnemy = "Enemy";
    public string TagToCheckEndLine = "EndLine";

    public bool invencible = false;

    public GameObject endScreen;

    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("texto")]

    public TextMeshPro uiTextPowerUp;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    [SerializeField] private BounceHelper _bounceHelper;


    //privates
    private Vector3 _pos;
    private bool _canRun;
    private float _currentSpeed;
    private Vector3 _startPosition;
    private float _baseSpeedToAnimation = 7;

    private void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
    }

    public void Bounce()
    {
        if(_bounceHelper != null)
            _bounceHelper.Bounce();
    }


    void Update()
    {

        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        //tambem era possivel fazer o contrario
     
        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == TagToCheckEnemy)
        {
            if (invencible == false)
            {
                MoveBack();
                EndGame(AnimatorManager.AnimationType.DEAD);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == TagToCheckEndLine)
        {
           if(!invencible) EndGame();
        }
    }

    private void MoveBack()
    {
        transform.DOMoveZ(-1f, .3f).SetRelative();
    }

    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        _canRun = false;
        endScreen.SetActive(true);
        animatorManager.Play(animationType);

    }

    public void StartToRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedToAnimation);
    }

    #region POWER UPS

    public void SetPowerUpText(string s)
    {
       uiTextPowerUp.text = s;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void SetInvencible(bool b = true)
    {
        invencible = b;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
      /*  var p = trasnform.position;
        p.y = _startPosition`.y + amount;
        transform.position = p; */

        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight()
    {
        transform.DOMoveY(_startPosition.y, .1f);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }

    #endregion

}
