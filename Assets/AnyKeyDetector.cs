using UnityEngine;

public class AnyKeyDetector : MonoBehaviour
{
    public LevelLoader ll;
    
    // Update is called once per frame
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            ll.LoadLevel(1);
        }
    }
}
