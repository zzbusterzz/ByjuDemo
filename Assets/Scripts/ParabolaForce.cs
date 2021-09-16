using UnityEngine;

public class ParabolaForce : MonoBehaviour
{
    public float currentImpactMul = 1.6f;
    public LineRenderer LR;

    private Rigidbody2D rigidBody;
    private Vector3 mouseUpPos = Vector3.zero;
    private bool isDrag = false;
    private Vector3 startPos;
    private Vector2 dir = Vector2.zero;
    private Vector3 beginPos;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        beginPos = transform.position;
    }

    private void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            isDrag = true;
            startPos = transform.position;
            LR.enabled = true;
            LR.positionCount = 2;
        }            
    }

    private void OnMouseDrag()
    {
        if (isDrag)
        {
            mouseUpPos = Input.mousePosition;
            mouseUpPos.z = Camera.main.nearClipPlane;
            mouseUpPos = Camera.main.ScreenToWorldPoint(mouseUpPos);

            Vector3 newMOuseUp = RotatePointAroundPivot(mouseUpPos, transform.position, new Vector3(180, 180, 0), 10);
            Debug.DrawLine(startPos, newMOuseUp, Color.blue);

            
            LR.SetPositions(new Vector3[] {transform.position, newMOuseUp});
        }
    }

    private void OnMouseUp()
    {
        if (isDrag)
        {
            mouseUpPos = Input.mousePosition;
            mouseUpPos.z = Camera.main.nearClipPlane;
           
            mouseUpPos = Camera.main.ScreenToWorldPoint(mouseUpPos);
        
            Vector3 newMOuseUp2 = RotatePointAroundPivot(mouseUpPos, transform.position, new Vector3(180, 180, 0));

            dir = newMOuseUp2 - transform.position;
            float ImpactVel = Mathf.Clamp(dir.magnitude , 0, 20) ;        
            rigidBody.AddForce(dir.normalized * ImpactVel * currentImpactMul, ForceMode2D.Impulse);
            
            isDrag = false;

            //Debug.DrawLine(startPos, newMOuseUp2, Color.red, 5);
            ////Debug.DrawLine(startPos, newMOuseUp, Color.red, 5);
            //Debug.DrawLine(startPos, dir.normalized, Color.blue, 5);
            LR.enabled = false;
            LR.positionCount = 0;
        }
    }

    Vector2 RotatePointAroundPivot(Vector3 point , Vector3 pivot, Vector3 angles, float scaleDir = 0.0f){
        Vector3 dir = point - pivot; 

        dir = Quaternion.Euler(angles) * dir; 

        if (scaleDir > 0)
            dir = dir.normalized * scaleDir;

        point = dir + pivot;

        return point; 
     }

    public void Reset()
    {
        rigidBody.velocity = Vector2.zero;
        transform.position = beginPos;
    }
}
