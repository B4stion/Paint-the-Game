using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class paintBullet : MonoBehaviour
{


    public Sprite paint_sprite;

    public Material paint_material;

    public PhysicMaterial physic_material;


    // Use this for initialization


    void Start()
    {

        paint_material.color = gameObject.GetComponent<Renderer>().material.color;

    }



    // Update is called once per frame

    void Update()
    {


    }



    private void OnCollisionEnter(Collision collision)
    {
        print(collision.collider.name);
        if (collision.collider.gameObject.tag == "Floor")
        {
            print(collision.collider.gameObject.name);
            Vector3 generatePoint = collision.contacts[0].point;
            GameObject blob = new GameObject("Blob");
            blob.tag = "Floor";
            blob.transform.localScale = new Vector3(7, 7, 7);
            blob.AddComponent<SpriteRenderer>();
            blob.GetComponent<SpriteRenderer>().sprite = paint_sprite;
            blob.GetComponent<SpriteRenderer>().material = paint_material;
            blob.GetComponent<SpriteRenderer>().material.color = Color.red;
            blob.AddComponent<BoxCollider>();
            blob.GetComponent<BoxCollider>().material = physic_material;
            blob.GetComponent<BoxCollider>().size = new Vector3(0.12f,0.12f,0.01f);
            blob.GetComponent<BoxCollider>().transform.rotation = Quaternion.LookRotation(collision.contacts[0].normal, Vector3.up);
            blob.transform.position = generatePoint;
            if (blob.transform.position.y < 0)
            {
                blob.transform.position = new Vector3(blob.transform.position.x, 0.009f, blob.transform.position.z);
            }
            blob.transform.rotation = Quaternion.LookRotation(collision.contacts[0].normal,Vector3.up);
            Destroy(gameObject);
        }
    }


    public void changeBlobProperties(Sprite sprite, Material material, PhysicMaterial physicMaterial)
    {
        paint_sprite = sprite;
        paint_material = material;
        physic_material = physicMaterial;
    }

}
