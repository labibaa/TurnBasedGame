//using UnityEngine;

//public class CutsceneManagerTester : MonoBehaviour
//{
//    public CutsceneManager cutsceneManager;
//    public float interval = 5f;

//    private void Start()
//    {
//        // Start the coroutine to play animations at intervals
//        StartCoroutine(PlayAnimationsAtIntervals());
//    }

//    private System.Collections.IEnumerator PlayAnimationsAtIntervals()
//    {
//        while (true)
//        {
//            // Example usage: Playing animations for different characters
//            PlayAnimationForCharacter("Mon", "KeenSenses");
//            yield return new WaitForSeconds(interval);

//        }
//    }

//    private void PlayAnimationForCharacter(string characterName, string animationName)
//    {
//        cutsceneManager.PlayAnimationForCharacter(characterName, animationName);
//    }
//}




//////tester normally

////using UnityEngine;

////public class CutsceneManagerTester : MonoBehaviour
////{
////    public CutsceneManager cutsceneManager;

////    private void Start()
////    {
////        //Example usage: Playing animations for different characters

////        PlayAnimationForCharacter("Mon", "Dwarf Idle");
////    }

////    private void PlayAnimationForCharacter(string characterName, string animationName)
////    {
////        cutsceneManager.PlayAnimationForCharacter(characterName, animationName);
////    }
////}