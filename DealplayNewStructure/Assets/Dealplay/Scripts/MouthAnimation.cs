using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ReadyPlayerMe.ExtensionMethods;
using ReadyPlayerMe;

public class MouthAnimation : MonoBehaviour
{
    private SkinnedMeshRenderer headMesh;
    private SkinnedMeshRenderer beardMesh;
    private SkinnedMeshRenderer teethMesh;
    private int mouthOpenBlendShapeIndexOnHeadMesh = -1;
    private int mouthOpenBlendShapeIndexOnBeardMesh = -1; 
    private int mouthOpenBlendShapeIndexOnTeethMesh = -1;

    private bool isAnimating = true;
    private float randTimer = 0.5f;
    private bool mouthOpen = false;
    private const string MouthOpenBlendShapeName = "mouthOpen";

    private Animator anim = null;

    // Start is called before the first frame update
    void Start()
    {
        GetMeshAndSetIndex(MeshType.HeadMesh, ref headMesh, ref mouthOpenBlendShapeIndexOnHeadMesh);
        GetMeshAndSetIndex(MeshType.BeardMesh, ref beardMesh, ref mouthOpenBlendShapeIndexOnBeardMesh);
        GetMeshAndSetIndex(MeshType.TeethMesh, ref teethMesh, ref mouthOpenBlendShapeIndexOnTeethMesh);
    
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAnimating == true) {
            if(randTimer > 0f) {
                randTimer -= Time.deltaTime;

                if(randTimer < 0f) {
                    mouthOpen = !mouthOpen;

                    if(mouthOpen == true) {
                        SetBlendshapeWeights(1f);
                    } else {
                        SetBlendshapeWeights(0f);
                    }
                    randTimer = Random.Range(0.25f, 0.75f);
                }
            }
        }
    }

    public void SetAnimation(bool isAnim) {
        isAnimating = isAnim;
        SetBlendshapeWeights(0f);
        randTimer = Random.Range(0.25f, 0.75f);
    }

    // public void SetAnimTrigger(bool isTalking) {
    //     if(isTalking == true && anim != null && isPlayer == true) {
    //         anim.SetTrigger("Talking");
    //         //Debug.Log("Talking");
    //         isAnimating = true;
    //         SetBlendshapeWeights(0f);
    //         randTimer = Random.Range(0.25f, 0.75f);
    //     } else {
    //         if(anim != null && isPlayer == true) {
    //             anim.SetTrigger("Idle");
    //             //Debug.Log("Idle");
    //             isAnimating = false;
    //             SetBlendshapeWeights(0f);
    //         }
    //     }
    // }



    private void GetMeshAndSetIndex(MeshType meshType, ref SkinnedMeshRenderer mesh, ref int index)
    {
        mesh = gameObject.GetMeshRenderer(meshType);
        
        if(mesh != null)
        {
            index = mesh.sharedMesh.GetBlendShapeIndex(MouthOpenBlendShapeName);
        }
    }

    private void SetBlendshapeWeights(float weight)
    {
        SetBlendShapeWeight(headMesh, mouthOpenBlendShapeIndexOnHeadMesh);
        SetBlendShapeWeight(beardMesh, mouthOpenBlendShapeIndexOnBeardMesh);
        SetBlendShapeWeight(teethMesh, mouthOpenBlendShapeIndexOnTeethMesh);

        void SetBlendShapeWeight(SkinnedMeshRenderer mesh, int index)
        {
            if (index >= 0)
            {
                mesh.SetBlendShapeWeight(index, weight * 100f);
            }
        }
    }
}
