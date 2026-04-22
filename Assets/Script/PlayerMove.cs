using UnityEngine;

namespace Script
{
    public class PlayerMove : MonoBehaviour
    {
        public Rigidbody rb;
        
        [Header("移动与旋转设置")]
        public float moveSpeed = 60f; 
        public float rotateSpeed = 150f; 
        
        [Header("跳跃设置")]
        public float jumpForce = 5f;
        public bool isGrounded; 

        private float _h, _v;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
       
            _h = Input.GetAxis("Horizontal");
            _v = Input.GetAxis("Vertical");
            
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
        }

        private void FixedUpdate()
        {
            Vector3 moveDir = (transform.right * _h + transform.forward * _v).normalized;
            rb.AddForce(moveDir * moveSpeed, ForceMode.Force);
        }

        private void Jump()
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }
        
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
            }
        }
    }
}