namespace AdApp.Application.DTO.Responses
{
    public record class AdPlatformResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<AdPlatformLocationResponse> Locations { get; set; }
    }
}
