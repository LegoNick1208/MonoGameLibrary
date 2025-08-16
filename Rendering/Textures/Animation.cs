using System;
using System.Collections.Generic;

namespace MonoGameLibrary.Rendering.Textures;

public class Animation
{
    public List<TextureRegion> Frames { get; private set; }
    public TimeSpan Delay { get; set; }
    public Animation(List<TextureRegion> frames, TimeSpan delay)
    {
        Frames = frames;
        Delay = delay;
    }
}
