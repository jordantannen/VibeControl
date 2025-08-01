using UnityEngine;

public class DemoInstrument : InstrumentScript
{
    
    protected override void Play()
    {
        if (audioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
