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
    public class ReflectionScene : Scene
    {
        public static new ReflectionScene Instance { get { return (ReflectionScene)instance; } }

        private new void Awake()
        {
            base.Awake();

            if(engageCharacter) engageCharacter.IsFreezeAnim = true;
        }
    }
}
