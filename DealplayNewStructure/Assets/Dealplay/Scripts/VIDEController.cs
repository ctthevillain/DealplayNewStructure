using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using Pvr_UnitySDKAPI; arif
using UnityEngine.InputSystem;

public class VIDEController : MonoBehaviour
{
    public Template_UIManager templateUIManager;
    public VIDE_Assign videAssign;

    public TextMeshProUGUI feedbackText;
    public GameObject feedbackObject;
    public GraphController graphController;
    public Animator anim;

    [SerializeField] private InputActionAsset inputActionAsset;

    private InputAction activateActionL;
    private InputAction activateActionR;

    private List<string> feedbacks;
    private bool conversationEnded = false;
    private int feedbackNumber = 0;

    private void Awake()
    {
        activateActionL = inputActionAsset.FindActionMap("XRI LeftHand Interaction").FindAction("Activate");
        activateActionR = inputActionAsset.FindActionMap("XRI RightHand Interaction").FindAction("Activate");
    }

    // Start is called before the first frame update
    void Start()
    {

        if(anim == null || graphController == null || templateUIManager == null || videAssign == null || feedbackText == null || feedbackObject == null) {
            throw new System.Exception("VIDEController is not set up correctly.");
        }

        templateUIManager.Interact(videAssign);
        feedbacks = new List<string>();
        feedbackObject.SetActive(false);
    }

    private void OnEnable()
    {
        activateActionL.Enable();
        activateActionR.Enable();
    }

    private void OnDisable()
    {
        activateActionL.Disable();
        activateActionR.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if(conversationEnded == true) {
            if(activateActionL.WasPressedThisFrame() || activateActionR.WasPressedThisFrame()) // Controller.UPvr_GetKeyDown (0, Pvr_KeyCode.TRIGGER) || Controller.UPvr_GetKeyDown (1, Pvr_KeyCode.TRIGGER) // arif
            {
                feedbackNumber++;

                if(feedbackNumber < feedbacks.Count ) {
                    feedbackText.text = feedbacks[feedbackNumber];
                }
            }
        }
    }

    public void AddFeedback(string feedback) {
        feedbacks.Add(feedback);
    }

    public void EndConversation()
    {
        Debug.Log("EndConversation");

        graphController.StopTimer();
        conversationEnded = true;
        feedbackNumber = 0;
        //feedbackObject.SetActive(true);
        //feedbackText.text = feedbacks[feedbackNumber];
        anim.SetTrigger("Idle");

    }
}
