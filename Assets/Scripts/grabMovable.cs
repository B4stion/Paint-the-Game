using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabMovable : MonoBehaviour
{

    bool grabMode;
    GameObject grabbedObject;
    float vicinity;
    PhysicMaterial grabbedPhysic;

    private bool isAssistMode;
    private GameObject assistLine;
    private LineRenderer assistLineRenderer;
    private Color assistColor;
    public Texture assistLineTexture;

    private GameObject brush;

    // Use this for initialization
    void Start()
    {
        vicinity = 1f;
        grabMode = false;
        grabbedObject = null;
        grabbedPhysic = null;

        isAssistMode = false;
        assistLine = new GameObject("Throw_Assist_Line");
        assistLine.AddComponent<LineRenderer>();
        assistLineRenderer = assistLine.GetComponent<LineRenderer>();
        assistColor = new Color(0,0,0,0);
        assistLineRenderer.startWidth = 0.2f;
        assistLineRenderer.endWidth = 0.2f;
        assistLineRenderer.material.mainTexture = assistLineTexture;        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkGrab();
        UpdateGrabbedPosition();
        DestroyGrabbedObject();
        DrawAssistLine();
    }

    void DrawAssistLine()
    {
        if (isAssistMode)
        {
            assistLineRenderer.enabled = true;
            Vector3 diff = (Camera.main.transform.position + Camera.main.transform.forward*1000f) - grabbedObject.transform.position;
            assistLineRenderer.material.SetTextureOffset("_MainTex", new Vector2(Time.timeSinceLevelLoad * 4f, 0f));
            assistLineRenderer.material.SetTextureScale("_MainTex", new Vector2(diff.magnitude, 1f));
            Vector3[] lineVertices = { (Camera.main.transform.position + Camera.main.transform.forward * 1000f), grabbedObject.transform.position };
            assistLineRenderer.SetPositions(lineVertices);
        }
        else
        {
            assistLineRenderer.enabled = false;
        }
    }

    void checkGrab()
    {
        if (!grabMode)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collider[] hitColliders = Physics.OverlapSphere(GameObject.Find("Player").transform.position, vicinity);
                List<Collider> hitCollidersList = new List<Collider>();

                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].gameObject.tag == "Movable")
                    {
                        print(hitColliders[i].name);
                        hitCollidersList.Add(hitColliders[i]);
                    }
                }

                hitCollidersList.Sort((x, y) => Vector3.Distance(x.gameObject.transform.position, GameObject.Find("Player").transform.position).CompareTo(Vector3.Distance(y.gameObject.transform.position, GameObject.Find("Player").transform.position)));

                for (int i = 0; i < hitCollidersList.Count; i++)
                {
                    print(hitCollidersList[i].name);
                    if (hitCollidersList[i].gameObject.GetComponent<Renderer>().isVisible)
                    {
                        grabbedObject = hitCollidersList[i].gameObject;
                        grabbedPhysic = hitCollidersList[i].material;
                        Destroy(grabbedObject.GetComponent<Rigidbody>());
                        Destroy(grabbedObject.GetComponent<BoxCollider>());
                        grabbedObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        assistColor = grabbedObject.GetComponent<MeshRenderer>().material.color;
                        grabMode = true;
                        break;
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                grabbedObject.AddComponent<BoxCollider>();
                grabbedObject.GetComponent<BoxCollider>().material = grabbedPhysic;
                grabbedObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                grabbedObject.AddComponent<Rigidbody>();
                grabMode = false;
                grabbedObject = null;
                grabbedPhysic = null;
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                var standardShaderMaterial1 = grabbedObject.GetComponent<MeshRenderer>().material;
                standardShaderMaterial1.color = new Color(0, 0, 0, 0.5f);
                standardShaderMaterial1.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial1.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                standardShaderMaterial1.SetInt("_ZWrite", 0);
                standardShaderMaterial1.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial1.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial1.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial1.renderQueue = 3000;
                isAssistMode = true;
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                isAssistMode = false;
                var standardShaderMaterial1 = grabbedObject.GetComponent<MeshRenderer>().material;
                standardShaderMaterial1.color = assistColor;
                standardShaderMaterial1.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                Vector3 forward = Camera.main.transform.forward * 1000f;
                grabbedObject.AddComponent<BoxCollider>();
                grabbedObject.GetComponent<BoxCollider>().material = grabbedPhysic;
                grabbedObject.AddComponent<Rigidbody>();
                grabbedObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                grabbedObject.GetComponent<Rigidbody>().AddForce(forward);
                grabMode = false;
                grabbedObject = null;
                grabbedPhysic = null;
                assistColor = new Color(0,0,0,0);
            }
        }
    }

    void UpdateGrabbedPosition()
    {
        if (grabMode)
        {
            grabbedObject.transform.position = GameObject.Find("Player").transform.position + Camera.main.transform.forward * 1.5f;
        }
    }

    void DestroyGrabbedObject()
    {
        if (grabMode)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Destroy(grabbedObject);
                grabbedObject = null;
                grabMode = false;
            }
        }
    }
}
