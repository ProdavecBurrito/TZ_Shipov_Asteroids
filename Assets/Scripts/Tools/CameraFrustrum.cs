using UnityEngine;

public static class CameraFrustrum 
{
    private static Plane[] frustrumPlanes;

    private static void CalculateFrustrum()
    {
        frustrumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
    }

    public static Vector2 CalculateBoxSize()
    {
        CalculateFrustrum();
        return new Vector2(frustrumPlanes[0].distance + frustrumPlanes[1].distance, frustrumPlanes[2].distance + frustrumPlanes[3].distance);
    }

    public static float CalculateWidth()
    {
        CalculateFrustrum();
        return frustrumPlanes[0].distance + frustrumPlanes[1].distance;
    }

    public static float CalculateHight()
    {
        CalculateFrustrum();
        return frustrumPlanes[2].distance + frustrumPlanes[3].distance;
    }

    public static float GetFifthPartOfHight()
    {
        return CalculateHight() / 5.0f;
    }
}
