using UnityEngine;

public class ParticleUIManager : Singleton<ParticleUIManager>
{
    // [SerializeField] private UIParticle fxTouchPrefab;
    [SerializeField] private ParticleSystem fxPaperFireworks; 
    
    public void PlayPaperFireworks() => fxPaperFireworks.Play();
    public void StopPaperFireworks() => fxPaperFireworks.Stop();
}
