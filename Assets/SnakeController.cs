using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private Touch touch;
    private float speed=5f;
    public GameObject BodyPrefab;
    private List<GameObject> BodyParts=new List<GameObject>();
    //private List<Vector3> PositionHistory=new List<Vector3>();
    //private int gap=3;
    // Start is called before the first frame update
    void Start()
    {
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0,Time.deltaTime*speed);

        
        //transform.position=transform.position*speed*Time.deltaTime;
        if(Input.touchCount>0)
        {
            touch=Input.GetTouch(0);
            if(touch.phase==TouchPhase.Moved)   
            {
                Vector3 changeInPosition;
                changeInPosition.x=transform.position.x+touch.deltaPosition.x*0.1f*Time.deltaTime;

                if(changeInPosition.x<3.1 && changeInPosition.x>-3.1f)
                transform.position = new Vector3(changeInPosition.x,
                                                transform.position.y,
                                                transform.position.z);
                print("Touched");
            }
        }
    
        int index=0;
        foreach(GameObject body in BodyParts)
        {
            Vector3 followingPosition;
            if(index==0)
            {
                followingPosition=this.transform.position;
                followingPosition.z=followingPosition.z-1.1f;
            }
            else{
                followingPosition=BodyParts[index-1].transform.position;
                followingPosition.z=followingPosition.z-1.1f;
            }
            BodyParts[index].transform.position=Vector3.Lerp(BodyParts[index].transform.position,followingPosition,4f);
            index++;
        }
        

       

    // //Stioring the Position History
    // PositionHistory.Insert(0,transform.position);
    
    // //move body parts
    //     int index=0;
    //     foreach(var body in BodyParts){
    //         //Vector3 point = PositionHistory[Mathf.Min(index * gap, 0, PositionHistory.Count - 1)];
    //         Vector3 point = PositionHistory[Mathf.Min(index * gap,PositionHistory.Count - 1)];
    //         Vector3 pointDirection=point-body.transform.position;
    //         body.transform.position=point;
    //         index++;
    //     }
        

    }

    void GrowSnake(){
        GameObject body=Instantiate(BodyPrefab);
        BodyParts.Add(body);
    }

    void OnCollisionEnter(Collision collision)
    {
       
        if(collision.collider.gameObject.tag=="pickup")
        {
            
            collision.gameObject.SetActive(false);
            GrowSnake();
            print("Collison Detected"+BodyParts.Count);
        }
        if(collision.collider.gameObject.tag=="hurdle")
        {
            if(BodyParts.Count!=0){
                Destroy(BodyParts[BodyParts.Count-1]);
                BodyParts.RemoveAt(BodyParts.Count-1);
            }
            print("Collision with hurdle "+BodyParts.Count);
            Destroy(collision.collider.gameObject);
        }    
    }
}
