using NodaTime;

namespace ServiceA
{
    public record VectorStream
    {
        public int RowId { get; init; }
        public string Id { get; init; }
        public NodaVectorEvent DefaultEvent { get; init; }
        public Duration DataRate { get; init; }
        public SampleType SampleType { get; init; }
        public List<NodeReference> NodeReferences { get; init; }
        public List<ElementDescriptor> ElementDescriptors { get; init; }
    }
    public record NodeReference
    {
        public uint NodeId { get; init; }
        public string Path { get; init; }
        public string DisplayPath { get; init; }
    }
    public record NodaVectorEvent
    {
        public Instant Time { get; init; }
        public double[] Values { get; init; }

        public NodaVectorEvent() => Values = Array.Empty<double>();

        public NodaVectorEvent(params double[] values) => Values = values;
    }
    public enum SampleType
    {
        Point,
        Interval,
        AvgMinMaxCnt
    }
    public record ElementDescriptor
    {
        public string Name { get; init; }
        public string UnitId { get; init; }
        public string DisplayUnitId { get; init; }
        public string DisplayName { get; init; }
    }
}
