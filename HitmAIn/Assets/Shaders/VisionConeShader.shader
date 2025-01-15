Shader "Custom/VisionConeShader"
{
    SubShader
    {
        Tags {"Queue" = "Overlay"}
        Stencil
        {
            Ref 1
            Comp Always
            Pass Replace
        }
        Pass
        {
            // Render the vision cone color if needed
            ColorMask 0
        }
    }
}
