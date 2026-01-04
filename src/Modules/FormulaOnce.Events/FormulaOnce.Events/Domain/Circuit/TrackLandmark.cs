namespace FormulaOnce.Events.Domain.Circuit;

public record TrackLandmark
{
    private TrackLandmark()
    {
    }

    public TrackLandmark(string labe, LandmarkType landmarkType, int nearTurn)
    {
        Label = labe;
        LandmarkType = landmarkType;
        NearTurn = nearTurn;
    }

    public string Label { get; set; }
    public LandmarkType LandmarkType { get; set; }
    public int NearTurn { get; set; }
}