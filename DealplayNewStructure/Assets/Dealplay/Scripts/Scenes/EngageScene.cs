using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
using VIDE_Data;
using TMPro;

namespace Dealplay
{
    public class EngageScene : Scene
    {
        public static new EngageScene Instance { get { return (EngageScene)instance; } }

        private new void Awake()
        {
            base.Awake();

            if (engageCharacter) engageCharacter.IsFreezeAnim = true;
        }

        protected override void OnCharacterVDNodeChange(string characterString)
        {
            if (characterString == "Engage")
            {
                if(engageCharacter && engageCharacter.IsFreezeAnim) engageCharacter.IsFreezeAnim = false;
            }
        }
    }
}
