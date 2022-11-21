using UnityEngine;
using UnityEngine.Assertions;

public class RandomDebugs : MonoBehaviour {

	void Update(){

        if(Random.value < Time.deltaTime * 3){
            float rng = Random.value;

            if(rng <= 0.31f){
                Debug.Log("An example log message");
            } else if(0.31f < rng && rng < 0.62f){
                Debug.LogWarning("An example warning message");                
            } else if(0.62f < rng && rng < 0.93f){
                Debug.LogError("An example error message");
            } else {
                Assert.IsNotNull(null, "An example assert message");
                throw new System.Exception(
                    "Throwing an exception with a very long message...\n" + 
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
            }
        }

    }
	
}
