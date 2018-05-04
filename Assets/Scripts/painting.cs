// Authors: Griffin Shaw, Ryun Han, Scott Hodnefield
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class painting : MonoBehaviour {
	private GameObject player;
	private GameObject orient;
	public static painting paintScript;
	public float brushDistance;
	private Vector3 brushPosition;
	public GameObject brush;
	private GameObject root;
	private GameObject temp;
	private Material renderMaterial;
	public PhysicMaterial bounce;
	public PhysicMaterial slide;
	public PhysicMaterial normal;
    public PhysicMaterial bounce_slide;
	private PhysicMaterial physicsMaterial;
	private List<Vector3> points;
	public Mesh primitiveMesh;
	private Mesh dynamicMesh;
	private bool canSwitch;
    private bool drawing;
	public enum Kind { normal, bounce, slide, floating, bounce_slide, bounce_floating, slide_floating, erase };
	public Kind current_kind;
	public string current_kind_string;
	private Color current_color;
    private int label;
    private int threshold=4;
    public float current_normal, current_bounce, current_slide, current_floating, total_normal, total_bounce, total_slide, total_floating;
    public string normal_text, bounce_text, slide_text, floating_text;
    public Sprite paint_sprite;
    public Material paint_material;

    private bool isFloating;
    private float addAmount = 2;

    public Mesh specialMesh;
    public bool ongoingSpecial;
    public Vector3 specialScale;

    private bool isAssistMode;
    private GameObject assistLine;
    private LineRenderer assistLineRenderer;
    public Texture assistLineTexture;

    public float deleteRange;
    public float full_length;

    //public globalVariableHolder GVH;
    //Variables to toggle when player can use the mixed paints
    public bool canPurple, canYellow, canTeal;

    private void Awake()
    {
        current_kind_string = "Normal";
    }
    // Use this for initialization
    void Start () {
		// Start brush reasonable distance from player
		player = GameObject.Find("Player");
		orient = GameObject.Find("PlayerOrient");
		paintScript = this;
		brushDistance = 2;
		brushPosition = orient.transform.position;
        brush = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //brush = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		brush.name = "Brush";
		Destroy(brush.GetComponent<SphereCollider>());
        isFloating = false;
        deleteRange = 1 / 2;

        // Initialize list used for drawing lines of paint
        points = new List<Vector3>();

		// Grab material from 
		renderMaterial = Resources.Load ("Materials/Paint") as Material;

		// Enable switching paints
		canSwitch = true;

		//color of the line and the properties.
		current_kind = Kind.normal;
		current_kind_string = "Normal";
		current_color = Color.black;
		ChangePointerColor(current_color);
		//physicsMaterial = Resources.Load("/Materials/Normal") as PhysicMaterial;
		physicsMaterial = normal;
        label = 0;
        total_normal = 0;
        total_bounce = 0;
        total_slide = 0;
        total_floating = 0;
        current_normal = total_normal;
        current_bounce = total_bounce;
        current_slide = total_slide;
        current_floating = total_floating;

        specialMesh = null;
        ongoingSpecial = false;

        isAssistMode = false;
        assistLine = new GameObject("Assist_Line");
        assistLine.AddComponent<LineRenderer>();
        assistLineRenderer = assistLine.GetComponent<LineRenderer>();
        assistLineRenderer.startWidth = 0.1f;
        assistLineRenderer.endWidth = 0.1f;
        assistLineRenderer.material.mainTexture = assistLineTexture;
    
        //Set mixed painting initially to false
        canPurple = globalVariableHolder.canPurple;
        canYellow = globalVariableHolder.canYellow;
        canTeal = globalVariableHolder.canTeal;
	}

	// Update is called once per frame
	void FixedUpdate () {
        orient = GameObject.Find("PlayerOrient");
        // Update brush distance
		brushDistance += Input.GetAxis("Mouse ScrollWheel")*5;
        brushDistance = Mathf.Max(0, brushDistance);
        brushPosition = orient.transform.position + orient.transform.forward * brushDistance;

        //This is the part where the paintbrush can go wherever it can
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(brushPosition);
		brushPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        canPurple = globalVariableHolder.canPurple;
        canYellow = globalVariableHolder.canYellow;
        canTeal = globalVariableHolder.canTeal;

        brush.transform.position = brushPosition;
        
        normal_text = "Normal: " + ((total_normal - current_normal)*100 / total_normal).ToString() + "% left. (" + (total_normal - current_normal) + " left)";
        bounce_text = "Bounce: " + ((total_bounce - current_bounce)*100 / total_bounce).ToString() + "% left. (" + (total_bounce - current_bounce) + " left)";
        slide_text = "Slide: " + ((total_slide - current_slide)*100 / total_slide).ToString() + "% left. (" + (total_slide - current_slide) + " left)";
        floating_text = "Floating: " + ((total_floating - current_floating) * 100 / total_floating).ToString() + "% left. (" + (total_floating - current_floating) + " left)";

        //print(normal_text);


        if (canSwitch)
		{
			ChangeKind();
		}


		// When mouse is clicked
		if (!drawing && Input.GetMouseButtonDown (0)) {
            ActivateDrawing();

            //Activate erasing if in eraser mode
            if (current_kind == Kind.erase)
            {
                Collider[] deletables = Physics.OverlapSphere(brush.transform.position, deleteRange);
                int i = 0;
                while (i < deletables.Length)
                {
                    if (deletables[i].tag == "Paint")
                    {
                        float recovered_paint = deletables[i].GetComponent<removeRB>().length;
                        Color recovered_color = deletables[i].GetComponent<MeshRenderer>().material.color;
                        ReturnPaint(recovered_paint, recovered_color);
                        Destroy(deletables[i].gameObject);
                    }
                    i++;
                }
            }
		} 
		// While mouse is held down
		else if (drawing && Input.GetMouseButton (0)) {

            // Calculate the length of a paint that will be used to connect the point of the current frame and that of the frame before.
            float length = Vector3.Distance(brushPosition, points[points.Count - 1]);
            full_length += length;

            // Add current point to a list only when there is still a paint left for the specific kind.           
            if (UpdateLength(current_kind, length))
            {
                points.Add(brushPosition);
                DrawLine();
                Destroy(root);
                root = temp;
            }
            // If paint is done, we deactivate the drawing.
            else
            {
                drawing = false;
                //Destroy(root);
                //root = null;
                DeactivateDrawing();
            }
			// Replace old paint object with new
		} 

		// When mouse is released
		else if (drawing && Input.GetMouseButtonUp (0)) {
            DeactivateDrawing();
            drawing = false;
		}

        checkSpecialMesh();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isAssistMode = !isAssistMode;
        }

        DrawAssistLine();
    }

    void drawSpecialMesh()
    {
        if (UpdateLength(current_kind, 3f))
        {
            GameObject newSpecialMesh = new GameObject("Special_Platform:" + specialMesh.name);
            newSpecialMesh.transform.position = brushPosition;
            newSpecialMesh.AddComponent<MeshFilter>();
            newSpecialMesh.AddComponent<MeshCollider>();
            newSpecialMesh.GetComponent<MeshFilter>().mesh = specialMesh;
            newSpecialMesh.GetComponent<MeshCollider>().sharedMesh = specialMesh;
            newSpecialMesh.GetComponent<MeshCollider>().material = physicsMaterial;
            newSpecialMesh.AddComponent<MeshRenderer>();
            newSpecialMesh.GetComponent<MeshRenderer>().material.color = current_color;
            newSpecialMesh.transform.localScale = specialScale;
            newSpecialMesh.tag = "Floor";
            if (!isFloating)
            {
                newSpecialMesh.AddComponent<Rigidbody>();
                Rigidbody RB = newSpecialMesh.GetComponent<Rigidbody>();
                RB.useGravity = true;
                RB.GetComponent<MeshCollider>().convex = true;
                // Freeze everything but vertical (y) position
                RB.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
            }
        } 
    }

    void checkSpecialMesh()
    {
        if (specialMesh != null)
        {
            if (!ongoingSpecial && Input.GetKeyDown(KeyCode.H))
            {
                ongoingSpecial = true;
                Destroy(brush);
                brush = new GameObject();
                brush.name = "Brush";
                brush.transform.position = brushPosition;
                brush.AddComponent<MeshFilter>();
                brush.GetComponent<MeshFilter>().mesh = specialMesh;
                brush.AddComponent<MeshRenderer>();
                brush.transform.localScale = specialScale;
                ChangePointerColor(current_color);
            }
            else if (ongoingSpecial && Input.GetKeyUp(KeyCode.H))
            {
                ongoingSpecial = false;
                Destroy(brush);
                brush = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                brush.name = "Brush";
                Destroy(brush.GetComponent<SphereCollider>());
                ChangePointerColor(current_color);
            }

            if(ongoingSpecial && Input.GetMouseButtonDown(0))
            {
                drawSpecialMesh();
            }
        }   
    }

    void ActivateDrawing()
    {
        // Disable changing paints
        canSwitch = false;
        drawing = true;
        // Add first point to list after clearing it.
        points.Clear();
        points.Add(brushPosition);

        // Add first element of mesh
        //dynamicMesh = primitiveMesh;
    }

    void DeactivateDrawing()
    {
        // Point variables away from now permanent paint so it isn't deleted next time
        temp = null;
        root = null;

		// Clear mesh for next paint
		dynamicMesh = null;
        
        if (points.Count < 3)
        {
            Destroy(GameObject.Find("Platform_" + label.ToString()));
        }
        else
        {
			//  If paint is not green (floating paint)
			if (!isFloating) {
				// Make final object affected by gravity
				GameObject finalObject = GameObject.Find("Platform_" + label.ToString());
				finalObject.AddComponent<Rigidbody>();
				Rigidbody finalRB = finalObject.GetComponent<Rigidbody>();
				finalRB.useGravity = true;
				finalRB.GetComponent<MeshCollider>().convex = true;
				// Freeze everything but vertical (y) position
				finalRB.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
				// Add script to remove rb on collision with floor
				finalObject.AddComponent<removeRB>();
                finalObject.GetComponent<removeRB>().length = full_length;
                full_length = 0;
			}
            //Floating paint needed rigidbody to be detected by the erasers overlapSphere
            //Disabled gravity and enable kinematic on floating paint
            if (isFloating)
            {
                // Make final object affected by gravity
                GameObject finalObject = GameObject.Find("Platform_" + label.ToString());
                finalObject.AddComponent<Rigidbody>();
                Rigidbody finalRB = finalObject.GetComponent<Rigidbody>();
                finalRB.useGravity = false;
                finalRB.GetComponent<MeshCollider>().convex = true;
                // Freeze everything but vertical (y) position
                finalRB.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                //finalObject.AddComponent<Rigidbody>();
                //finalObject.GetComponent<Rigidbody>().useGravity = false;
                //finalObject.GetComponent<Rigidbody>().isKinematic = true;
                finalObject.AddComponent<removeRB>();
                finalObject.GetComponent<removeRB>().length = full_length;
                full_length = 0;
            }
            label++;
        }

		// Clear up the list of points for the next paint
        points.Clear();
        
        
        // Enable switching of paints
        canSwitch = true;
    }
    
	void DrawLine() {

		// Initialize temp as new line object
		temp = new GameObject ("Platform_"+label.ToString());

        
		LineRenderer lr = temp.AddComponent<LineRenderer> ();

        /*
		//lr.material = renderMaterial;
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.startColor = current_color;
		lr.endColor = current_color;
		lr.startWidth = 2f;
		lr.endWidth = 2f;
		lr.material.color = current_color;
        */

		// Finish setting parameters of line
		lr.positionCount = points.Count;
		lr.SetPositions(points.ToArray());
		//lr.transform.position.Set(brush.transform.position.x, brush.transform.position.y, brush.transform.position.z);
        
        

		// Update Mesh
		CombineInstance[] combine = new CombineInstance[2];
		// Previous state of dynamically generated mesh
		combine [0].mesh = dynamicMesh;
		combine [0].transform = temp.GetComponent<LineRenderer> ().transform.localToWorldMatrix;
        Destroy(lr);

		// New primitive mesh around brush head
		combine [1].mesh = primitiveMesh;
		combine [1].transform = brush.transform.localToWorldMatrix;

		// Create temporary container for combined mesh 
		Mesh tmp = new Mesh();
		// Combine dynamic mesh with newest addition
		tmp.CombineMeshes (combine);
		dynamicMesh = tmp;

		// Add a mesh collider to detect collisions using the dynamic mesh
		MeshCollider collider = temp.AddComponent<MeshCollider>();
		collider.sharedMesh = dynamicMesh;
		temp.tag = "Paint";
		collider.material = physicsMaterial;


		// TO DO: create physics materials
		//collider.material = physicsMaterial;

		// TEMPORARY:
		// Render collider for testing purposes


		MeshFilter filter = temp.AddComponent<MeshFilter> ();
		filter.mesh = dynamicMesh;

		MeshRenderer rend = temp.AddComponent<MeshRenderer> ();
		rend.material = renderMaterial;
        rend.material.color = current_color;
	}

    /*
    void shootPaintball()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Vector3 forward = Camera.main.transform.forward * 10000;
            GameObject paintball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            paintball.transform.position = gameObject.transform.position;
            paintball.AddComponent<paintBullet>();
            paintball.GetComponent<paintBullet>().changeBlobProperties(paint_sprite, paint_material, bounce);
            paintball.GetComponent<Renderer>().material.color = Color.red;
            paintball.AddComponent<Rigidbody>();
            paintball.GetComponent<Rigidbody>().AddForce(forward);
        }
    }
    */

    void ChangeKind()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			if (current_kind == Kind.normal)
			{
                isFloating = false;
				current_kind = Kind.bounce;
				current_kind_string = "Bounce";
				current_color = Color.red;
				ChangePointerColor(current_color);
				physicsMaterial = bounce;
				//physicsMaterial = Resources.Load("/Materials/Bounce") as PhysicMaterial;
			}
			else if (current_kind == Kind.bounce)
			{
                isFloating = false;
                current_kind = Kind.slide;
				current_kind_string = "Slide";
				current_color = Color.blue;
				ChangePointerColor(current_color);
				physicsMaterial = slide;
				//physicsMaterial = Resources.Load("/Materials/Slide") as PhysicMaterial;
			}
			else if (current_kind == Kind.slide)
			{
                isFloating = true;
                current_kind = Kind.floating;
                current_kind_string = "Floating";
                current_color = Color.green;
                ChangePointerColor(current_color);
                physicsMaterial = normal;
                //physicsMaterial = Resources.Load("/Materials/Normal") as PhysicMaterial;
            }
            // Here is the part where color mixing is available
            else if (current_kind == Kind.floating)
            {
                isFloating = false;
                current_kind = Kind.bounce_slide;
                current_kind_string = "Bounce&Slide";
                current_color = new Color(255,0,255);
                ChangePointerColor(current_color);
                physicsMaterial = bounce_slide;
                //physicsMaterial = Resources.Load("/Materials/Bounce_Slide") as PhysicMaterial;
            }
            else if (current_kind == Kind.bounce_slide)
            {
                isFloating = true;
                current_kind = Kind.bounce_floating;
                current_kind_string = "Bounce&Floating";
                current_color = new Color(255, 255, 0);
                ChangePointerColor(current_color);
                physicsMaterial = bounce;
                //physicsMaterial = Resources.Load("/Materials/Bounce") as PhysicMaterial;
            }
            else if (current_kind == Kind.bounce_floating)
            {
                isFloating = true;
                current_kind = Kind.slide_floating;
                current_kind_string = "Slide&Floating";
                current_color = new Color(0, 255, 255);
                ChangePointerColor(current_color);
                physicsMaterial = slide;
                //physicsMaterial = Resources.Load("/Materials/Slide") as PhysicMaterial;
            }
            //Change to eraser
            else if (current_kind == Kind.slide_floating)
            {
                current_kind = Kind.erase;
                current_kind_string = "Erase";
                current_color = Color.grey;
                ChangePointerColor(current_color);
            }
            else
            {
                isFloating = false;
                current_kind = Kind.normal;
                current_kind_string = "Normal";
                current_color = Color.black;
                ChangePointerColor(current_color);
                physicsMaterial = normal;
                //physicsMaterial = Resources.Load("/Materials/Normal") as PhysicMaterial;
            }
        }

        else if (Input.GetKeyDown(KeyCode.X))
        {
            if (current_kind == Kind.slide)
            {
                isFloating = false;
                current_kind = Kind.bounce;
                current_kind_string = "Bounce";
                current_color = Color.red;
                ChangePointerColor(current_color);
                physicsMaterial = bounce;
                //physicsMaterial = Resources.Load("/Materials/Bounce") as PhysicMaterial;
            }
            else if (current_kind == Kind.floating)
            {
                isFloating = false;
                current_kind = Kind.slide;
                current_kind_string = "Slide";
                current_color = Color.blue;
                ChangePointerColor(current_color);
                physicsMaterial = slide;
                //physicsMaterial = Resources.Load("/Materials/Slide") as PhysicMaterial;
            }
            else if (current_kind == Kind.bounce_slide)
            {
                isFloating = true;
                current_kind = Kind.floating;
                current_kind_string = "Floating";
                current_color = Color.green;
                ChangePointerColor(current_color);
                physicsMaterial = normal;
                //physicsMaterial = Resources.Load("/Materials/Normal") as PhysicMaterial;
            }
            // Here is the part where color mixing is available
            else if (current_kind == Kind.bounce_floating)
            {
                isFloating = false;
                current_kind = Kind.bounce_slide;
                current_kind_string = "Bounce&Slide";
                current_color = new Color(255, 0, 255);
                ChangePointerColor(current_color);
                physicsMaterial = bounce_slide;
                //physicsMaterial = Resources.Load("/Materials/Bounce_Slide") as PhysicMaterial;
            }
            else if (current_kind == Kind.slide_floating)
            {
                isFloating = true;
                current_kind = Kind.bounce_floating;
                current_kind_string = "Bounce&Floating";
                current_color = new Color(255, 255, 0);
                ChangePointerColor(current_color);
                physicsMaterial = bounce;
                //physicsMaterial = Resources.Load("/Materials/Bounce") as PhysicMaterial;
            }
            else if (current_kind == Kind.erase)
            {
                isFloating = true;
                current_kind = Kind.slide_floating;
                current_kind_string = "Slide&Floating";
                current_color = new Color(0, 255, 255);
                ChangePointerColor(current_color);
                physicsMaterial = slide;
                //physicsMaterial = Resources.Load("/Materials/Slide") as PhysicMaterial;
            }
            //Change to eraser
            else if (current_kind == Kind.normal)
            {
                current_kind = Kind.erase;
                current_kind_string = "Erase";
                current_color = Color.grey;
                ChangePointerColor(current_color);
            }
            else if (current_kind == Kind.bounce)
            {
                isFloating = false;
                current_kind = Kind.normal;
                current_kind_string = "Normal";
                current_color = Color.black;
                ChangePointerColor(current_color);
                physicsMaterial = normal;
                //physicsMaterial = Resources.Load("/Materials/Normal") as PhysicMaterial;
            }
        }
    }

	void ChangePointerColor(Color input)
	{
        var standardShaderMaterial1 = brush.GetComponent<MeshRenderer>().material;
        standardShaderMaterial1.color = new Color(input.r, input.g, input.b, 0.75f);
        standardShaderMaterial1.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        standardShaderMaterial1.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        standardShaderMaterial1.SetInt("_ZWrite", 0);
        standardShaderMaterial1.DisableKeyword("_ALPHATEST_ON");
        standardShaderMaterial1.DisableKeyword("_ALPHABLEND_ON");
        standardShaderMaterial1.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        standardShaderMaterial1.renderQueue = 3000;
	}

    bool UpdateLength(Kind current, float length)
    {
        if(current == Kind.floating)
        {
            if (current_floating <= length)
            {
                return false;
            }
            else
            {
                current_floating -= length;
                return true;
            }
        }
        else if(current == Kind.bounce)
        {
            if(current_bounce <= length)
            {
                return false;
            }
            else
            {
                current_bounce -= length;
                return true;
            }
        }
        else if (current == Kind.slide)
        {
            if (current_slide <= length)
            {
                return false;
            }
            else
            {
                current_slide -= length;
                return true;
            }
        }
        else if (current == Kind.bounce_slide)
        {
            if ((current_slide<=length)||(current_bounce<=length))
            {
                return false;
            }
            else if (canPurple == true)
            {
                current_slide -= length;
                current_bounce -= length;
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (current == Kind.bounce_floating)
        {
            if ((current_floating <= length) || (current_bounce <= length))
            {
                return false;
            }
            else if (canYellow == true)
            {
                current_floating -= length;
                current_bounce -= length;
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (current == Kind.slide_floating)
        {
            if ((current_slide <= length) || (current_floating <= length))
            {
                return false;
            }
            else if(canTeal == true)
            {
                current_slide -= length;
                current_floating -= length;
                return true;
            }
            else
            {
                return false;
            }
        }
        //Check if on eraser
        else if (current == Kind.erase)
        {
            return false;
        }
        else
        {
            if(current_normal <= length)
            {
                return false;
            }
            else
            {
                current_normal -= length;
                return true;
            }
        }
    }

    void DrawAssistLine()
    {
        if (isAssistMode)
        {
            assistLineRenderer.enabled = true;
            Vector3 diff = brush.transform.position - player.transform.position;
            assistLineRenderer.material.SetTextureOffset("_MainTex", new Vector2(Time.timeSinceLevelLoad * 4f, 0f));
            assistLineRenderer.material.SetTextureScale("_MainTex", new Vector2(diff.magnitude, 1f));
            Vector3[] lineVertices = { brush.transform.position, player.transform.position };
            assistLineRenderer.SetPositions(lineVertices);
        }
        else
        {
            assistLineRenderer.enabled = false;
        }
    }
        
    void ReturnPaint(float length, Color color)
    {
        if (color == Color.black)
        {
            current_normal += length;
        }
        else if (color == Color.red)
        {
            current_bounce += length;
        }
        else if (color == Color.blue)
        {
            current_slide += length;
        }
        else if (color == Color.green)
        {
            current_floating += length;
        }
        else if (color == new Color(255, 0, 255))
        {
            current_slide += length;
            current_bounce += length;
        }
        else if (color == new Color(255, 255, 0))
        {
            current_floating += length;
            current_bounce += length;
        }
        else if (color == new Color(0, 255, 255))
        {
            current_slide += length;
            current_floating += length;
        }
        else
        {

        }
    }

}


