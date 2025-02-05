using UnityEngine;
//using MoreMountains.NiceVibrations;

[RequireComponent(typeof(Collider))]
public class ClickOrTapToExplode : MonoBehaviour {

	public static ClickOrTapToExplode current;



#if UNITY_EDITOR || (!UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID))
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
			GetComponent<BoxCollider>().enabled = false;
			StartExplosion();
			//MMVibrationManager.Haptic(HapticTypes.MediumImpact);
			CarController.Current.ChangeHealth(-5);
			CarController.Current.ChangeBlendShape(2);
		}
    }
#endif

    private void Awake()
    {
		current = this;
    }


    void Update() {
		var thisCollider = GetComponent<Collider>();

		foreach (var i in Input.touches) {
			if (i.phase != TouchPhase.Began) {
				continue;
			}
			
			// It's kinda wasteful to do this raycast repeatedly for every ClickToExplode in the
			// scene, but since this component is just for testing I don't think it's worth the
			// bother to figure out some shared static solution.
			RaycastHit hit;
			if (!Physics.Raycast(Camera.main.ScreenPointToRay(i.position), out hit)) {
				continue;
			}
			if (hit.collider != thisCollider) {
				continue;
			}
			
			StartExplosion();
			return;
		}
	}
	
	public  void StartExplosion() {
		BroadcastMessage("Explode");
		GameObject.Destroy(gameObject);
		Debug.Log("explode");
	}
	
}
