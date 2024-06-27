using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{

    public string compareTag = "CoinCollector";
    public ParticleSystem particleSystem;
    //deletar o new depois
    public GameObject graphicItem;
    public float timeToHide = 0f;

    [Header("Sounds")]
    public AudioSource audioSource;

    private void Awake()
    {
        if (particleSystem != null) particleSystem.transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    private void HideObject()
    {
        graphicItem.SetActive(false);
    }


    protected virtual void Collect() 
    {
        Debug.Log("Collect");
        Invoke("HideObject", timeToHide);
        OnCollect();
     
    }
   
    
    protected virtual void OnCollect() 
    { 
        if(particleSystem != null) particleSystem.Play();
        if(audioSource != null)
        {
            audioSource.transform.parent = null;
            audioSource.Play();
            Destroy(audioSource.gameObject, 2f);


        }

        
    }


}
