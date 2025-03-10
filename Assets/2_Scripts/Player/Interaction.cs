using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.XR;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public float wallmaxCheckDistance;
    public float wallStaminaDef;
    public LayerMask itemlayerMask;
    public LayerMask walllayerMask;

    public GameObject curInteractGameObject;
    private IInteractable curinteractable;

    public TextMeshProUGUI promptText;

    public Camera camera;
    public Transform wallDect;
    private bool isWall;

    private void Start()
    {
        if (CameraMovement.isCurrentFp)
            camera = GameManager.Instance.cameraMovement.fpCamera;
        else
            camera = GameManager.Instance.cameraMovement.tpCamera;

    }

    private void Update()
    {
        if (CameraMovement.isCurrentFp)
        {
            camera = GameManager.Instance.cameraMovement.fpCamera;
            maxCheckDistance = 1f;

        }
        else
        {
            camera = GameManager.Instance.cameraMovement.tpCamera;
            maxCheckDistance = 5f;

        }


        Ray ray  = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); ;
        RaycastHit hit;

        if(Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.deltaTime;

            if(Physics.Raycast(ray, out hit, maxCheckDistance, itemlayerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curinteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curinteractable = null;
                promptText.gameObject.SetActive(false);
            }

        }
    }
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curinteractable.GetInteractPrompt();
    }

    public void OnInteractionInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && curinteractable != null)
        {
            curinteractable.OnInteract();
            curInteractGameObject = null;
            curinteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }


    public void WallInteraction(Rigidbody rigid,float slidingSpeed)
    {

        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.deltaTime;

            Ray[] rays = new Ray[4]
            {
                new Ray(wallDect.position, Vector3.forward),
                new Ray(wallDect.position, Vector3.right),
                new Ray(wallDect.position, -Vector3.forward),
                new Ray(wallDect.position, -Vector3.right)
            };

            RaycastHit hit;


            for(int i = 0; i < rays.Length; i++)
            {
                if (Physics.Raycast(rays[i], out hit, wallmaxCheckDistance, walllayerMask) && CharacterManager.Instance.Player.playerCondition.uiCondition.stamina.curValue > wallStaminaDef)
                {
                    isWall = true;
                    CharacterManager.Instance.Player.animationHandler.WallMove(isWall);
                    rigid.AddForce(Vector3.up * slidingSpeed, ForceMode.Force);
                    CharacterManager.Instance.Player.playerCondition.uiCondition.stamina.Substract(wallStaminaDef * Time.deltaTime);
                    break;
                }
                else
                {
                    isWall = false; // 벽이 감지되지 않았을 때 false 설정
                    CharacterManager.Instance.Player.animationHandler.WallMove(isWall); // isWall을 false로 설정

                }

            }

        }
    }



}
