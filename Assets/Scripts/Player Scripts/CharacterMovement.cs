using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] 
    protected float xSpeed = 1f, ySpeed = 1f;

    private Vector3 moveDelta;

    private RaycastHit2D movementHit;

    private BoxCollider2D myCollider;

    private bool _hasPlayerTarget;

    private Rigidbody2D rb;

    public bool HasPlayerTarget
    {
        get { return _hasPlayerTarget; }
        set { _hasPlayerTarget = value; }
    }

    protected virtual void Awake() //overrideable
    {
        myCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected void HandleMovement(float x, float y)
    {
        moveDelta = new Vector3(x * xSpeed, y * ySpeed, 0f);
        rb.MovePosition(transform.position + new Vector3(moveDelta.x * Time.deltaTime, moveDelta.y * Time.deltaTime, 0f));
        
        //ITS NOT WORKING SOLID  RIGIDBODY 
        /*// Y AXIS COLLISION CHECK
        movementHit = Physics2D.BoxCast(transform.position, myCollider.size, 0f,new Vector2(0f, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), 
            LayerMask.GetMask(TagManager.BLOCKING_LAYER_MASK)); //Imagine like u attaching a box to player
        if (movementHit.collider == null)
        {
             //can move y axis
            //transform.Translate(0f, moveDelta.y * Time.deltaTime, 0f); 
        }
        
        // X AXIS COLLISION CHECK
        movementHit = Physics2D.BoxCast(transform.position, myCollider.size, 0f,new Vector2(moveDelta.x, 0f), Mathf.Abs(moveDelta.x * Time.deltaTime), 
            LayerMask.GetMask(TagManager.BLOCKING_LAYER_MASK)); //Imagine like u attaching a box to player
        if (movementHit.collider == null) 
        {
            //rb.MovePosition(transform.position + new Vector3(moveDelta.x * Time.deltaTime, 0f, 0f)); //can move x axis
            //transform.Translate(moveDelta.x * Time.deltaTime, 0f, 0f); //can move x axis
        }*/
        
    }

    public Vector3 GetMoveDelta()
    {
        return moveDelta;
    }

}
