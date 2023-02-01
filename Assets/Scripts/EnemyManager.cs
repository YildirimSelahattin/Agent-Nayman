using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyManager : MonoBehaviour
{

    public static EnemyManager Instance = null;
    [SerializeField] public GameObject enemy;

   public float Health=100f;
    
    private void Start() {
       if(Instance ==null){
            Instance = this;
        }
    }
   

   public void getHit(float damage){

    Health -= damage;
    if (Health <= 0)
    {
        
        float x = this.gameObject.transform.position.x+Random.Range(-7,7);
        float y = this.gameObject.transform.position.y-8f;
        float z = this.gameObject.transform.position.z;
        this.gameObject.transform.DOMove(new Vector3(x,y,z),1f);
        this.gameObject.transform.DOScale(0,1f).OnComplete(()=>{
            Destroy(this.gameObject);
        });
        
    }
    

   }
}
